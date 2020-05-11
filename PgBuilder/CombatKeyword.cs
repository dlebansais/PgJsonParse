﻿namespace PgBuilder
{
    public enum CombatKeyword
    {
        None,
        Ignore,
        DamageBoost,
        DamageBoostDouble,
        DealDirectHealthDamage,
        DealIndirectDamage,
        DealArmorDamage,
        NextAttack,
        SameTarget,
        TargetSelf,
        UntilAttacked,
        NonStackingDebuff,
        StackingDebuffLimit,
        MaxStack,
        Combo1,
        Combo2,
        Combo3,
        Combo4,
        Combo5,
        Combo6,
        Combo7,
        ComboFinalStepBurst,
        ComboFinalStepDamage,
        ComboFinalStepDamageAndStun,
        ComboFinalStepBoostBaseDamage,
        ReflectOnAnyAttack,
        ReflectOnMelee,
        ReflectOnBurst,
        ReflectOnRanged,
        ReflectKnockbackOnFirstMelee,
        ActiveSkill,
        NotAttackedRecently,
        LessThanHalfMaxHealth,
        SpiderPetAvoidBurst,
        CombatRefreshRestoreHeatlth,
        DamageBoostAgainstSpecie,
        DamageBoostToHealthAndArmor,
        BaseDamageBoost,
        DrainHealth,
        DrainArmor,
        DrainHealthMax,
        DrainArmorMax,
        ReflectMeleeIndirectDamage,
        MaxOccurence,
        EffectDuration,
        EffectDurationMinute,
        EffectDelay,
        EffectRecurrence,
        AddRage,
        ZeroRage,
        IncreaseMaxRage,
        AddPowerCost,
        AddPowerCostMax,
        AddHealthRegen,
        AddArmorRegen,
        AddPowerRegen,
        AddMaxHealth,
        AddMaxArmor,
        AddResetTimer,
        AddCombatRefreshTimer,
        AddTaunt,
        ZeroTaunt,
        ChangeTaunt,
        ShuffleTaunt,
        Ignored,
        BelowArmor,
        AboveRage,
        RestoreHealth,
        RestoreMaxHealth,
        RestoreArmor,
        RestorePower,
        RestoreHealthArmor,
        RestoreHealthArmorPower,
        RestoreAny,
        RestoreHealthArmorToPet,
        RestoreBodyHeat,
        ZeroPowerCost,
        AddSprintSpeed,
        AddFlySpeed,
        AddSwimSpeed,
        AddOutOfCombatSpeed,
        Slow,
        ApplyToNecroPet,
        ApplyToDeerPet,
        ApplyToSpiderPet,
        ApplyToPet,
        ApplyToPetAndMaster,
        AddRange,
        Stun,
        Concussion,
        StunIncorporeal,
        SelfStun,
        StunImmunity,
        SlowRootImmunity,
        RemoveStun,
        RemoveSlowRoot,
        SelfImmolation,
        PetImmolation,
        Knockback,
        ResetOtherAbilityTimer,
        EnableOtherAbility,
        VariableMitigation,
        ApplyToAbilitiesShield,
        ApplyToAllies,
        AddEvasionBurst,
        AddEvasionProjectile,
        AddEvasionMelee,
        AddMitigation,
        AddMitigationUniversal,
        AddMitigationPhysical,
        AddMitigationInternal,
        AddMitigationDirect,
        AddMitigationIndirect,
        MitigationLimit,
        AddProtectionCold,
        AddChanceToKnockdown,
        AddChanceToIgnoreKnockback,
        AddChanceToIgnoreStun,
        ApplyWithChance,
        RequireTwoKnives,
        RequireNoAggro,
        ApplyToMeleeReflect,
        ChanceToConsume,
        UntilTrigger,
        AnimalPetAttackBoost,
        AnimalPetRageAttackBoost,
        RandomDamage,
        IfDamageType,
        IfWerewolf,
        TargetSubsequentAttacks,
        TargetNextRageAttack,
        NextUse,
        WithinDistance,
        TargetAnatomyArthropods,
        TargetElite,
        TargetNotElite,
        TargetUndead,
        TargetAnatomyAbberation,
        TargetVulnerable,
        TargetSentient,
        TargetUnderEffect,
        TargetKnockedDown,
        AddAccuracy,
        AddMeleeAccuracy,
        RemoveEvasion,
        AddProjectileEvasion,
        AddMeleeEvasion,
        AddPhysicalReflection,
        AddElementalDamageResistance,
        AddDamageResistance,
        Fear,
        FearSentient,
        SetVulnerable,
        ExtraTraumaDamage,
        AddVulnerability,
        AddIndirectVulnerability,
        AddDirectVulnerability,
        DestroyedByDamageType,
        ChangeDamageType,
        IgnoreKnockback,
        IncreaseXPGain,
        MaxKillTime,
        RemoveEffects,
        TargetAbilityBoost,
        NoYellForHelp,
        ToKickerTarget,
        RepairBrokenBone,
        AddEnthusiasm,
        AddDeathAvoidance,
        DamageOverTime,
        SecondBlast,
        NoDispel,
        IgnoreArmor,
        ReturnDamage,
        BoostJumpHeight,
        RepeatedCasting,
        FreeMovementLeaping,
        MendBrokenBone,
        ReduceCriticalChance,
        AnotherTrap,
        AddChannelingTime,
        Again,
        OnEvade,
        OnEvadeMelee,
        MitigateReflect,
        MitigateReflectKick,
        ReflectRate,
        ThickArmor,
        DrainAsArmor,
        WhenTeleporting,
        But,
    }
}
