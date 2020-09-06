using System.Linq;

namespace RWview.SectionMaps
{
    class Geometry : SectionBase
    {
        public override string Name => "Geometry";
        public override string SectionId => "0F000000";

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
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Flags : {Utils.HexToBinary(Utils.ReadFile(structSection, StructIndex, 4, ref StructIndex))}");
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Unknown Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 4, ref StructIndex), true)}");
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Face Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Vertex Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Frame Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");

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
    }
}
