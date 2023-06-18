namespace Preprocessor;

internal class Ability
{
    public Ability(RawAbility fromRawAbility)
    {
        AbilityGroup = fromRawAbility.AbilityGroup;
        AbilityGroupName = fromRawAbility.AbilityGroupName;
        AmmoConsumeChance = fromRawAbility.AmmoConsumeChance;
        AmmoDescription = fromRawAbility.AmmoDescription;
        AmmoKeywords = fromRawAbility.AmmoKeywords;
        AmmoStickChance = fromRawAbility.AmmoStickChance;
        Animation = fromRawAbility.Animation;
        AoEIsCenteredOnCaster = fromRawAbility.AoEIsCenteredOnCaster;
        AttributesThatDeltaDelayLoopTime = fromRawAbility.AttributesThatDeltaDelayLoopTime;
        AttributesThatDeltaPowerCost = fromRawAbility.AttributesThatDeltaPowerCost;
        AttributesThatDeltaResetTime = fromRawAbility.AttributesThatDeltaResetTime;
        AttributesThatDeltaWorksWhileStunned = fromRawAbility.AttributesThatDeltaWorksWhileStunned;
        AttributesThatModAmmoConsumeChance = fromRawAbility.AttributesThatModAmmoConsumeChance;
        AttributesThatModPowerCost = fromRawAbility.AttributesThatModPowerCost;
        CanBeOnSidebar = fromRawAbility.CanBeOnSidebar;
        CanSuppressMonsterShout = fromRawAbility.CanSuppressMonsterShout;
        CanTargetUntargetableEnemies = fromRawAbility.CanTargetUntargetableEnemies;
        CausesOfDeath = fromRawAbility.CausesOfDeath;
        CombatRefreshBaseAmount = fromRawAbility.CombatRefreshBaseAmount;
        Costs = fromRawAbility.Costs;
        DamageType = fromRawAbility.DamageType;
        DelayLoopIsAbortedIfAttacked = fromRawAbility.DelayLoopIsAbortedIfAttacked;
        DelayLoopIsOnlyUsedInCombat = fromRawAbility.DelayLoopIsOnlyUsedInCombat;
        DelayLoopMessage = fromRawAbility.DelayLoopMessage;
        DelayLoopTime = fromRawAbility.DelayLoopTime;
        Description = fromRawAbility.Description;
        EffectKeywordsIndicatingEnabled = fromRawAbility.EffectKeywordsIndicatingEnabled;
        ExtraKeywordsForTooltips = fromRawAbility.ExtraKeywordsForTooltips;
        IconID = fromRawAbility.IconID;
        IgnoreEffectErrors = fromRawAbility.IgnoreEffectErrors;
        InternalAbility = fromRawAbility.InternalAbility;
        InternalName = fromRawAbility.InternalName;
        InventoryKeywordReqErrorMessage = fromRawAbility.InventoryKeywordReqErrorMessage;
        InventoryKeywordReqs = fromRawAbility.InventoryKeywordReqs;
        IsCosmeticPet = fromRawAbility.IsCosmeticPet;
        IsHarmless = fromRawAbility.IsHarmless;
        ItemKeywordReqErrorMessage = fromRawAbility.ItemKeywordReqErrorMessage;
        ItemKeywordReqs = fromRawAbility.ItemKeywordReqs;
        Keywords = fromRawAbility.Keywords;
        Level = fromRawAbility.Level;
        Name = fromRawAbility.Name;
        PetTypeTagReq = fromRawAbility.PetTypeTagReq;
        PetTypeTagReqMax = fromRawAbility.PetTypeTagReqMax;
        Prerequisite = fromRawAbility.Prerequisite;
        Projectile = fromRawAbility.Projectile;
        PvE = fromRawAbility.PvE is null ? null : new PvEAbility(fromRawAbility.PvE);
        Rank = fromRawAbility.Rank is null ? null : int.Parse(fromRawAbility.Rank);
        ResetTime = fromRawAbility.ResetTime;
        SelfParticle = fromRawAbility.SelfParticle;
        SelfPreParticle = fromRawAbility.SelfPreParticle;
        SharesResetTimerWith = fromRawAbility.SharesResetTimerWith;
        Skill = fromRawAbility.Skill;
        SpecialCasterRequirements = Preprocessor.ToSingleOrMultiple<Requirement>(fromRawAbility.SpecialCasterRequirements, out SpecialCasterRequirementsIsSingle);
        SpecialCasterRequirementsErrorMessage = fromRawAbility.SpecialCasterRequirementsErrorMessage;
        SpecialInfo = fromRawAbility.SpecialInfo;
        SpecialTargetingTypeReq = fromRawAbility.SpecialTargetingTypeReq;
        Target = fromRawAbility.Target;
        TargetEffectKeywordReq = fromRawAbility.TargetEffectKeywordReq;
        TargetParticle = fromRawAbility.TargetParticle;
        TargetTypeTagReq = fromRawAbility.TargetTypeTagReq;
        UpgradeOf = fromRawAbility.UpgradeOf;
        WorksInCombat = fromRawAbility.WorksInCombat;
        WorksUnderwater = fromRawAbility.WorksUnderwater;
        WorksWhileFalling = fromRawAbility.WorksWhileFalling;
        WorksWhileMounted = fromRawAbility.WorksWhileMounted;
        WorksWhileStunned = fromRawAbility.WorksWhileStunned;
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
    public string? SelfParticle { get; set; }
    public string? SelfPreParticle { get; set; }
    public string? SharesResetTimerWith { get; set; }
    public string? Skill { get; set; }
    public Requirement[]? SpecialCasterRequirements { get; set; }
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
        Result.Projectile = Projectile;
        Result.PvE = PvE is null ? null : PvE.ToRawPvEAbility();
        Result.Rank = Rank is null ? null : Rank.ToString();
        Result.ResetTime = ResetTime;
        Result.SelfParticle = SelfParticle;
        Result.SelfPreParticle = SelfPreParticle;
        Result.SharesResetTimerWith = SharesResetTimerWith;
        Result.Skill = Skill;
        Result.SpecialCasterRequirements = Preprocessor.FromSingleOrMultiple(SpecialCasterRequirements, SpecialCasterRequirementsIsSingle);
        Result.SpecialCasterRequirementsErrorMessage = SpecialCasterRequirementsErrorMessage;
        Result.SpecialInfo = SpecialInfo;
        Result.SpecialTargetingTypeReq = SpecialTargetingTypeReq;
        Result.Target = Target;
        Result.TargetEffectKeywordReq = TargetEffectKeywordReq;
        Result.TargetParticle = TargetParticle;
        Result.TargetTypeTagReq = TargetTypeTagReq;
        Result.UpgradeOf = UpgradeOf;
        Result.WorksInCombat = WorksInCombat;
        Result.WorksUnderwater = WorksUnderwater;
        Result.WorksWhileFalling = WorksWhileFalling;
        Result.WorksWhileMounted = WorksWhileMounted;
        Result.WorksWhileStunned = WorksWhileStunned;

        return Result;
    }

    private bool SpecialCasterRequirementsIsSingle;
}
