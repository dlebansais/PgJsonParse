namespace PgObjects
{
    public class PgQuestObjectiveLootItem : PgQuestObjective
    {
        public string? Item_Key { get; set; }
        public MonsterTypeTag MonsterTypeTag { get; set; }
    }
}
