namespace Preprocessor;

internal class Ability
{
    public Ability(RawAbility rawAbility)
    {
        AbilityGroup = rawAbility.AbilityGroup;
        AbilityGroupName = rawAbility.AbilityGroupName;
        AmmoConsumeChance = rawAbility.AmmoConsumeChance;
        AmmoDescription = rawAbility.AmmoDescription;
        AmmoKeywords = rawAbility.AmmoKeywords;
        AmmoStickChance = rawAbility.AmmoStickChance;
        Animation = rawAbility.Animation;
        AoEIsCenteredOnCaster = rawAbility.AoEIsCenteredOnCaster;
        AttributesThatDeltaDelayLoopTime = rawAbility.AttributesThatDeltaDelayLoopTime;
        AttributesThatDeltaPowerCost = rawAbility.AttributesThatDeltaPowerCost;
        AttributesThatDeltaResetTime = rawAbility.AttributesThatDeltaResetTime;
        AttributesThatDeltaWorksWhileStunned = rawAbility.AttributesThatDeltaWorksWhileStunned;
        AttributesThatModAmmoConsumeChance = rawAbility.AttributesThatModAmmoConsumeChance;
        AttributesThatModPowerCost = rawAbility.AttributesThatModPowerCost;
        CanBeOnSidebar = rawAbility.CanBeOnSidebar;
        CanSuppressMonsterShout = rawAbility.CanSuppressMonsterShout;
        CanTargetUntargetableEnemies = rawAbility.CanTargetUntargetableEnemies;
        CausesOfDeath = rawAbility.CausesOfDeath;
        CombatRefreshBaseAmount = rawAbility.CombatRefreshBaseAmount;
        Costs = rawAbility.Costs;
        DamageType = rawAbility.DamageType;
        DelayLoopIsAbortedIfAttacked = rawAbility.DelayLoopIsAbortedIfAttacked;
        DelayLoopIsOnlyUsedInCombat = rawAbility.DelayLoopIsOnlyUsedInCombat;
        DelayLoopMessage = rawAbility.DelayLoopMessage;
        DelayLoopTime = rawAbility.DelayLoopTime;
        Description = rawAbility.Description;
        EffectKeywordsIndicatingEnabled = rawAbility.EffectKeywordsIndicatingEnabled;
        ExtraKeywordsForTooltips = rawAbility.ExtraKeywordsForTooltips;
        IconID = rawAbility.IconID;
        IgnoreEffectErrors = rawAbility.IgnoreEffectErrors;
        InternalAbility = rawAbility.InternalAbility;
        InternalName = rawAbility.InternalName;
        InventoryKeywordReqErrorMessage = rawAbility.InventoryKeywordReqErrorMessage;
        InventoryKeywordReqs = rawAbility.InventoryKeywordReqs;
        IsCosmeticPet = rawAbility.IsCosmeticPet;
        IsHarmless = rawAbility.IsHarmless;
        ItemKeywordReqErrorMessage = rawAbility.ItemKeywordReqErrorMessage;
        ItemKeywordReqs = rawAbility.ItemKeywordReqs;
        Keywords = rawAbility.Keywords;
        Level = rawAbility.Level;
        Name = rawAbility.Name;
        PetTypeTagReq = rawAbility.PetTypeTagReq;
        PetTypeTagReqMax = rawAbility.PetTypeTagReqMax;
        Prerequisite = rawAbility.Prerequisite;

        IsProjectileNone = rawAbility.Projectile == "0";
        Projectile = IsProjectileNone ? null : rawAbility.Projectile;

        PvE = rawAbility.PvE is null ? null : new PvEAbility(rawAbility.PvE);
        Rank = rawAbility.Rank is null ? null : int.Parse(rawAbility.Rank);
        ResetTime = rawAbility.ResetTime;
        SelfParticle = AbilityParticle.Parse(rawAbility.SelfParticle);
        SelfPreParticle = AbilityParticle.Parse(rawAbility.SelfPreParticle);
        SharesResetTimerWith = rawAbility.SharesResetTimerWith;
        Skill = rawAbility.Skill;
        SpecialCasterRequirements = Preprocessor.ToSingleOrMultiple<Requirement>(rawAbility.SpecialCasterRequirements, out SpecialCasterRequirementsIsSingle);
        SpecialCasterRequirementsErrorMessage = rawAbility.SpecialCasterRequirementsErrorMessage;
        SpecialInfo = rawAbility.SpecialInfo;
        SpecialTargetingTypeReq = rawAbility.SpecialTargetingTypeReq;
        Target = rawAbility.Target;
        TargetEffectKeywordReq = rawAbility.TargetEffectKeywordReq;
        TargetParticle = AbilityParticle.Parse(rawAbility.TargetParticle);
        TargetTypeTagReq = rawAbility.TargetTypeTagReq;
        UpgradeOf = rawAbility.UpgradeOf;
        WorksInCombat = rawAbility.WorksInCombat;
        WorksUnderwater = rawAbility.WorksUnderwater;
        WorksWhileFalling = rawAbility.WorksWhileFalling;
        WorksWhileMounted = rawAbility.WorksWhileMounted;
        WorksWhileStunned = rawAbility.WorksWhileStunned;
    }

    public string? AbilityGroup { get; set; }
    public string? AbilityGroupName { get; set; }
    public decimal? AmmoConsumeChance { get; set; }
    public string? AmmoDescription { get; set; }
    public Ammo[]? AmmoKeywords { get; set; }
    public decimal? AmmoStickChance { get; set; }
    public string? Animation { get; set; }
    public bool? AoEIsCenteredOnCaster { get; set; }
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
    public Cost[]? Costs { get; set; }
    public string? DamageType { get; set; }
    public bool? DelayLoopIsAbortedIfAttacked { get; set; }
    public bool? DelayLoopIsOnlyUsedInCombat { get; set; }
    public string? DelayLoopMessage { get; set; }
    public int? DelayLoopTime { get; set; }
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
    public string? ItemKeywordReqErrorMessage { get; set; }
    public string[]? ItemKeywordReqs { get; set; }
    public string[]? Keywords { get; set; }
    public int Level { get; set; }
    public string? Name { get; set; }
    public string? PetTypeTagReq { get; set; }
    public int? PetTypeTagReqMax { get; set; }
    public string? Prerequisite { get; set; }
    public string? Projectile { get; set; }
    public PvEAbility? PvE { get; set; }
    public int? Rank { get; set; }
    public decimal ResetTime { get; set; }
    public AbilityParticle? SelfParticle { get; set; }
    public AbilityParticle? SelfPreParticle { get; set; }
    public string? SharesResetTimerWith { get; set; }
    public string? Skill { get; set; }
    public Requirement[]? SpecialCasterRequirements { get; set; }
    public string? SpecialCasterRequirementsErrorMessage { get; set; }
    public string? SpecialInfo { get; set; }
    public int? SpecialTargetingTypeReq { get; set; }
    public string? Target { get; set; }
    public string? TargetEffectKeywordReq { get; set; }
    public AbilityParticle? TargetParticle { get; set; }
    public string? TargetTypeTagReq { get; set; }
    public string? UpgradeOf { get; set; }
    public bool? WorksInCombat { get; set; }
    public bool? WorksUnderwater { get; set; }
    public bool? WorksWhileFalling { get; set; }
    public bool? WorksWhileMounted { get; set; }
    public bool? WorksWhileStunned { get; set; }

    public RawAbility ToRawAbility()
    {
        RawAbility Result = new();

        Result.AbilityGroup = AbilityGroup;
        Result.AbilityGroupName = AbilityGroupName;
        Result.AmmoConsumeChance = AmmoConsumeChance;
        Result.AmmoDescription = AmmoDescription;
        Result.AmmoKeywords = AmmoKeywords;
        Result.AmmoStickChance = AmmoStickChance;
        Result.Animation = Animation;
        Result.AoEIsCenteredOnCaster = AoEIsCenteredOnCaster;
        Result.AttributesThatDeltaDelayLoopTime = AttributesThatDeltaDelayLoopTime;
        Result.AttributesThatDeltaPowerCost = AttributesThatDeltaPowerCost;
        Result.AttributesThatDeltaResetTime = AttributesThatDeltaResetTime;
        Result.AttributesThatDeltaWorksWhileStunned = AttributesThatDeltaWorksWhileStunned;
        Result.AttributesThatModAmmoConsumeChance = AttributesThatModAmmoConsumeChance;
        Result.AttributesThatModPowerCost = AttributesThatModPowerCost;
        Result.CanBeOnSidebar = CanBeOnSidebar;
        Result.CanSuppressMonsterShout = CanSuppressMonsterShout;
        Result.CanTargetUntargetableEnemies = CanTargetUntargetableEnemies;
        Result.CausesOfDeath = CausesOfDeath;
        Result.CombatRefreshBaseAmount = CombatRefreshBaseAmount;
        Result.Costs = Costs;
        Result.DamageType = DamageType;
        Result.DelayLoopIsAbortedIfAttacked = DelayLoopIsAbortedIfAttacked;
        Result.DelayLoopIsOnlyUsedInCombat = DelayLoopIsOnlyUsedInCombat;
        Result.DelayLoopMessage = DelayLoopMessage;
        Result.DelayLoopTime = DelayLoopTime;
        Result.Description = Description;
        Result.EffectKeywordsIndicatingEnabled = EffectKeywordsIndicatingEnabled;
        Result.ExtraKeywordsForTooltips = ExtraKeywordsForTooltips;
        Result.IconID = IconID;
        Result.IgnoreEffectErrors = IgnoreEffectErrors;
        Result.InternalAbility = InternalAbility;
        Result.InternalName = InternalName;
        Result.InventoryKeywordReqErrorMessage = InventoryKeywordReqErrorMessage;
        Result.InventoryKeywordReqs = InventoryKeywordReqs;
        Result.IsCosmeticPet = IsCosmeticPet;
        Result.IsHarmless = IsHarmless;
        Result.ItemKeywordReqErrorMessage = ItemKeywordReqErrorMessage;
        Result.ItemKeywordReqs = ItemKeywordReqs;
        Result.Keywords = Keywords;
        Result.Level = Level;
        Result.Name = Name;
        Result.PetTypeTagReq = PetTypeTagReq;
        Result.PetTypeTagReqMax = PetTypeTagReqMax;
        Result.Prerequisite = Prerequisite;

        Result.Projectile = IsProjectileNone ? "0" : Projectile;

        Result.PvE = PvE is null ? null : PvE.ToRawPvEAbility();
        Result.Rank = Rank is null ? null : Rank.ToString();
        Result.ResetTime = ResetTime;
        Result.SelfParticle = AbilityParticle.ToString(SelfParticle);
        Result.SelfPreParticle = AbilityParticle.ToString(SelfPreParticle);
        Result.SharesResetTimerWith = SharesResetTimerWith;
        Result.Skill = Skill;
        Result.SpecialCasterRequirements = Preprocessor.FromSingleOrMultiple(SpecialCasterRequirements, SpecialCasterRequirementsIsSingle);
        Result.SpecialCasterRequirementsErrorMessage = SpecialCasterRequirementsErrorMessage;
        Result.SpecialInfo = SpecialInfo;
        Result.SpecialTargetingTypeReq = SpecialTargetingTypeReq;
        Result.Target = Target;
        Result.TargetEffectKeywordReq = TargetEffectKeywordReq;
        Result.TargetParticle = AbilityParticle.ToString(TargetParticle);
        Result.TargetTypeTagReq = TargetTypeTagReq;
        Result.UpgradeOf = UpgradeOf;
        Result.WorksInCombat = WorksInCombat;
        Result.WorksUnderwater = WorksUnderwater;
        Result.WorksWhileFalling = WorksWhileFalling;
        Result.WorksWhileMounted = WorksWhileMounted;
        Result.WorksWhileStunned = WorksWhileStunned;

        return Result;
    }

    private readonly bool SpecialCasterRequirementsIsSingle;
    private readonly bool IsProjectileNone;
}
