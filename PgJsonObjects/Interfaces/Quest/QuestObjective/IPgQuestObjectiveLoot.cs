using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestObjectiveLoot
    {
        Item QuestItem { get; }
        List<Item> ItemList { get; }
        ItemKeyword ItemTarget { get; }
        MonsterTypeTag MonsterTypeTag { get; }
    }
}
