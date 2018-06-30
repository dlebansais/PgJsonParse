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
        public ServerInfoEffectCollection ServerInfoEffectList { get { return GetObjectList(4, ref _ServerInfoEffectList, ServerInfoEffectCollection.CreateItem, () => new ServerInfoEffectCollection()); } } private ServerInfoEffectCollection _ServerInfoEffectList;
        public ItemCollection GiveItemList { get { return GetObjectList(8, ref _GiveItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _GiveItemList;
        public int NumItemsToGive { get { return RawNumItemsToGive.HasValue ? RawNumItemsToGive.Value : 0; } }
        public int? RawNumItemsToGive { get { return GetInt(12); } }
        public AbilityRequirementCollection OtherRequirementList { get { return GetObjectList(16, ref _OtherRequirementList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _OtherRequirementList;
        protected override List<string> FieldTableOrder { get { return GetStringList(20, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public ItemRequiredHotspot RequiredHotspot { get { return GetEnum<ItemRequiredHotspot>(24); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
