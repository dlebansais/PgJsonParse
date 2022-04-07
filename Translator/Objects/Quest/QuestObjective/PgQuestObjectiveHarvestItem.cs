namespace PgObjects
{
    public class PgQuestObjectiveHarvestItem : PgQuestObjective
    {
        public string? Item_Key { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
