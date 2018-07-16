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
        public IPgNpcPreferenceCollection PreferenceList { get { return GetObjectList(12, ref _PreferenceList, PgNpcPreferenceCollection.CreateItem, () => new PgNpcPreferenceCollection()); } } private IPgNpcPreferenceCollection _PreferenceList;
        public IPgNpcPreferenceCollection LikeList { get { return GetObjectList(16, ref _LikeList, PgNpcPreferenceCollection.CreateItem, () => new PgNpcPreferenceCollection()); } } private IPgNpcPreferenceCollection _LikeList;
        public IPgNpcPreferenceCollection HateList { get { return GetObjectList(20, ref _HateList, PgNpcPreferenceCollection.CreateItem, () => new PgNpcPreferenceCollection()); } } private IPgNpcPreferenceCollection _HateList;
        protected override List<string> FieldTableOrder { get { return GetStringList(24, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public MapAreaName AreaName { get { return GetEnum<MapAreaName>(28); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Name } },
            { "AreaName", new FieldParser() {
                Type = FieldType.String,
                GetString = GetAreaName } },
            { "AreaFriendlyName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => AreaFriendlyName } },
            { "Preferences", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray= () => PreferenceList } },
        }; } }

        private string GetAreaName()
        {
            if (AreaName != MapAreaName.Internal_None)
                return "area" + StringToEnumConversion<MapAreaName>.ToString(AreaName);
            else
                return null;
        }
    }
}
