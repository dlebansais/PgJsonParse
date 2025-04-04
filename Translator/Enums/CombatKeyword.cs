﻿namespace PgObjects
{
    public enum CombatKeyword
    {
        Internal_None,
        Ignore,
        DamageBoost,
        DirectOnlyDamageBoost,
        DamageBoostDouble,
        DealDirectHealthDamage,
        DealIndirectDamage,
        DealArmorDamage,
        NextAttack,
        MeleeAttack,
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
        Combo8,
        ComboFinalStepBurst,
        ComboFinalStepDamage,
        ComboFinalStepDamageAndStun,

        // ComboFinalStepBoostBaseDamage,
        ReflectOnAnyAttack,
        ReflectOnMelee,
        ReflectOnBurst,
        ReflectOnRanged,
        ReflectKnockbackOnFirstMelee,
        ActiveSkill,
        ActiveAbility,
        NotAttackedRecently,

        // LessThanHalfMaxHealth,
        // SpiderPetAvoidBurst,
        CombatRefreshRestoreHeatlth,
        DamageBoostAgainstSpecie,
        DamageBoostToHealthAndArmor,
        BaseDamageBoost,
        DrainHealth,
        //DrainArmor,
        DrainHealthMax,
        //DrainArmorMax,
        ReflectMeleeIndirectDamage,

        // MaxOccurence,
        EffectDuration,
        EffectDurationMinute,
        EffectDelay,
        EffectRecurrence,
        AddRage,
        ZeroRage,
        IncreaseMaxRage,
        AddPowerCost,
        AddPowerCostMax,

        // AddHealthRegen,
        AddArmorRegen,
        AddPowerRegen,
        AddMaxHealth,
        AddMaxArmor,
        AddMaxPower,
        AddResetTimer,
        AddCombatRefreshTimer,
        AddTaunt,
        ZeroTaunt,
        ChangeTaunt,
        ShuffleTaunt,
        Ignored,
        BelowArmor,
        BelowMaxRage,
        AboveRage,
        RestoreHealth,
        IncreaseHealEfficiency,
        RegenPercentageOfArmor,

        // RestoreMaxHealth,
        RestoreArmor,
        RestorePower,
        RestoreHealthArmor,
        RestoreHealthArmorPower,

        // RestoreAny,
        //RestoreHealthArmorToPet,
        RestoreBodyHeat,
        ZeroPowerCost,
        AddSprintSpeed,
        AddSprintPowerCost,
        AddFlySpeed,
        AddSwimSpeed,
        AddOutOfCombatSpeed,
        Slow,

        // ApplyToNecroPet,
        // ApplyToDeerPet,
        // ApplyToSpiderPet,
        ApplyToPet,
        ApplyToPetAndMaster,
        AddRange,
        AddAoERange,
        Stun,
        StunnedTarget,
        StunnedSelf,
        Concussion,
        StunIncorporeal,
        StunDebuffed,
        SelfStun,
        StunImmunity,
        SlowRootImmunity,
        RemoveStun,
        RemoveSlowRoot,
        UsableWhileStunned,

        // SelfImmolation,
        PetImmolation,
        Knockback,
        ResetOtherAbilityTimer,

        // EnableOtherAbility,
        VariableMitigation,

        // ApplyToAbilitiesShield,
        ApplyToAllies,
        AddEvasionBurst,
        AddEvasionProjectile,
        AddEvasionMelee,
        AddEvasionRanged,
        AddMitigation,

        // AddMitigationUniversal,
        AddMitigationPhysical,
        AddMitigationInternal,
        AddMitigationDirect,
        AddMitigationIndirect,
        ImmunityDirect,
        MitigationLimit,
        DebuffMitigation,
        AddProtectionCold,
        AddChanceToKnockdown,
        AddChanceToIgnoreKnockback,
        AddChanceToIgnoreStun,
        ApplyWithChance,
        RequireTwoKnives,
        RequireNoAggro,
        ChanceToConsume,
        UntilTrigger,

        // AnimalPetAttackBoost,
        AnimalPetRageAttackBoost,
        RandomDamage,
        IfDamageType,
        IfWerewolf,
        TargetSubsequentAttacks,
        TargetSubsequentRageAttacks,
        TargetNextRageAttack,
        NextUse,
        WithinDistance,
        AnotherWithinDistance,
        TargetAnatomyArthropods,
        TargetFishAndSnail,
        TargetAnatomyAbberation,
        TargetElite,
        TargetNotElite,
        TargetUndead,
        TargetVulnerable,
        TargetSentient,
        TargetUnderEffect,
        TargetKnockedDown,
        AddAccuracy,
        AddMeleeAccuracy,
        AddBurstAccuracy,
        RemoveEvasion,

        AddPhysicalReflection,
        AddElementalDamageResistance,
        AddDamageResistance,
        //Fear,
        FearSentient,
        SetVulnerable,
        ExtraTraumaDamage,
        AddVulnerability,
        AddIndirectVulnerability,
        AddDirectVulnerability,
        AddVulnerabilityToElite,
        //DestroyedByDamageType,
        ChangeDamageType,
        ConditionalDamageType,
        IgnoreKnockback,
        IncreaseXPGain,
        MaxKillTime,
        RemoveEffects,
        TargetAbilityBoost,
        NoYellForHelp,
        ToKickerTarget,
        RepairBrokenBone,
        //AddEnthusiasm,
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
        AddCriticalChance,
        AnotherTrap,
        SummonDeer,
        AddChannelingTime,
        Again,
        OnEvade,
        OnEvadeMelee,
        MitigateReflect,
        MitigateReflectKick,
        ReflectRate,
        ThickArmor,
        //DrainAsArmor,
        WhenTeleporting,
        But,
        ApplyToCrits,
        ApplyToBasic,
        ImplantEgg,
        AbsorbDamage,
        CancelSlowdown,
        DoNotIgnoreKnockback,
        IfRestoreArmorAfterDelay,
        MultiplyArmorRestauration,
        MultiplyPowerRestauration,
    }
}
