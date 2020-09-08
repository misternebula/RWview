namespace RWview.SectionMaps
{
    class BinMeshPLG : SectionBase
    {
        public override string Name => "Bin Mesh PLG";
        public override string SectionId => "0E050000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            ConsoleWriter.Write(levelsDeep + 1, $"Face Type : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            var splitCount = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little);
            ConsoleWriter.Write(levelsDeep + 1, $"Split Count: {splitCount}");
            ConsoleWriter.Write(levelsDeep + 1, $"Index Count: {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little)}");
            for (int i = 0; i < splitCount; i++)
            {
                ConsoleWriter.Write(levelsDeep + 1, $"[Split {i + 1}/{splitCount}] Index Count: {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little)}");
                ConsoleWriter.Write(levelsDeep + 1, $"[Split {i + 1}/{splitCount}] Material: {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little)}");
            }
        }
    }
}
