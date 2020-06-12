namespace PgObjects
{
    public class PgQuestObjectiveBeAttacked : PgQuestObjective
    {
        public string InteractionTarget { get; set; } = string.Empty;
        public PgSkill AnatomySkill { get; set; }
    }
}
