namespace PgJsonObjects
{
    public class PgQuestObjectiveLoot : PgQuestObjective
    {
        public ItemKeyword ItemTarget { get; set; }
        public PgItem QuestItem { get; set; }
        public MonsterTypeTag MonsterTypeTag { get; set; }
    }
}
