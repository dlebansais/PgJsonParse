using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemBehavior : GenericPgObject, IPgItemBehavior
    {
        public PgItemBehavior(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public ServerInfo ServerInfo { get { return GetObject(0, ref _ServerInfo); } } private ServerInfo _ServerInfo;
        public List<ItemUseRequirement> UseRequirementList { get { return GetEnumList(4, ref _UseRequirementList); } } private List<ItemUseRequirement> _UseRequirementList;
        public ItemUseAnimation UseAnimation { get { return GetEnum<ItemUseAnimation>(8); } }
        public ItemUseAnimation UseDelayAnimation { get { return GetEnum<ItemUseAnimation>(10); } }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get { return GetInt(12); } }
        public double UseDelay { get { return RawUseDelay.HasValue ? RawUseDelay.Value : 0; } }
        public double? RawUseDelay { get { return GetDouble(16); } }
        public ItemUseVerb UseVerb { get { return GetEnum<ItemUseVerb>(20); } }
    }
}
