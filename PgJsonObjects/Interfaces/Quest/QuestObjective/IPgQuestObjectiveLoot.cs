namespace PgJsonObjects
{
    public interface IPgQuestObjectiveLoot
    {
        IPgItem QuestItem { get; }
        ItemCollection ItemList { get; }
        ItemKeyword ItemTarget { get; }
        MonsterTypeTag MonsterTypeTag { get; }
    }
}
