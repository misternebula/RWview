using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWview.SectionMaps
{
    class Extension : SectionBase
    {
        public override string Name => "Extension";
        public override string SectionId => "03000000";

        private int Index = 0;

        public override void Deserialize(string hex, string consolePrefix1, string consolePrefix2)
        {
            Console.WriteLine($"{consolePrefix1}{Name}, length {hex.Length / 2}");
            if (hex == "")
            {
                return;
            }
            var nextHeader = Utils.ReadHeader(hex, Index, ref Index);
            var nextHeaderPlugin = PluginManager.GetSectionFromId(nextHeader.ID);
            if (nextHeaderPlugin == null)
            {
                Console.WriteLine($"{consolePrefix1} ├─ Unknown ({nextHeader.ID}), length {nextHeader.Length}");
            }
            else
            {
                nextHeaderPlugin.Deserialize(Utils.ReadFile(hex, Index, nextHeader.Length * 2, ref Index), " ├─ ", " │  ");
            }
        }
    }
}
