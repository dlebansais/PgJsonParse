namespace PgObjects
{
    public class PgQuestObjectiveBeAttacked : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public PgSkill AnatomySkill { get; set; }
    }
}
