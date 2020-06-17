namespace PgObjects
{
    public class PgQuestObjectiveCollect : PgQuestObjective
    {
        public ItemKeyword Target { get; set; }
        public PgItem Item { get; set; }
    }
}
