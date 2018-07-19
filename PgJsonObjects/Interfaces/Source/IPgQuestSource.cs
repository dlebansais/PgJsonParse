namespace PgJsonObjects
{
    public interface IPgQuestSource : IPgGenericSource
    {
        IPgQuest Quest { get; }
    }
}
