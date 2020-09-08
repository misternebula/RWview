namespace RWview.SectionMaps
{
    class String : SectionBase
    {
        public override string Name => "String";
        public override string SectionId => "02000000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            ConsoleWriter.Write(levelsDeep + 1, $"String : {Utils.HexToString(Utils.ReadFile(hex, Index, hex.Length, ref Index))}");
        }
    }
}
