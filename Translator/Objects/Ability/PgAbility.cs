namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAbility : PgObject
    {
        public string? AbilityGroup_Key { get; set; }
        public int BoolValues { get; set; }
        public string AbilityGroupName { get; set; } = string.Empty;
        public AbilityAnimation Animation { get; set; }
        public const int AoEIsCenteredOnCasterNotNull = 1 << 28;
        public const int AoEIsCenteredOnCasterIsTrue = 1 << 29;
        public bool AoEIsCenteredOnCaster { get { return (BoolValues & (AoEIsCenteredOnCasterNotNull + AoEIsCenteredOnCasterIsTrue)) == (AoEIsCenteredOnCasterNotNull + AoEIsCenteredOnCasterIsTrue); } }
        public bool? RawAoEIsCenteredOnCaster { get { return ((BoolValues & AoEIsCenteredOnCasterNotNull) != 0) ? (BoolValues & AoEIsCenteredOnCasterIsTrue) != 0 : null; } }
        public void SetAoEIsCenteredOnCaster(bool value) { BoolValues |= (BoolValues & ~(AoEIsCenteredOnCasterNotNull + AoEIsCenteredOnCasterIsTrue)) | ((value ? AoEIsCenteredOnCasterIsTrue : 0) + AoEIsCenteredOnCasterNotNull); }
        public float AoERange { get { return RawAoERange.HasValue ? RawAoERange.Value : 0; } }
        public float? RawAoERange { get; set; }
        public PgAttributeCollection AttributesThatModAmmoConsumeChanceList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaDelayLoopTimeList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaPowerCostList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaResetTimeList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaWorksWhileStunnedList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModPowerCostList { get; set; } = new PgAttributeCollection();
        public const int CanBeOnSidebarNotNull = 1 << 0;
        public const int CanBeOnSidebarIsTrue = 1 << 1;
        public bool CanBeOnSidebar { get { return (BoolValues & (CanBeOnSidebarNotNull + CanBeOnSidebarIsTrue)) == (CanBeOnSidebarNotNull + CanBeOnSidebarIsTrue); } }
        public bool? RawCanBeOnSidebar { get { return ((BoolValues & CanBeOnSidebarNotNull) != 0) ? (BoolValues & CanBeOnSidebarIsTrue) != 0 : null; } }
        public void SetCanBeOnSidebar(bool value) { BoolValues |= (BoolValues & ~(CanBeOnSidebarNotNull + CanBeOnSidebarIsTrue)) | ((value ? CanBeOnSidebarIsTrue : 0) + CanBeOnSidebarNotNull); }
        public const int CanSuppressMonsterShoutNotNull = 1 << 2;
        public const int CanSuppressMonsterShoutIsTrue = 1 << 3;
        public bool CanSuppressMonsterShout { get { return (BoolValues & (CanSuppressMonsterShoutNotNull + CanSuppressMonsterShoutIsTrue)) == (CanSuppressMonsterShoutNotNull + CanSuppressMonsterShoutIsTrue); } }
        public bool? RawCanSuppressMonsterShout { get { return ((BoolValues & CanSuppressMonsterShoutNotNull) != 0) ? (BoolValues & CanSuppressMonsterShoutIsTrue) != 0 : null; } }
        public void SetCanSuppressMonsterShout(bool value) { BoolValues |= (BoolValues & ~(CanSuppressMonsterShoutNotNull + CanSuppressMonsterShoutIsTrue)) | ((value ? CanSuppressMonsterShoutIsTrue : 0) + CanSuppressMonsterShoutNotNull); }
        public const int CanTargetUntargetableEnemiesNotNull = 1 << 4;
        public const int CanTargetUntargetableEnemiesIsTrue = 1 << 5;
        public bool CanTargetUntargetableEnemies { get { return (BoolValues & (CanTargetUntargetableEnemiesNotNull + CanTargetUntargetableEnemiesIsTrue)) == (CanTargetUntargetableEnemiesNotNull + CanTargetUntargetableEnemiesIsTrue); } }
        public bool? RawCanTargetUntargetableEnemies { get { return ((BoolValues & CanTargetUntargetableEnemiesNotNull) != 0) ? (BoolValues & CanTargetUntargetableEnemiesIsTrue) != 0 : null; } }
        public void SetCanTargetUntargetableEnemies(bool value) { BoolValues |= (BoolValues & ~(CanTargetUntargetableEnemiesNotNull + CanTargetUntargetableEnemiesIsTrue)) | ((value ? CanTargetUntargetableEnemiesIsTrue : 0) + CanTargetUntargetableEnemiesNotNull); }
        public List<Deaths> CausesOfDeathList { get; set; } = new List<Deaths>();
        public PgRecipeCost? Cost { get; set; }
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get; set; }
        public DamageType DamageType { get; set; }
        public const int DelayLoopIsAbortedIfAttackedNotNull = 1 << 6;
        public const int DelayLoopIsAbortedIfAttackedIsTrue = 1 << 7;
        public bool DelayLoopIsAbortedIfAttacked { get { return (BoolValues & (DelayLoopIsAbortedIfAttackedNotNull + DelayLoopIsAbortedIfAttackedIsTrue)) == (DelayLoopIsAbortedIfAttackedNotNull + DelayLoopIsAbortedIfAttackedIsTrue); } }
        public bool? RawDelayLoopIsAbortedIfAttacked { get { return ((BoolValues & DelayLoopIsAbortedIfAttackedNotNull) != 0) ? (BoolValues & DelayLoopIsAbortedIfAttackedIsTrue) != 0 : null; } }
        public void SetDelayLoopIsAbortedIfAttacked(bool value) { BoolValues |= (BoolValues & ~(DelayLoopIsAbortedIfAttackedNotNull + DelayLoopIsAbortedIfAttackedIsTrue)) | ((value ? DelayLoopIsAbortedIfAttackedIsTrue : 0) + DelayLoopIsAbortedIfAttackedNotNull); }
        public string DelayLoopMessage { get; set; } = string.Empty;
        public float DelayLoopTime { get { return RawDelayLoopTime.HasValue ? RawDelayLoopTime.Value : 0; } }
        public float? RawDelayLoopTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get; set; }
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get; set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public const int IgnoreEffectErrorsNotNull = 1 << 8;
        public const int IgnoreEffectErrorsIsTrue = 1 << 9;
        public bool IgnoreEffectErrors { get { return (BoolValues & (IgnoreEffectErrorsNotNull + IgnoreEffectErrorsIsTrue)) == (IgnoreEffectErrorsNotNull + IgnoreEffectErrorsIsTrue); } }
        public bool? RawIgnoreEffectErrors { get { return ((BoolValues & IgnoreEffectErrorsNotNull) != 0) ? (BoolValues & IgnoreEffectErrorsIsTrue) != 0 : null; } }
        public void SetIgnoreEffectErrors(bool value) { BoolValues |= (BoolValues & ~(IgnoreEffectErrorsNotNull + IgnoreEffectErrorsIsTrue)) | ((value ? IgnoreEffectErrorsIsTrue : 0) + IgnoreEffectErrorsNotNull); }
        public const int InternalAbilityNotNull = 1 << 10;
        public const int InternalAbilityIsTrue = 1 << 11;
        public bool InternalAbility { get { return (BoolValues & (InternalAbilityNotNull + InternalAbilityIsTrue)) == (InternalAbilityNotNull + InternalAbilityIsTrue); } }
        public bool? RawInternalAbility { get { return ((BoolValues & InternalAbilityNotNull) != 0) ? (BoolValues & InternalAbilityIsTrue) != 0 : null; } }
        public void SetInternalAbility(bool value) { BoolValues |= (BoolValues & ~(InternalAbilityNotNull + InternalAbilityIsTrue)) | ((value ? InternalAbilityIsTrue : 0) + InternalAbilityNotNull); }
        public string InternalName { get; set; } = string.Empty;
        public const int IsHarmlessNotNull = 1 << 12;
        public const int IsHarmlessIsTrue = 1 << 13;
        public bool IsHarmless { get { return (BoolValues & (IsHarmlessNotNull + IsHarmlessIsTrue)) == (IsHarmlessNotNull + IsHarmlessIsTrue); } }
        public bool? RawIsHarmless { get { return ((BoolValues & IsHarmlessNotNull) != 0) ? (BoolValues & IsHarmlessIsTrue) != 0 : null; } }
        public void SetIsHarmless(bool value) { BoolValues |= (BoolValues & ~(IsHarmlessNotNull + IsHarmlessIsTrue)) | ((value ? IsHarmlessIsTrue : 0) + IsHarmlessNotNull); }
        public string ItemKeywordReqErrorMessage { get; set; } = string.Empty;
        public List<AbilityItemKeyword> ItemKeywordReqList { get; set; } = new List<AbilityItemKeyword>();
        public List<AbilityKeyword> KeywordList { get; set; } = new List<AbilityKeyword>();
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public string Name { get; set; } = string.Empty;
        public AbilityPetType PetTypeTagReq { get; set; }
        public int PetTypeTagReqMax { get { return RawPetTypeTagReqMax.HasValue ? RawPetTypeTagReqMax.Value : 0; } }
        public int? RawPetTypeTagReqMax { get; set; }
        public string? Prerequisite_Key { get; set; }
        public AbilityProjectile Projectile { get; set; }
        public PgAbilityPvX PvE { get; set; } = null!;
        public PgAbilityPvX? PvP { get; set; }
        public float ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : 0; } }
        public float? RawResetTime { get; set; }
        public PgSelfParticle SelfParticle { get; set; } = null!;
        public PgSelfPreParticle SelfPreParticle { get; set; } = null!;
        public uint AbilityTargetParticleColor0 { get { return RawAbilityTargetParticleColor0.HasValue ? RawAbilityTargetParticleColor0.Value : 0; } }
        public uint? RawAbilityTargetParticleColor0 { get; set; }
        public uint AbilityTargetParticleColor1 { get { return RawAbilityTargetParticleColor1.HasValue ? RawAbilityTargetParticleColor1.Value : 0; } }
        public uint? RawAbilityTargetParticleColor1 { get; set; }
        public string AmmoDescription { get; set; } = string.Empty;
        public string? SharesResetTimerWith_Key { get; set; }
        public string? Skill_Key { get; set; }
        public PgAbilityRequirementCollection SpecialCasterRequirementList { get; set; } = new PgAbilityRequirementCollection();
        public string SpecialCasterRequirementsErrorMessage { get; set; } = string.Empty;
        public string SpecialInfo { get; set; } = string.Empty;
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        public int? RawSpecialTargetingTypeReq { get; set; }
        public AbilityTarget Target { get; set; }
        public TargetEffectKeyword TargetEffectKeywordReq { get; set; }
        public PgTargetParticle? TargetParticle { get; set; } = null!;
        public string? UpgradeOf_Key { get; set; }
        public const int WorksInCombatNotNull = 1 << 14;
        public const int WorksInCombatIsTrue = 1 << 15;
        public bool WorksInCombat { get { return (BoolValues & (WorksInCombatNotNull + WorksInCombatIsTrue)) == (WorksInCombatNotNull + WorksInCombatIsTrue); } }
        public bool? RawWorksInCombat { get { return ((BoolValues & WorksInCombatNotNull) != 0) ? (BoolValues & WorksInCombatIsTrue) != 0 : null; } }
        public void SetWorksInCombat(bool value) { BoolValues |= (BoolValues & ~(WorksInCombatNotNull + WorksInCombatIsTrue)) | ((value ? WorksInCombatIsTrue : 0) + WorksInCombatNotNull); }
        public const int WorksUnderwaterNotNull = 1 << 16;
        public const int WorksUnderwaterIsTrue = 1 << 17;
        public bool WorksUnderwater { get { return (BoolValues & (WorksUnderwaterNotNull + WorksUnderwaterIsTrue)) == (WorksUnderwaterNotNull + WorksUnderwaterIsTrue); } }
        public bool? RawWorksUnderwater { get { return ((BoolValues & WorksUnderwaterNotNull) != 0) ? (BoolValues & WorksUnderwaterIsTrue) != 0 : null; } }
        public void SetWorksUnderwater(bool value) { BoolValues |= (BoolValues & ~(WorksUnderwaterNotNull + WorksUnderwaterIsTrue)) | ((value ? WorksUnderwaterIsTrue : 0) + WorksUnderwaterNotNull); }
        public const int WorksWhileFallingNotNull = 1 << 18;
        public const int WorksWhileFallingIsTrue = 1 << 19;
        public bool WorksWhileFalling { get { return (BoolValues & (WorksWhileFallingNotNull + WorksWhileFallingIsTrue)) == (WorksWhileFallingNotNull + WorksWhileFallingIsTrue); } }
        public bool? RawWorksWhileFalling { get { return ((BoolValues & WorksWhileFallingNotNull) != 0) ? (BoolValues & WorksWhileFallingIsTrue) != 0 : null; } }
        public void SetWorksWhileFalling(bool value) { BoolValues |= (BoolValues & ~(WorksWhileFallingNotNull + WorksWhileFallingIsTrue)) | ((value ? WorksWhileFallingIsTrue : 0) + WorksWhileFallingNotNull); }
        public const int DelayLoopIsOnlyUsedInCombatNotNull = 1 << 20;
        public const int DelayLoopIsOnlyUsedInCombatIsTrue = 1 << 21;
        public bool DelayLoopIsOnlyUsedInCombat { get { return (BoolValues & (DelayLoopIsOnlyUsedInCombatNotNull + DelayLoopIsOnlyUsedInCombatIsTrue)) == (DelayLoopIsOnlyUsedInCombatNotNull + DelayLoopIsOnlyUsedInCombatIsTrue); } }
        public bool? RawDelayLoopIsOnlyUsedInCombat { get { return ((BoolValues & DelayLoopIsOnlyUsedInCombatNotNull) != 0) ? (BoolValues & DelayLoopIsOnlyUsedInCombatIsTrue) != 0 : null; } }
        public void SetDelayLoopIsOnlyUsedInCombat(bool value) { BoolValues |= (BoolValues & ~(DelayLoopIsOnlyUsedInCombatNotNull + DelayLoopIsOnlyUsedInCombatIsTrue)) | ((value ? DelayLoopIsOnlyUsedInCombatIsTrue : 0) + DelayLoopIsOnlyUsedInCombatNotNull); }
        public PgAbilityAmmoCollection AmmoKeywordList { get; set; } = new PgAbilityAmmoCollection();
        public float AmmoConsumeChance { get { return RawAmmoConsumeChance.HasValue ? RawAmmoConsumeChance.Value : 0; } }
        public float? RawAmmoConsumeChance { get; set; }
        public float AmmoStickChance { get { return RawAmmoStickChance.HasValue ? RawAmmoStickChance.Value : 0; } }
        public float? RawAmmoStickChance { get; set; }
        public PgSourceCollection SourceList { get; set; } = new PgSourceCollection();
        public string DigitStrippedName { get; set; } = string.Empty;
        public Dictionary<CombatKeyword, string> AssociatedEffectKeyTable { get; set; } = new Dictionary<CombatKeyword, string>();
        public string? TargetTypeTagReq_Key { get; set; }
        public const int WorksWhileMountedNotNull = 1 << 22;
        public const int WorksWhileMountedIsTrue = 1 << 23;
        public bool WorksWhileMounted { get { return (BoolValues & (WorksWhileMountedNotNull + WorksWhileMountedIsTrue)) == (WorksWhileMountedNotNull + WorksWhileMountedIsTrue); } }
        public bool? RawWorksWhileMounted { get { return ((BoolValues & WorksWhileMountedNotNull) != 0) ? (BoolValues & WorksWhileMountedIsTrue) != 0 : null; } }
        public void SetWorksWhileMounted(bool value) { BoolValues |= (BoolValues & ~(WorksWhileMountedNotNull + WorksWhileMountedIsTrue)) | ((value ? WorksWhileMountedIsTrue : 0) + WorksWhileMountedNotNull); }
        public const int IsCosmeticPetNotNull = 1 << 24;
        public const int IsCosmeticPetIsTrue = 1 << 25;
        public bool IsCosmeticPet { get { return (BoolValues & (IsCosmeticPetNotNull + IsCosmeticPetIsTrue)) == (IsCosmeticPetNotNull + IsCosmeticPetIsTrue); } }
        public bool? RawIsCosmeticPet { get { return ((BoolValues & IsCosmeticPetNotNull) != 0) ? (BoolValues & IsCosmeticPetIsTrue) != 0 : null; } }
        public void SetIsCosmeticPet(bool value) { BoolValues |= (BoolValues & ~(IsCosmeticPetNotNull + IsCosmeticPetIsTrue)) | ((value ? IsCosmeticPetIsTrue : 0) + IsCosmeticPetNotNull); }
        public const int WorksWhileStunnedNotNull = 1 << 26;
        public const int WorksWhileStunnedIsTrue = 1 << 27;
        public bool WorksWhileStunned { get { return (BoolValues & (WorksWhileStunnedNotNull + WorksWhileStunnedIsTrue)) == (WorksWhileStunnedNotNull + WorksWhileStunnedIsTrue); } }
        public bool? RawWorksWhileStunned { get { return ((BoolValues & WorksWhileStunnedNotNull) != 0) ? (BoolValues & WorksWhileStunnedIsTrue) != 0 : null; } }
        public void SetWorksWhileStunned(bool value) { BoolValues |= (BoolValues & ~(WorksWhileStunnedNotNull + WorksWhileStunnedIsTrue)) | ((value ? WorksWhileStunnedIsTrue : 0) + WorksWhileStunnedNotNull); }
        public int Rank { get { return RawRank.HasValue ? RawRank.Value : 0; } }
        public int? RawRank { get; set; }
        public string InventoryKeywordReqErrorMessage { get; set; } = string.Empty;
        public List<AbilityItemKeyword> InventoryKeywordReqList { get; set; } = new List<AbilityItemKeyword>();

        public int FriendlyIconId { get; set; }

        public override int ObjectIconId { get { return FriendlyIconId; } }
        public override string ObjectName { get { return Name; } }
        public override string ToString() { return Name; }
    }
}
