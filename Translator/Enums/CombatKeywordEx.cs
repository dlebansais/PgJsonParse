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

        // Other effects
        ApplyWithChance,
        EffectDelay,
        EffectDuration,
        ApplyToSelf,
        ApplyToSelfAndAllies,
        ApplyToPet,
        ApplyToSelfAndPet,
        NextUse,

        // Synthetic
        GiveBuff,
        GiveBuffOneUse,
    }
}
