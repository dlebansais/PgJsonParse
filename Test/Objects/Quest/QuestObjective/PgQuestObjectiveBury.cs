namespace PgObjects
{
    public class PgQuestObjectiveBury : PgQuestObjective
    {
        public string InteractionTarget { get; set; } = string.Empty;
        public PgSkill AnatomySkill { get; set; }
    }
}
