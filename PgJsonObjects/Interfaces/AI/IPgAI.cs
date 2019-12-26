namespace PgJsonObjects
{
    public interface IPgAI : IJsonKey, IObjectContentGenerator
    {
        IPgAIAbilitySet Abilities { get; }
        string Comment { get; }
        float? RawMinDelayBetweenAbilities { get; }
        bool? RawIsMelee { get; }
        bool? RawIsUncontrolledPet { get; }
        bool? RawIsStationary { get; }
        bool? RawIsServerDriven { get; }
        bool? RawUseAbilitiesWithoutEnemyTarget { get; }
        bool? RawSwimming { get; }
    }
}
