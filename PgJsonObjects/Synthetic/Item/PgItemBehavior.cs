using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemBehavior : GenericPgObject<PgItemBehavior>, IPgItemBehavior
    {
        public PgItemBehavior(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgItemBehavior CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItemBehavior CreateNew(byte[] data, ref int offset)
        {
            return new PgItemBehavior(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgServerInfo ServerInfo { get { return GetObject(4, ref _ServerInfo, PgServerInfo.CreateNew); } } private IPgServerInfo _ServerInfo;
        public List<ItemUseRequirement> UseRequirementList { get { return GetEnumList(8, ref _UseRequirementList); } } private List<ItemUseRequirement> _UseRequirementList;
        public ItemUseAnimation UseAnimation { get { return GetEnum<ItemUseAnimation>(12); } }
        public ItemUseAnimation UseDelayAnimation { get { return GetEnum<ItemUseAnimation>(14); } }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get { return GetInt(16); } }
        public double UseDelay { get { return RawUseDelay.HasValue ? RawUseDelay.Value : 0; } }
        public double? RawUseDelay { get { return GetDouble(20); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(24, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public ItemUseVerb UseVerb { get { return GetEnum<ItemUseVerb>(28); } }
        public bool IsServerInfoEmpty { get { return RawIsServerInfoEmpty.HasValue && RawIsServerInfoEmpty.Value; } }
        public bool? RawIsServerInfoEmpty { get { return GetBool(30, 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "UseVerb", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemUseVerb>.ToString(UseVerb, TextMaps.UseVerbStringMap, ItemUseVerb.Internal_None) } },
            { "ServerInfo", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => PgServerInfoCollection.CreateSingleOrEmptyList(ServerInfo),
                GetArrayIsEmpty = () => IsServerInfoEmpty,
                SimplifyArray = true } },
            { "UseRequirements", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<ItemUseRequirement>.ToStringList(UseRequirementList) } },
            { "UseAnimation", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemUseAnimation>.ToString(UseAnimation, null, ItemUseAnimation.Internal_None) } },
            { "UseDelayAnimation", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemUseAnimation>.ToString(UseDelayAnimation, null, ItemUseAnimation.Internal_None) } },
            { "MetabolismCost", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMetabolismCost } },
            { "UseDelay", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawUseDelay } },
        }; } }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion
    }
}
