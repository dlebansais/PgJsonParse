namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgAbility
    {
        public string Key { get; set; } = string.Empty;
        public PgAbility AbilityGroup { get; set; }
        public AbilityAnimation Animation { get; set; }
        public PgAttributeCollection AttributesThatModAmmoConsumeChanceList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaDelayLoopTimeList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaPowerCostList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaResetTimeList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModPowerCostList { get; } = new PgAttributeCollection();
        public bool CanBeOnSidebar { get { return RawCanBeOnSidebar.HasValue && RawCanBeOnSidebar.Value; } }
        public bool? RawCanBeOnSidebar { get; set; }
        public bool CanSuppressMonsterShout { get { return RawCanSuppressMonsterShout.HasValue && RawCanSuppressMonsterShout.Value; } }
        public bool? RawCanSuppressMonsterShout { get; set; }
        public bool CanTargetUntargetableEnemies { get { return RawCanTargetUntargetableEnemies.HasValue && RawCanTargetUntargetableEnemies.Value; } }
        public bool? RawCanTargetUntargetableEnemies { get; set; }
        public List<Deaths> CausesOfDeathList { get; } = new List<Deaths>();
        public PgRecipeCostCollection CostList { get; } = new PgRecipeCostCollection();
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get; set; }
        public DamageType DamageType { get; set; }
        public bool DelayLoopIsAbortedIfAttacked { get { return RawDelayLoopIsAbortedIfAttacked.HasValue && RawDelayLoopIsAbortedIfAttacked.Value; } }
        public bool? RawDelayLoopIsAbortedIfAttacked { get; set; }
        public string DelayLoopMessage { get; set; } = string.Empty;
        public float DelayLoopTime { get { return RawDelayLoopTime.HasValue ? RawDelayLoopTime.Value : 0; } }
        public float? RawDelayLoopTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get; set; }
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get; set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public bool IgnoreEffectErrors { get { return RawIgnoreEffectErrors.HasValue && RawIgnoreEffectErrors.Value; } }
        public bool? RawIgnoreEffectErrors { get; set; }
        public bool InternalAbility { get { return RawInternalAbility.HasValue && RawInternalAbility.Value; } }
        public bool? RawInternalAbility { get; set; }
        public string InternalName { get; set; } = string.Empty;
        public bool IsHarmless { get { return RawIsHarmless.HasValue && RawIsHarmless.Value; } }
        public bool? RawIsHarmless { get; set; }
        public string ItemKeywordReqErrorMessage { get; set; } = string.Empty;
        public List<AbilityItemKeyword> ItemKeywordReqList { get; } = new List<AbilityItemKeyword>();
        public List<AbilityKeyword> KeywordList { get; } = new List<AbilityKeyword>();
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public string Name { get; set; } = string.Empty;
        public AbilityPetType PetTypeTagReq { get; set; }
        public int PetTypeTagReqMax { get { return RawPetTypeTagReqMax.HasValue ? RawPetTypeTagReqMax.Value : 0; } }
        public int? RawPetTypeTagReqMax { get; set; }
        public PgAbility Prerequisite { get; set; }
        public AbilityProjectile Projectile { get; set; }
        public PgAbilityPvX PvE { get; set; }
        public PgAbilityPvX PvP { get; set; }
        public float ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : 0; } }
        public float? RawResetTime { get; set; }
        public SelfParticle SelfParticle { get; set; }
        public string AmmoDescription { get; set; } = string.Empty;
        public PgAbility SharesResetTimerWith { get; set; }
        public PgSkill Skill { get; set; }
        public PgAbilityRequirementCollection SpecialCasterRequirementList { get; } = new PgAbilityRequirementCollection();
        public string SpecialCasterRequirementsErrorMessage { get; set; } = string.Empty;
        public string SpecialInfo { get; set; } = string.Empty;
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        public int? RawSpecialTargetingTypeReq { get; set; }
        public AbilityTarget Target { get; set; }
        public TargetEffectKeyword TargetEffectKeywordReq { get; set; }
        public AbilityTargetParticle TargetParticle { get; set; }
        public PgAbility UpgradeOf { get; set; }
        public bool WorksInCombat { get { return RawWorksInCombat.HasValue && RawWorksInCombat.Value; } }
        public bool? RawWorksInCombat { get; set; }
        public bool WorksUnderwater { get { return RawWorksUnderwater.HasValue && RawWorksUnderwater.Value; } }
        public bool? RawWorksUnderwater { get; set; }
        public bool WorksWhileFalling { get { return RawWorksWhileFalling.HasValue && RawWorksWhileFalling.Value; } }
        public bool? RawWorksWhileFalling { get; set; }
        public bool DelayLoopIsOnlyUsedInCombat { get { return RawDelayLoopIsOnlyUsedInCombat.HasValue && RawDelayLoopIsOnlyUsedInCombat.Value; } }
        public bool? RawDelayLoopIsOnlyUsedInCombat { get; set; }
        public PgAbilityAmmoCollection AmmoKeywordList { get; } = new PgAbilityAmmoCollection();
        public float AmmoConsumeChance { get { return RawAmmoConsumeChance.HasValue ? RawAmmoConsumeChance.Value : 0; } }
        public float? RawAmmoConsumeChance { get; set; }
        public float AmmoStickChance { get { return RawAmmoStickChance.HasValue ? RawAmmoStickChance.Value : 0; } }
        public float? RawAmmoStickChance { get; set; }
    }
}
