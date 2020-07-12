namespace PgObjects
{
    public class PgQuestObjectiveLootItemKeyword : PgQuestObjective
    {
        public ItemKeyword Keyword { get; set; }
        public MonsterTypeTag MonsterTypeTag { get; set; }
    }
}
