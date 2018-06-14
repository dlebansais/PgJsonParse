using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgDoT : GenericPgObject, IPgDoT
    {
        public PgDoT(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int DamagePerTick { get { return RawDamagePerTick.HasValue ? RawDamagePerTick.Value : 0; } }
        public int? RawDamagePerTick { get { return GetInt(0); } }
        public int NumTicks { get { return RawNumTicks.HasValue ? RawNumTicks.Value : 0; } }
        public int? RawNumTicks { get { return GetInt(4); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(8); } }
        public List<DoTSpecialRule> SpecialRuleList { get { return GetEnumList(12, ref _SpecialRuleList); } } private List<DoTSpecialRule> _SpecialRuleList;
        public string RawPreface { get { return GetString(16); } }
        public DamageType DamageType { get { return GetEnum<DamageType>(20); } }
    }
}
