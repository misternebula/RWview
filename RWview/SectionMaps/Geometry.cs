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
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            ConsoleWriter.Write(levelsDeep + 1, $"Struct");
            ConsoleWriter.Write(levelsDeep + 2, $"Flags : {Utils.HexToBinary(Utils.ReadFile(structSection, StructIndex, 4, ref StructIndex))}");
            ConsoleWriter.Write(levelsDeep + 2, $"Unknown Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 4, ref StructIndex), Endian.Little)}");
            ConsoleWriter.Write(levelsDeep + 2, $"Face Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), Endian.Little)}");
            ConsoleWriter.Write(levelsDeep + 2, $"Vertex Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), Endian.Little)}");
            ConsoleWriter.Write(levelsDeep + 2, $"Frame Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), Endian.Little)}");

            while (Index != (hex.Length))
            {
                Utils.FindNextSection(hex, ref Index, levelsDeep);
            }
        }
    }
}
