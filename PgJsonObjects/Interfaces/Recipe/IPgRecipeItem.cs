using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgRecipeItem
    {
        Item Item { get; }
        int ItemCode { get; }
        int? RawItemCode { get; }
        int StackSize { get; }
        int? RawStackSize { get; }
        double PercentChance { get; }
        double? RawPercentChance { get; }
        List<RecipeItemKey> ItemKeyList { get; }
        ItemCollection MatchingKeyItemList { get; }
        string Desc { get; }
        double ChanceToConsume { get; }
        double? RawChanceToConsume { get; }
        double DurabilityConsumed { get; }
        double? RawDurabilityConsumed { get; }
        bool AttuneToCrafter { get; }
        bool? RawAttuneToCrafter { get; }
    }
}
