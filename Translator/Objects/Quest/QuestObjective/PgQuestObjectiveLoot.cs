namespace PgJsonObjects
{
    public class PgQuestObjectiveLoot : PgQuestObjective
    {
        public PgItem QuestItem { get; set; }
        public PgItemCollection ItemList { get; } = new PgItemCollection();
        public ItemKeyword ItemTarget { get; set; }
        public MonsterTypeTag MonsterTypeTag { get; set; }
    }
}
