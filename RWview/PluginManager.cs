using RWview.SectionMaps;
using System.Collections.Generic;
using System.Linq;

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
                new Extension(),
                new FrameList(),
                new GeometryList(),
                new Geometry(),
                new HAnimPLG()
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
