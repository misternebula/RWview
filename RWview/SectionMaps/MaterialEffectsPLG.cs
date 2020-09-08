namespace RWview.SectionMaps
{
    class MaterialEffectsPLG : SectionBase
    {
        public override string Name => "Material Effects PLG";
        public override string SectionId => "20010000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            ConsoleWriter.Write(levelsDeep + 1, $"Type : {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little)}");
            if (hex.Length == Index)
            {
                return;
            }
            for (int i = 0; i < 2; i++)
            {
                var type = Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little);
                ConsoleWriter.Write(levelsDeep + 1, $"[Effect {i + 1}] Type : {type}");
                switch (type)
                {
                    case 0:
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 5:
                    case 6:
                        ConsoleWriter.Write(levelsDeep + 1, $"[Effect {i + 1}] RWView does not support this type yet.");
                        break;
                    case 4:
                        ConsoleWriter.Write(levelsDeep + 1, $"[Effect {i + 1}] Source Blend Mode : {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little)}");
                        ConsoleWriter.Write(levelsDeep + 1, $"[Effect {i + 1}] Destination Blend Mode : {Utils.HexToInt(Utils.ReadFile(hex, Index, 8, ref Index), Endian.Little)}");
                        var hasTexture = Utils.HexToBool(Utils.ReadFile(hex, Index, 8, ref Index));
                        ConsoleWriter.Write(levelsDeep + 1, $"[Effect {i + 1}] Has Texture : {hasTexture}");
                        if (hasTexture)
                        {
                            var nextHeader = Utils.ReadHeader(hex, Index, ref Index);
                            var nextHeaderPlugin = SectionMapManager.GetSectionFromId(nextHeader.ID);
                            nextHeaderPlugin.Deserialize(Utils.ReadFile(hex, Index, nextHeader.Length * 2, ref Index), levelsDeep + 1);
                        }
                        break;
                }
            }
        }
    }
}
