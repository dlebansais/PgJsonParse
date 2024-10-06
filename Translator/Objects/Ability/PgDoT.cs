namespace PgObjects
{
    using System.Collections.Generic;

    public class PgDoT
    {
        public int DamagePerTick { get { return RawDamagePerTick.HasValue ? RawDamagePerTick.Value : 0; } }
        public int? RawDamagePerTick { get; set; }
        public int NumTicks { get { return RawNumTicks.HasValue ? RawNumTicks.Value : 0; } }
        public int? RawNumTicks { get; set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; set; }
        public DamageType DamageType { get; set; }
        public List<DoTSpecialRule> SpecialRuleList { get; set; } = new List<DoTSpecialRule>();
        public PgAttributeCollection AttributesThatDeltaList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModList { get; set; } = new PgAttributeCollection();
        public string Preface { get; set; } = string.Empty;
        public EffectKeyword ReqEffectKeyword { get; set; }
    }
}
