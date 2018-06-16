using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgServerInfo : GenericPgObject, IPgServerInfo
    {
        public PgServerInfo(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<ServerInfoEffect> ServerInfoEffectList { get { return GetObjectList(0, ref _ServerInfoEffectList); } } private List<ServerInfoEffect> _ServerInfoEffectList;
        public List<Item> GiveItemList { get { return GetObjectList(4, ref _GiveItemList); } } private List<Item> _GiveItemList;
        public int NumItemsToGive { get { return RawNumItemsToGive.HasValue ? RawNumItemsToGive.Value : 0; } }
        public int? RawNumItemsToGive { get { return GetInt(8); } }
        public List<AbilityRequirement> OtherRequirementList { get { return GetObjectList(12, ref _OtherRequirementList); } } private List<AbilityRequirement> _OtherRequirementList;
        public ItemRequiredHotspot RequiredHotspot { get { return GetEnum<ItemRequiredHotspot>(16); } }
    }
}
