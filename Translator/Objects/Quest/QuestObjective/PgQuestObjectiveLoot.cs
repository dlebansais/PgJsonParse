namespace PgObjects
{
    public class PgQuestObjectiveLoot : PgQuestObjective
    {
        public ItemKeyword Target { get; set; }
        public PgItem Item { get; set; }
        public MonsterTypeTag MonsterTypeTag { get; set; }
    }
}
