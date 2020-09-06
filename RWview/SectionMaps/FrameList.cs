using System.Linq;

namespace RWview.SectionMaps
{
    class FrameList : SectionBase
    {
        public override string Name => "Frame List";
        public override string SectionId => "0E000000";

        private int Index = 0;
        private int StructIndex = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            Index = 0;
            StructIndex = 0;
            ConsoleWriter.Write(levelsDeep, $"{Name}, length {hex.Length / 2}", true);
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            ConsoleWriter.Write(levelsDeep, $" ├─ Struct, length {structHeader.Length}");
            var frameCount = Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true);
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Frame Count : {frameCount}");
            for (int i = 0; i < frameCount; i++)
            {
                ConsoleWriter.Write(levelsDeep, $" │   ├─ [{i + 1}/{frameCount}] Rotation Matrix : {Utils.ReadFile(structSection, StructIndex, 72, ref StructIndex)}");
                ConsoleWriter.Write(levelsDeep, $" │   ├─ [{i + 1}/{frameCount}] Position : {Utils.ReadFile(structSection, StructIndex, 24, ref StructIndex)}");
                ConsoleWriter.Write(levelsDeep, $" │   ├─ [{i + 1}/{frameCount}] Parent Frame : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
                if (i + 1 != frameCount)
                {
                    ConsoleWriter.Write(levelsDeep, $" │   ├─ [{i + 1}/{frameCount}] Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
                }
                else
                {
                    ConsoleWriter.Write(levelsDeep, $" │   └─ [{i + 1}/{frameCount}] Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
                }
            }

            RWHeader nextHeader;
            SectionBase nextHeaderPlugin;

            while (Index != (hex.Length))
            {
                nextHeader = Utils.ReadHeader(hex, Index, ref Index);
                nextHeaderPlugin = PluginManager.GetSectionFromId(nextHeader.ID);
                if (nextHeaderPlugin == null)
                {
                    ConsoleWriter.Write(levelsDeep, $" ├─ Unknown ({nextHeader.ID}), length {nextHeader.Length}");
                }
                else
                {
                    nextHeaderPlugin.Deserialize(Utils.ReadFile(hex, Index, nextHeader.Length * 2, ref Index), levelsDeep + 1);
                }
            }
        }

        private string DeserializeTVector()
        {
            return "";
        }
    }
}
