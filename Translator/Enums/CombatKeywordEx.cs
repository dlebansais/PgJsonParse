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

        // Other effects
        ApplyWithChance,
        EffectDelay,
        EffectDuration,
        ApplyToSelf,
        ApplyToSelfAndAllies,
        ApplyToPet,
        ApplyToSelfAndPet,
    }
}
