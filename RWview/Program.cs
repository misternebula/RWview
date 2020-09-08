using System;
using System.IO;

namespace RWview
{
    class Program
    {
        static void Main(string[] args)
        {
            SectionMapManager.LoadSectionMaps();
            var bytes = File.ReadAllBytes(@"E:\SimpsonsGame\output\WHOLEFOLDER\build\PS3\pal_en\assets\props\shared\generic_crate\geo\export\generic_crate_des\model.rws.PS3.preinstanced");
            string hex = BitConverter.ToString(bytes).Replace("-", "");

            var index = 0;
            var header = Utils.ReadHeader(hex, 0, ref index); // Should be clump
            var plugin = SectionMapManager.GetSectionFromId(header.ID); // Get clump plugin
            plugin.Deserialize(Utils.ReadFile(hex, index, header.Length * 2, ref index), 0); // Deserialize file
            ConsoleWriter.WriteStoredLines();
        }
    }
}
