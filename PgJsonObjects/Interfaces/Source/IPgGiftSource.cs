namespace PgJsonObjects
{
    public interface IPgGiftSource : IPgGenericSource
    {
        string NpcName { get; }
        IPgGameNpc Npc { get; }
    }
}
