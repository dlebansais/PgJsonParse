namespace PgJsonObjects
{
    public interface IPgTrainingSource : IPgGenericSource
    {
        string NpcName { get; }
        IPgGameNpc Npc { get; }
    }
}
