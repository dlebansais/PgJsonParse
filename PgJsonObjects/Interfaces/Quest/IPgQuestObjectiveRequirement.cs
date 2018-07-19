namespace PgJsonObjects
{
    public interface IPgQuestObjectiveRequirement : IObjectContentGenerator
    {
        string Type { get; }
        int? RawMinHour { get; }
        int? RawMaxHour { get; }
    }
}
