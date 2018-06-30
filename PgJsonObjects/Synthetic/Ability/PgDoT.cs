using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgDoT : GenericPgObject<PgDoT>, IPgDoT
    {
        public PgDoT(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgDoT CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgDoT CreateNew(byte[] data, ref int offset)
        {
            return new PgDoT(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int DamagePerTick { get { return RawDamagePerTick.HasValue ? RawDamagePerTick.Value : 0; } }
        public int? RawDamagePerTick { get { return GetInt(4); } }
        public int NumTicks { get { return RawNumTicks.HasValue ? RawNumTicks.Value : 0; } }
        public int? RawNumTicks { get { return GetInt(8); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(12); } }
        public List<DoTSpecialRule> SpecialRuleList { get { return GetEnumList(16, ref _SpecialRuleList); } } private List<DoTSpecialRule> _SpecialRuleList;
        public string RawPreface { get { return GetString(20); } }
        public DamageType DamageType { get { return GetEnum<DamageType>(24); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
