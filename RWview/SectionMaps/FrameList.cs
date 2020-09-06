using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWview.SectionMaps
{
    class FrameList : SectionBase
    {
        public override string Name => "Frame List";
        public override string SectionId => "0E000000";

        private int Index = 0;
        private int StructIndex = 0;

        public override void Deserialize(string hex, string consolePrefix1, string consolePrefix2)
        {
            Console.WriteLine($"{consolePrefix1}{Name}, length {hex.Length / 2}");
            var structHeader = Utils.ReadHeader(hex, Index, ref Index);
            var structSection = new string(hex.Skip(Index).Take(structHeader.Length * 2).ToArray());
            Index += (structHeader.Length * 2);
            Console.WriteLine($"{consolePrefix2} ├─ Struct, length {structHeader.Length}");
            var frameCount = Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true);
            Console.WriteLine($"{consolePrefix2} │   ├─ Frame Count : {frameCount}");
            for (int i = 0; i < frameCount; i++)
            {
                Console.WriteLine($"{consolePrefix2} │   ├─ [{i + 1}/{frameCount}] Rotation Matrix : {Utils.ReadFile(structSection, StructIndex, 72, ref StructIndex)}");
                Console.WriteLine($"{consolePrefix2} │   ├─ [{i + 1}/{frameCount}] Position : {Utils.ReadFile(structSection, (i * 56) + StructIndex, 24, ref StructIndex)}");
                Console.WriteLine($"{consolePrefix2} │   ├─ [{i + 1}/{frameCount}] Parent Frame : {Utils.HexToInt(Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex), true)}");
                if (i + 1 != frameCount)
                {
                    Console.WriteLine($"{consolePrefix2} │   ├─ [{i + 1}/{frameCount}] Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
                }
                else
                {
                    Console.WriteLine($"{consolePrefix2} │   └─ [{i + 1}/{frameCount}] Flags : {Utils.ReadFile(structSection, StructIndex, 8, ref StructIndex)}");
                }
            }

            var nextHeader = Utils.ReadHeader(hex, Index, ref Index);
            var nextHeaderPlugin = PluginManager.GetSectionFromId(nextHeader.ID);
            if (nextHeaderPlugin == null)
            {
                Console.WriteLine($"{consolePrefix2} ├─ Unknown ({nextHeader.ID}), length {nextHeader.Length}");
            }
            else
            {
                nextHeaderPlugin.Deserialize(Utils.ReadFile(hex, Index, nextHeader.Length * 2, ref Index), consolePrefix1 + " ├─ ", consolePrefix2 + " │  ");
            }

            var nextHeader2 = Utils.ReadHeader(hex, Index, ref Index);
            var nextHeader2Plugin = PluginManager.GetSectionFromId(nextHeader2.ID);
            if (nextHeader2Plugin == null)
            {
                Console.WriteLine($"{consolePrefix2} ├─ Unknown ({nextHeader2.ID}), length {nextHeader2.Length}");
            }
            else
            {
                nextHeader2Plugin.Deserialize(Utils.ReadFile(hex, Index, nextHeader2.Length * 2, ref Index), consolePrefix1 + " ├─ ", consolePrefix2 + " │  ");
            }
        }

        private string DeserializeTVector()
        {
            return "";
        }
    }
}
