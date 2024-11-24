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
        public List<AbilityKeyword> MustHaveKeywordList { get; set; } = new();
        public List<AbilityKeyword> MustNotHaveKeywordList { get; set; } = new();
    }
}
