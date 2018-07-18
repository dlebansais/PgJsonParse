using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgServerInfo : GenericPgObject<PgServerInfo>, IPgServerInfo
    {
        public PgServerInfo(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgServerInfo CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgServerInfo CreateNew(byte[] data, ref int offset)
        {
            return new PgServerInfo(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgServerInfoEffectCollection ServerInfoEffectList { get { return GetObjectList(4, ref _ServerInfoEffectList, PgServerInfoEffectCollection.CreateItem, () => new PgServerInfoEffectCollection()); } } private IPgServerInfoEffectCollection _ServerInfoEffectList;
        public IPgItemCollection GiveItemList { get { return GetObjectList(8, ref _GiveItemList, PgItemCollection.CreateItem, () => new PgItemCollection()); } } private IPgItemCollection _GiveItemList;
        public int NumItemsToGive { get { return RawNumItemsToGive.HasValue ? RawNumItemsToGive.Value : 0; } }
        public int? RawNumItemsToGive { get { return GetInt(12); } }
        public IPgAbilityRequirementCollection OtherRequirementList { get { return GetObjectList(16, ref _OtherRequirementList, PgAbilityRequirementCollection.CreateItem, () => new PgAbilityRequirementCollection()); } } private IPgAbilityRequirementCollection _OtherRequirementList;
        protected override List<string> FieldTableOrder { get { return GetStringList(20, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public ItemRequiredHotspot RequiredHotspot { get { return GetEnum<ItemRequiredHotspot>(24); } }
        public bool IsServerInfoEffectListEmpty { get { return RawIsServerInfoEffectListEmpty.HasValue && RawIsServerInfoEffectListEmpty.Value; } }
        public bool? RawIsServerInfoEffectListEmpty { get { return GetBool(26, 0); } }
        public bool IsOtherRequirementListEmpty { get { return RawIsOtherRequirementListEmpty.HasValue && RawIsOtherRequirementListEmpty.Value; } }
        public bool? RawIsOtherRequirementListEmpty { get { return GetBool(26, 2); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Effects", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = GetEffects,
                GetArrayIsEmpty = () => IsServerInfoEffectListEmpty } },
            { "GiveItems", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = GetGiveItems } },
            { "RequiredHotspot", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemRequiredHotspot>.ToString(RequiredHotspot, null, ItemRequiredHotspot.Internal_None) } },
            { "NumItemsToGive", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumItemsToGive } },
            { "OtherRequirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OtherRequirementList,
                GetArrayIsEmpty = () => IsOtherRequirementListEmpty,
                SimplifyArray = true } },
        }; } }

        private List<string> GetEffects()
        {
            List<string> Result = new List<string>();

            foreach (ServerInfoEffect Item in ServerInfoEffectList)
            {
                string RawEffect = Item.RawEffect;
                Result.Add(RawEffect);
            }

            return Result;
        }

        private List<string> GetGiveItems()
        {
            List<string> Result = new List<string>();

            foreach (Item Item in GiveItemList)
                Result.Add(Item.InternalName);

            return Result;
        }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion
    }
}
