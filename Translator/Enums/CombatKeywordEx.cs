namespace PgObjects
{
    public enum CombatKeywordEx
    {
        Internal_None,
        RestoreHealth,
        RestoreHealthOverTime,
        RestoreArmor,
        RestoreArmorOverTime,
        RestorePower,
        RestorePowerOverTime,
        RestoreHealthOrArmor,
        AddSprintSpeed,
        IncreaseMaxHealth,
        IncreaseMaxArmor,
        IncreaseMaxPower,
        RegenPercentageOfArmor,
        IncreaseHealEfficiency,

        // Other effects
        ApplyWithChance,
        EffectDelay,
        EffectDuration,
        EffecOverTime,
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

        // Synthetic
        GiveBuff,
        GiveBuffOneUse,
    }
}
