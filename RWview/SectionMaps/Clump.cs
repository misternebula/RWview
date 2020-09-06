using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWview.SectionMaps
{
    class Clump : SectionBase
    {
        public override string Name => "Clump";
        public override string SectionId => "10000000";

        private int Index = 0;
        private int StructIndex = 0;

        public override void Deserialize(string hex, string consolePrefix1, string consolePrefix2)
        {
            Console.WriteLine($"{Name}, length {hex.Length / 2}");
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            Console.WriteLine($"{consolePrefix1} ├─ Struct, length {structHeader.Length}");
            Console.WriteLine($"{consolePrefix1} │   ├─ Atomic Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
            Console.WriteLine($"{consolePrefix1} │   ├─ Light Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
            Console.WriteLine($"{consolePrefix1} │   └─ Camera Count : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");

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
