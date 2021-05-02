namespace PgObjects
{
    public class PgQuestObjectiveLootItem : PgQuestObjective
    {
        public PgItem Item { get; set; } = null!;
        public MonsterTypeTag MonsterTypeTag { get; set; }
    }
}
