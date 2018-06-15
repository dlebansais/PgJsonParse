using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestObjectiveItem
    {
        Item QuestItem { get; }
        List<Item> TargetItemList { get; }
        ItemKeyword Target { get; }
    }
}
