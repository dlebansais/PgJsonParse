using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestObjectiveGuildGiveItem
    {
        Item QuestItem { get; }
        GameNpc DeliverNpc { get; }
        List<Item> ItemList { get; }
        ItemKeyword ItemKeyword { get; }
    }
}
