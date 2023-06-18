namespace Preprocessor;

internal class Ability
{
    public Ability(Ability1 fromRawAbility1)
    {
        AbilityGroup = fromRawAbility1.AbilityGroup;
        AbilityGroupName = fromRawAbility1.AbilityGroupName;
        AmmoConsumeChance = fromRawAbility1.AmmoConsumeChance;
        AmmoDescription = fromRawAbility1.AmmoDescription;
        AmmoKeywords = fromRawAbility1.AmmoKeywords;
        AmmoStickChance = fromRawAbility1.AmmoStickChance;
        Animation = fromRawAbility1.Animation;
        AoEIsCenteredOnCaster = fromRawAbility1.AoEIsCenteredOnCaster;
        AttributesThatDeltaDelayLoopTime = fromRawAbility1.AttributesThatDeltaDelayLoopTime;
        AttributesThatDeltaPowerCost = fromRawAbility1.AttributesThatDeltaPowerCost;
        AttributesThatDeltaResetTime = fromRawAbility1.AttributesThatDeltaResetTime;
        AttributesThatDeltaWorksWhileStunned = fromRawAbility1.AttributesThatDeltaWorksWhileStunned;
        AttributesThatModAmmoConsumeChance = fromRawAbility1.AttributesThatModAmmoConsumeChance;
        AttributesThatModPowerCost = fromRawAbility1.AttributesThatModPowerCost;
        CanBeOnSidebar = fromRawAbility1.CanBeOnSidebar;
        CanSuppressMonsterShout = fromRawAbility1.CanSuppressMonsterShout;
        CanTargetUntargetableEnemies = fromRawAbility1.CanTargetUntargetableEnemies;
        CausesOfDeath = fromRawAbility1.CausesOfDeath;
        CombatRefreshBaseAmount = fromRawAbility1.CombatRefreshBaseAmount;
        Costs = fromRawAbility1.Costs;
        DamageType = fromRawAbility1.DamageType;
        DelayLoopIsAbortedIfAttacked = fromRawAbility1.DelayLoopIsAbortedIfAttacked;
        DelayLoopIsOnlyUsedInCombat = fromRawAbility1.DelayLoopIsOnlyUsedInCombat;
        DelayLoopMessage = fromRawAbility1.DelayLoopMessage;
        DelayLoopTime = fromRawAbility1.DelayLoopTime;
        Description = fromRawAbility1.Description;
        EffectKeywordsIndicatingEnabled = fromRawAbility1.EffectKeywordsIndicatingEnabled;
        ExtraKeywordsForTooltips = fromRawAbility1.ExtraKeywordsForTooltips;
        IconID = fromRawAbility1.IconID;
        IgnoreEffectErrors = fromRawAbility1.IgnoreEffectErrors;
        InternalAbility = fromRawAbility1.InternalAbility;
        InternalName = fromRawAbility1.InternalName;
        InventoryKeywordReqErrorMessage = fromRawAbility1.InventoryKeywordReqErrorMessage;
        InventoryKeywordReqs = fromRawAbility1.InventoryKeywordReqs;
        IsCosmeticPet = fromRawAbility1.IsCosmeticPet;
        IsHarmless = fromRawAbility1.IsHarmless;
        ItemKeywordReqErrorMessage = fromRawAbility1.ItemKeywordReqErrorMessage;
        ItemKeywordReqs = fromRawAbility1.ItemKeywordReqs;
        Keywords = fromRawAbility1.Keywords;
        Level = fromRawAbility1.Level;
        Name = fromRawAbility1.Name;
        PetTypeTagReq = fromRawAbility1.PetTypeTagReq;
        PetTypeTagReqMax = fromRawAbility1.PetTypeTagReqMax;
        Prerequisite = fromRawAbility1.Prerequisite;
        Projectile = fromRawAbility1.Projectile;
        PvE = fromRawAbility1.PvE is null ? null : new PvEAbility(fromRawAbility1.PvE);
        Rank = fromRawAbility1.Rank is null ? null : int.Parse(fromRawAbility1.Rank);
        ResetTime = fromRawAbility1.ResetTime;
        SelfParticle = fromRawAbility1.SelfParticle;
        SelfPreParticle = fromRawAbility1.SelfPreParticle;
        SharesResetTimerWith = fromRawAbility1.SharesResetTimerWith;
        Skill = fromRawAbility1.Skill;
        SpecialCasterRequirements = Preprocessor.ToSingleOrMultiple<Requirement>(fromRawAbility1.SpecialCasterRequirements, out SpecialCasterRequirementsIsSingle);
        SpecialCasterRequirementsErrorMessage = fromRawAbility1.SpecialCasterRequirementsErrorMessage;
        SpecialInfo = fromRawAbility1.SpecialInfo;
        SpecialTargetingTypeReq = fromRawAbility1.SpecialTargetingTypeReq;
        Target = fromRawAbility1.Target;
        TargetEffectKeywordReq = fromRawAbility1.TargetEffectKeywordReq;
        TargetParticle = fromRawAbility1.TargetParticle;
        TargetTypeTagReq = fromRawAbility1.TargetTypeTagReq;
        UpgradeOf = fromRawAbility1.UpgradeOf;
        WorksInCombat = fromRawAbility1.WorksInCombat;
        WorksUnderwater = fromRawAbility1.WorksUnderwater;
        WorksWhileFalling = fromRawAbility1.WorksWhileFalling;
        WorksWhileMounted = fromRawAbility1.WorksWhileMounted;
        WorksWhileStunned = fromRawAbility1.WorksWhileStunned;
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

    public Ability1 ToRawAbility1()
    {
        Ability1 Result = new();

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
        Result.PvE = PvE is null ? null : PvE.ToRawPvEAbility1();
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
