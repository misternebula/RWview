using RWview.SectionMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWview
{
    static class PluginManager
    {
        static List<SectionBase> PluginList = new List<SectionBase>();

        public static void LoadPlugins()
        {
            PluginList = new List<SectionBase>
            {
                new Clump(),
                new FrameList(),
                new Extension()
            };
        }

        public static SectionBase GetSectionFromId(string sectionId)
        {
            if (!PluginList.Any(x => x.SectionId == sectionId))
            {
                return null;
            }
            return PluginList.First(section => section.SectionId == sectionId);
        }
    }
}
