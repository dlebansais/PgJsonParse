namespace PgJsonObjects
{
    public interface IPgQuestObjectiveSpecial
    {
        int MinAmount { get; }
        int? RawMinAmount { get; }
        int MaxAmount { get; }
        int? RawMaxAmount { get; }
        string StringParam { get; }
        string InteractionTarget { get; }
    }
}
