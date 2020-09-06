namespace RWview.SectionMaps
{
    class Extension : SectionBase
    {
        public override string Name => "Extension";
        public override string SectionId => "03000000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            Index = 0;
            ConsoleWriter.Write(levelsDeep, $"{Name}", true);
            if (hex == "")
            {
                return;
            }

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
