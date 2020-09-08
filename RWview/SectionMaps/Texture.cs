using System.Linq;

namespace RWview.SectionMaps
{
    class Texture : SectionBase
    {
        public override string Name => "Texture";
        public override string SectionId => "06000000";

        private int Index = 0;
        private int StructIndex = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            ConsoleWriter.Write(levelsDeep + 1, $"Struct");
            ConsoleWriter.Write(levelsDeep + 2, $"Filter Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");

            while (Index != (hex.Length))
            {
                Utils.FindNextSection(hex, ref Index, levelsDeep);
            }
        }
    }
}
