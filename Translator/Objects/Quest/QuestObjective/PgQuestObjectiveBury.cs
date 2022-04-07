namespace PgObjects
{
    public class PgQuestObjectiveBury : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public string? AnatomySkill_Key { get; set; }
    }
}
