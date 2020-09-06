using System;

namespace RWview.SectionMaps
{
    class Extension : SectionBase
    {
        public override string Name => "Extension";
        public override string SectionId => "03000000";

        private int Index = 0;

        public override void Deserialize(string hex, int levelsDeep)
        {
            ConsoleWriter.Write(levelsDeep, $"{Name}", true);
            if (hex == "")
            {
                return;
            }

            while (Index != (hex.Length))
            {
                Utils.FindNextSection(hex, ref Index, levelsDeep);
            }
        }
    }
}
