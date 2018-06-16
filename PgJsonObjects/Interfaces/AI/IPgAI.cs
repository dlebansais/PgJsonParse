namespace PgJsonObjects
{
    public interface IPgAI
    {
        AIAbilitySet Abilities { get; }
        string Comment { get; }
        float? RawMinDelayBetweenAbilities { get; }
        bool? RawIsMelee { get; }
        bool? RawIsUncontrolledPet { get; }
        bool? RawIsStationary { get; }
        bool? RawIsServerDriven { get; }
    }
}
