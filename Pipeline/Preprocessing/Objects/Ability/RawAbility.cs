﻿namespace Preprocessor;

public class RawAbility
{
    public string? AbilityGroup { get; set; }
    public string? AbilityGroupName { get; set; }
    public decimal? AmmoConsumeChance { get; set; }
    public string? AmmoDescription { get; set; }
    public Ammo[]? AmmoKeywords { get; set; }
    public decimal? AmmoStickChance { get; set; }
    public string? Animation { get; set; }
    public bool? AoEIsCenteredOnCaster { get; set; }
    public string? AttributeThatPreventsDelayLoopAbortOnAttacked { get; set; }
    public string[]? AttributesThatDeltaCritChance { get; set; }
    public string[]? AttributesThatDeltaDelayLoopTime { get; set; }
    public string[]? AttributesThatDeltaPowerCost { get; set; }
    public string[]? AttributesThatDeltaResetTime { get; set; }
    public string[]? AttributesThatDeltaWorksWhileStunned { get; set; }
    public string[]? AttributesThatModAmmoConsumeChance { get; set; }
    public string[]? AttributesThatModPowerCost { get; set; }
    public bool? CanBeOnSidebar { get; set; }
    public bool? CanSuppressMonsterShout { get; set; }
    public bool? CanTargetUntargetableEnemies { get; set; }
    public string[]? CausesOfDeath { get; set; }
    public int? CombatRefreshBaseAmount { get; set; }
    public RawConditionalKeyword[]? ConditionalKeywords { get; set; }
    public Cost[]? Costs { get; set; }
    public string? DamageType { get; set; }
    public bool? DelayLoopIsAbortedIfAttacked { get; set; }
    public bool? DelayLoopIsOnlyUsedInCombat { get; set; }
    public string? DelayLoopMessage { get; set; }
    public float? DelayLoopTime { get; set; }
    public string? Description { get; set; }
    public string[]? EffectKeywordsIndicatingEnabled { get; set; }
    public string[]? ExtraKeywordsForTooltips { get; set; }
    public int IconID { get; set; }
    public bool? IgnoreEffectErrors { get; set; }
    public bool? InternalAbility { get; set; }
    public string? InternalName { get; set; }
    public string? InventoryKeywordReqErrorMessage { get; set; }
    public string[]? InventoryKeywordReqs { get; set; }
    public bool? IsCosmeticPet { get; set; }
    public bool? IsHarmless { get; set; }
    public bool? IsTimerResetWhenDisabling { get; set; }
    public string? ItemKeywordReqErrorMessage { get; set; }
    public string[]? ItemKeywordReqs { get; set; }
    public string[]? Keywords { get; set; }
    public int Level { get; set; }
    public string? Name { get; set; }
    public string? PetTypeTagReq { get; set; }
    public int? PetTypeTagReqMax { get; set; }
    public string? Prerequisite { get; set; }
    public string? Projectile { get; set; }
    public RawPvEAbility? PvE { get; set; }
    public string? Rank { get; set; }
    public decimal ResetTime { get; set; }
    public string? SelfParticle { get; set; }
    public string? SelfPreParticle { get; set; }
    public string? SharesResetTimerWith { get; set; }
    public string? Skill { get; set; }
    public object? SpecialCasterRequirements { get; set; }
    public string? SpecialCasterRequirementsErrorMessage { get; set; }
    public string? SpecialInfo { get; set; }
    public int? SpecialTargetingTypeReq { get; set; }
    public string? Target { get; set; }
    public string? TargetEffectKeywordReq { get; set; }
    public string? TargetParticle { get; set; }
    public string? TargetTypeTagReq { get; set; }
    public string? UpgradeOf { get; set; }
    public bool? WorksInCombat { get; set; }
    public bool? WorksUnderwater { get; set; }
    public bool? WorksWhileFalling { get; set; }
    public bool? WorksWhileMounted { get; set; }
    public bool? WorksWhileStunned { get; set; }
}
