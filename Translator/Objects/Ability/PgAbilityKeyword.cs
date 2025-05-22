namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAbilityKeyword
    {
        public PgAttributeCollection AttributesThatDeltaAccuracyList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaCritChanceList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaDamageList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaPowerCostList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaRangeList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaResetTimeList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModCritDamageList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModDamageList { get; set; } = new PgAttributeCollection();
        public List<AbilityKeyword> MustHaveAbilityKeywordList { get; set; } = new();
        public string? MustHaveActiveSkill_Key { get; set; }
        public List<EffectKeyword> MustHaveEffectKeywordList { get; set; } = new();
        public List<AbilityKeyword> MustNotHaveAbilityKeywordList { get; set; } = new();
    }
}
