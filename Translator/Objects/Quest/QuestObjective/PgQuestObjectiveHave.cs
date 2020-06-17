namespace PgObjects
{
    public class PgQuestObjectiveHave : PgQuestObjective
    {
        public ItemKeyword Target { get; set; }
        public PgItem Item { get; set; }
    }
}
