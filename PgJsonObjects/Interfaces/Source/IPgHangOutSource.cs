namespace PgJsonObjects
{
    public interface IPgHangOutSource : IPgGenericSource
    {
        string NpcName { get; }
        IPgGameNpc Npc { get; }
    }
}
