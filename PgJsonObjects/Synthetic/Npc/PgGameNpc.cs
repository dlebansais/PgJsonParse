﻿namespace PgJsonObjects
{
    public class PgGameNpc : MainPgObject<PgGameNpc>, IPgGameNpc
    {
        public PgGameNpc(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgGameNpc CreateItem(byte[] data, int offset)
        {
            return new PgGameNpc(data, offset);
        }

        public string Name { get { return GetString(0); } }
        public string AreaFriendlyName { get { return GetString(4); } }
        public NpcPreferenceCollection PreferenceList { get { return GetObjectList(8, ref _PreferenceList, NpcPreferenceCollection.CreateItem, () => new NpcPreferenceCollection()); } } private NpcPreferenceCollection _PreferenceList;
        public NpcPreferenceCollection LikeList { get { return GetObjectList(12, ref _LikeList, NpcPreferenceCollection.CreateItem, () => new NpcPreferenceCollection()); } } private NpcPreferenceCollection _LikeList;
        public NpcPreferenceCollection HateList { get { return GetObjectList(16, ref _HateList, NpcPreferenceCollection.CreateItem, () => new NpcPreferenceCollection()); } } private NpcPreferenceCollection _HateList;
        public MapAreaName AreaName { get { return GetEnum<MapAreaName>(20); } }
    }
}
