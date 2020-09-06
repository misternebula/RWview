using System.Linq;

namespace RWview.SectionMaps
{
    class Texture : SectionBase
    {
        public override string Name => "Material List";
        public override string SectionId => "06000000";

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
            ConsoleWriter.Write(levelsDeep, $" │   └─ Filter Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");

            RWHeader nextHeader;
            SectionBase nextHeaderPlugin;

            while (Index != (hex.Length))
            {
                nextHeader = Utils.ReadHeader(hex, Index, ref Index);
                nextHeaderPlugin = PluginManager.GetSectionFromId(nextHeader.ID);
                if (nextHeaderPlugin == null)
                {
                    ConsoleWriter.Write(levelsDeep, $" ├─ Unknown ({nextHeader.ID})");
                }
                else
                {
                    nextHeaderPlugin.Deserialize(Utils.ReadFile(hex, Index, nextHeader.Length * 2, ref Index), levelsDeep + 1);
                }
            }
        }
    }
}
