using RWview.SectionMaps;
using System.Collections.Generic;
using System.Linq;

namespace RWview
{
    static class SectionMapManager
    {
        static List<SectionBase> SectionList = new List<SectionBase>();

        public static void LoadSectionMaps()
        {
            SectionList = new List<SectionBase>
            {
                new Atomic(),
                new BinMeshPLG(),
                new Clump(),
                new EAMesh(),
                new Extension(),
                new FrameList(),
                new GeometryList(),
                new Geometry(),
                new HAnimPLG(),
                new Material(),
                new MaterialEffectsPLG(),
                new MaterialList(),
                new String(),
                new Texture()
            };
        }

        public static SectionBase GetSectionFromId(string sectionId)
        {
            if (!SectionList.Any(x => x.SectionId == sectionId))
            {
                return null;
            }
            return SectionList.First(section => section.SectionId == sectionId);
        }
    }
}
