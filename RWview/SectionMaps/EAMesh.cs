namespace RWview.SectionMaps
{
    class EAMesh : SectionBase
    {
        public override string Name => "EA Mesh";
        public override string SectionId => "33EA0000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            ConsoleWriter.Write(levelsDeep + 1, $"Face Data Offset : {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), true)}");
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            var meshChunkStart = Index;
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            var tableCount = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), false);
            ConsoleWriter.Write(levelsDeep + 1, $"Table Count : {tableCount}");
            var meshSubCount = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), false);
            ConsoleWriter.Write(levelsDeep + 1, $"Sub-mesh count : {meshSubCount}");
            ConsoleWriter.Write(levelsDeep + 1, $"Unknown Table");
            for (int i = 0; i < tableCount; i++)
            {
                ConsoleWriter.Write(levelsDeep + 2, $"[{i+1}/{tableCount}] Unknown : {Utils.ReadFile(hex, Index, 16, ref Index)}");
            }
            for (int i = 0; i < meshSubCount; i++)
            {
                ConsoleWriter.Write(levelsDeep + 1, $"New Sub Mesh");
                ConsoleWriter.Write(levelsDeep + 2, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
                ConsoleWriter.Write(levelsDeep + 2, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
                var offset = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), false);
                ConsoleWriter.Write(levelsDeep + 2, $"Offset : {offset}");
                var difference = (meshChunkStart + offset * 2 + 24) - Index;
                for (int x = 0; x < difference; x += 8)
                {
                    ConsoleWriter.Write(levelsDeep + 2, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
                }
                var vertCountDataOffset = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), false);
                ConsoleWriter.Write(levelsDeep + 2, $"Vert Count Data Offset : {vertCountDataOffset}");
                difference = (meshChunkStart + vertCountDataOffset * 2) - Index;
                for (int x = 0; x < difference; x += 8)
                {
                    ConsoleWriter.Write(levelsDeep + 2, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
                }
                var vertChunkTotalSize = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), false);
                ConsoleWriter.Write(levelsDeep + 2, $"Vert Chunk Total Size : {vertChunkTotalSize}");
                var vertChunkSize = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), false);
                ConsoleWriter.Write(levelsDeep + 2, $"Vert Chunk Size : {vertChunkSize}");
                ConsoleWriter.Write(levelsDeep + 2, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
                ConsoleWriter.Write(levelsDeep + 2, $"Unknown : {Utils.ReadFile(hex, Index, 8, ref Index)}");
            }
        }
    }
}
