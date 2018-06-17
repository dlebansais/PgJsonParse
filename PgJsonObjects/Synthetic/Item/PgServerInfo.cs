namespace PgJsonObjects
{
    public class PgServerInfo : GenericPgObject, IPgServerInfo
    {
        public PgServerInfo(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public ServerInfoEffectCollection ServerInfoEffectList { get { return GetObjectList(0, ref _ServerInfoEffectList, ServerInfoEffectCollection.CreateItem, () => new ServerInfoEffectCollection()); } } private ServerInfoEffectCollection _ServerInfoEffectList;
        public ItemCollection GiveItemList { get { return GetObjectList(4, ref _GiveItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _GiveItemList;
        public int NumItemsToGive { get { return RawNumItemsToGive.HasValue ? RawNumItemsToGive.Value : 0; } }
        public int? RawNumItemsToGive { get { return GetInt(8); } }
        public AbilityRequirementCollection OtherRequirementList { get { return GetObjectList(12, ref _OtherRequirementList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _OtherRequirementList;
        public ItemRequiredHotspot RequiredHotspot { get { return GetEnum<ItemRequiredHotspot>(16); } }
    }
}
