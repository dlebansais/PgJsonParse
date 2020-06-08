namespace PgJsonObjects
{
    public class PgQuestObjectiveHave : PgQuestObjective
    {
        public ItemKeyword ItemTarget { get; set; }
        public PgItem QuestItem { get; set; }
    }
}
