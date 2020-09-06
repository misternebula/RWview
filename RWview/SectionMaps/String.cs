namespace RWview.SectionMaps
{
    class String : SectionBase
    {
        public override string Name => "String";
        public override string SectionId => "02000000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            Index = 0;
            ConsoleWriter.Write(levelsDeep, $"{Name}", true);
            ConsoleWriter.Write(levelsDeep, $" ├─ String : {Utils.HexToString(Utils.ReadFile(hex, Index, hex.Length, ref Index))}");
        }
    }
}
