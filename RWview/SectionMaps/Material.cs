using System.Linq;

namespace RWview.SectionMaps
{
    class Material : SectionBase
    {
        public override string Name => "Material";
        public override string SectionId => "07000000";

        private int Index = 0;
        private int StructIndex = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            Index = 0;
            StructIndex = 0;
            ConsoleWriter.Write(levelsDeep, $"{Name}", true);
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            ConsoleWriter.Write(levelsDeep, $" ├─ Struct");
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Color RGBA : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
            ConsoleWriter.Write(levelsDeep, $" │   ├─ ??? : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
            ConsoleWriter.Write(levelsDeep, $" │   └─ Has Texture : {Utils.HexToBool(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex))}");

            while (Index != (hex.Length))
            {
                Utils.FindNextSection(hex, ref Index, levelsDeep);
            }
        }
    }
}
