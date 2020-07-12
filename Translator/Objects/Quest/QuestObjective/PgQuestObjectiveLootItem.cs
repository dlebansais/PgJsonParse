namespace PgObjects
{
    public class PgQuestObjectiveLootItem : PgQuestObjective
    {
        public PgItem Item { get; set; }
        public MonsterTypeTag MonsterTypeTag { get; set; }
    }
}
