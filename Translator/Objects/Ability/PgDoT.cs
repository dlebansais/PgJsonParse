namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgDoT
    {
        public int DamagePerTick { get { return RawDamagePerTick.HasValue ? RawDamagePerTick.Value : 0; } }
        public int? RawDamagePerTick { get; private set; }
        public int NumTicks { get { return RawNumTicks.HasValue ? RawNumTicks.Value : 0; } }
        public int? RawNumTicks { get; private set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; private set; }
        public List<DoTSpecialRule> SpecialRuleList { get; } = new List<DoTSpecialRule>();
        public string Preface { get; private set; } = string.Empty;
        public DamageType DamageType { get; private set; }
        public PgAttributeCollection AttributesThatDeltaList { get; private set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModList { get; private set; } = new PgAttributeCollection();
    }
}
