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
