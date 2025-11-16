namespace PgObjects
{
    public class PgQuestObjectiveBeAttacked : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public string? AnatomySkill_Key { get; set; }
        public DamageType DamageType { get; set; }
    }
}
