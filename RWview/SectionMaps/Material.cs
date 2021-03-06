﻿using System.Linq;

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
            ConsoleWriter.Write(levelsDeep, $"{Name}");
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            ConsoleWriter.Write(levelsDeep + 1, $"Struct");
            ConsoleWriter.Write(levelsDeep + 2, $"Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
            ConsoleWriter.Write(levelsDeep + 2, $"Color RGBA : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
            ConsoleWriter.Write(levelsDeep + 2, $"??? : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
            ConsoleWriter.Write(levelsDeep + 2, $"Has Texture : {Utils.HexToBool(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex))}");

            while (Index != (hex.Length))
            {
                Utils.FindNextSection(hex, ref Index, levelsDeep);
            }
        }
    }
}
