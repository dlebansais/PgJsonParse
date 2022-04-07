namespace PgObjects
{
    public class PgQuestObjectiveUseItem : PgQuestObjective
    {
        public string? Item_Key { get; set; }
        public PgQuestObjectiveRequirement? QuestObjectiveRequirement { get; set; }
    }
}
