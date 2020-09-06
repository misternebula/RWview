namespace RWview.SectionMaps
{
    class HAnimPLG : SectionBase
    {
        public override string Name => "HAnim PLG";
        public override string SectionId => "1E010000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}", true);
            ConsoleWriter.Write(levelsDeep, $" ├─ HAnim Version : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            ConsoleWriter.Write(levelsDeep, $" ├─ Node ID : {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), true)}");
            var boneCount = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), true);
            if (boneCount == 0)
            {
                ConsoleWriter.Write(levelsDeep, $" └─ Bone Count : {boneCount}");
            }
            else
            {
                ConsoleWriter.Write(levelsDeep, $" └─ THERE ARE BONES HERE, BUT HANIMPLG.CS HAS NOT BEEN WRITTEN TO HANDLE THEM.");
            }
        }
    }
}
