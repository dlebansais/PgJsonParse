namespace PgJsonObjects
{
    public interface IPgQuestObjectiveLoot
    {
        Item QuestItem { get; }
        ItemCollection ItemList { get; }
        ItemKeyword ItemTarget { get; }
        MonsterTypeTag MonsterTypeTag { get; }
    }
}
