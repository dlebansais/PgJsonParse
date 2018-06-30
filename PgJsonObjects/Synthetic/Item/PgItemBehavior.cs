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
        public ItemUseVerb UseVerb { get { return GetEnum<ItemUseVerb>(24); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
