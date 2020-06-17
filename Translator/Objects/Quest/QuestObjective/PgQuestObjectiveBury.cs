namespace PgObjects
{
    public class PgQuestObjectiveBury : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public PgSkill AnatomySkill { get; set; }
    }
}
