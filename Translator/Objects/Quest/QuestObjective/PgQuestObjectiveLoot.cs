namespace PgJsonObjects
{
    public class PgQuestObjectiveLoot : PgQuestObjective
    {
        public PgItem QuestItem { get; private set; }
        public PgItemCollection ItemList { get; private set; } = new PgItemCollection();
        public ItemKeyword ItemTarget { get; private set; }
        public MonsterTypeTag MonsterTypeTag { get; private set; }
    }
}
