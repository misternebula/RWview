using System.Linq;

namespace RWview.SectionMaps
{
    class MaterialList : SectionBase
    {
        public override string Name => "Material List";
        public override string SectionId => "08000000";

        private int Index = 0;
        private int StructIndex = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}", true);
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            ConsoleWriter.Write(levelsDeep, $" ├─ Struct");
            var matCount = Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true);
            ConsoleWriter.Write(levelsDeep, $" │   ├─ Material Count : {matCount}");
            for (int i = 0; i < matCount; i++)
            {
                if (i + 1 != matCount)
                {
                    ConsoleWriter.Write(levelsDeep, $" │   ├─ [{i + 1}/{matCount}] Material Index : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
                }
                else
                {
                    ConsoleWriter.Write(levelsDeep, $" │   └─ [{i + 1}/{matCount}] Material Index : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
                }
            }

            while (Index != (hex.Length))
            {
                Utils.FindNextSection(hex, ref Index, levelsDeep);
            }
        }
    }
}
