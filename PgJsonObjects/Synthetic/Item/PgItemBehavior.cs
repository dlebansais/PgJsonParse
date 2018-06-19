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

        public IPgServerInfo ServerInfo { get { return GetObject(0, ref _ServerInfo, PgServerInfo.CreateNew); } } private IPgServerInfo _ServerInfo;
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
