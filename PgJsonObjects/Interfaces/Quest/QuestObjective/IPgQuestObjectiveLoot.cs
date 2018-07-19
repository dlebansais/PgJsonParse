namespace PgJsonObjects
{
    public interface IPgQuestObjectiveLoot
    {
        IPgItem QuestItem { get; }
        IPgItemCollection ItemList { get; }
        ItemKeyword ItemTarget { get; }
        MonsterTypeTag MonsterTypeTag { get; }
        bool HasMonsterTypeTag { get; }
    }
}
