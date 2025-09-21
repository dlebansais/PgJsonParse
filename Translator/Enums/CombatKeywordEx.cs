namespace PgObjects
{
    public enum CombatKeywordEx
    {
        Internal_None,
        RestoreHealth,
        RestoreArmor,
        RestorePower,
        RestoreHealthOrArmor,
        AddSprintSpeed,
        IncreaseMaxHealth,
        IncreaseMaxArmor,
        IncreaseMaxPower,
        RegenPercentageOfArmor,
        IncreaseHealEfficiency,
        IncreaseRefreshTime,
        StunImmunity,
        Knockback,
        DamageBoost,

        // Other effects
        ApplyWithChance,
        EffectDelay,
        EffectDuration,
        EffectOverTime,
        EffectEverySecond,
        RecurringEffect,
        ApplyToSelf,
        ApplyToAllies,
        ApplyToSelfAndAllies,
        ApplyToPet,
        ApplyToSelfAndPet,
        NextUse,
        TargetRange,
        RequireTwoKnives,
        RequireNoAggro,
        RequirePlayingSong,
        RequireSpecialForm,
        RequireSameTarget,
        ToKickerTarget,
        RequireTargetOfAbility,
        RequireDirectDamageKillShot,
        RequireLowRage,

        // Synthetic
        GiveBuff,
        GiveBuffOneUse,
        RestoreHealthOverTime,
        RestoreArmorOverTime,
        RestorePowerOverTime,
        RestoreHealthOrArmorOverTime,
    }
}
