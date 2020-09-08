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
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            ConsoleWriter.Write(levelsDeep + 1, $"Struct");
            var frameCount = Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), Endian.Little);
            ConsoleWriter.Write(levelsDeep + 2, $"Frame Count : {frameCount}");
            for (int i = 0; i < frameCount; i++)
            {
                ConsoleWriter.Write(levelsDeep + 2, $"[{i + 1}/{frameCount}] Rotation Matrix : {Utils.ReadFile(structSection, StructIndex, 72, ref StructIndex)}");
                ConsoleWriter.Write(levelsDeep + 2, $"[{i + 1}/{frameCount}] Position : {Utils.ReadFile(structSection, StructIndex, 24, ref StructIndex)}");
                ConsoleWriter.Write(levelsDeep + 2, $"[{i + 1}/{frameCount}] Parent Frame : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), Endian.Little)}");
                ConsoleWriter.Write(levelsDeep + 2, $"[{i + 1}/{frameCount}] Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
            }

            while (Index != (hex.Length))
            {
                Utils.FindNextSection(hex, ref Index, levelsDeep);
            }
        }

        private string DeserializeTVector()
        {
            return "";
        }
    }
}
