using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgGameNpc : MainPgObject<PgGameNpc>, IPgGameNpc
    {
        public PgGameNpc(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 30;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgGameNpc CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgGameNpc CreateNew(byte[] data, ref int offset)
        {
            return new PgGameNpc(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Name { get { return GetString(4); } }
        public string AreaFriendlyName { get { return GetString(8); } }
        public NpcPreferenceCollection PreferenceList { get { return GetObjectList(12, ref _PreferenceList, NpcPreferenceCollection.CreateItem, () => new NpcPreferenceCollection()); } } private NpcPreferenceCollection _PreferenceList;
        public NpcPreferenceCollection LikeList { get { return GetObjectList(16, ref _LikeList, NpcPreferenceCollection.CreateItem, () => new NpcPreferenceCollection()); } } private NpcPreferenceCollection _LikeList;
        public NpcPreferenceCollection HateList { get { return GetObjectList(20, ref _HateList, NpcPreferenceCollection.CreateItem, () => new NpcPreferenceCollection()); } } private NpcPreferenceCollection _HateList;
        protected override List<string> FieldTableOrder { get { return GetStringList(24, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public MapAreaName AreaName { get { return GetEnum<MapAreaName>(28); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
