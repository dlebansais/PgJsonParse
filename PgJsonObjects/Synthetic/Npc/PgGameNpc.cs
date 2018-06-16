using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgGameNpc : GenericPgObject, IPgGameNpc
    {
        public PgGameNpc(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Name { get { return GetString(0); } }
        public string AreaFriendlyName { get { return GetString(4); } }
        public List<NpcPreference> PreferenceList { get { return GetObjectList(8, ref _PreferenceList); } } private List<NpcPreference> _PreferenceList;
        public List<NpcPreference> LikeList { get { return GetObjectList(12, ref _LikeList); } } private List<NpcPreference> _LikeList;
        public List<NpcPreference> HateList { get { return GetObjectList(16, ref _HateList); } } private List<NpcPreference> _HateList;
        public MapAreaName AreaName { get { return GetEnum<MapAreaName>(20); } }
    }
}
