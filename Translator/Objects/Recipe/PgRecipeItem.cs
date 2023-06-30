namespace PgObjects
{
    using System.Collections.Generic;

    public class PgRecipeItem
    {
        public string? Item_Key { get; set; }
        public int StackSize { get { return RawStackSize.HasValue ? (RawStackSize.Value > 0 ? RawStackSize.Value : 1) : 0; } }
        public int? RawStackSize { get; set; }
        public int PercentChance { get { return RawPercentChance.HasValue ? RawPercentChance.Value : 100; } }
        public int? RawPercentChance { get; set; }
        public List<RecipeItemKey> ItemKeyList { get; set; } = new List<RecipeItemKey>();
        public string Description { get; set; } = string.Empty;
        public int ChanceToConsume { get { return RawChanceToConsume.HasValue ? RawChanceToConsume.Value : 100; } }
        public int? RawChanceToConsume { get; set; }
        public int DurabilityConsumed { get { return RawDurabilityConsumed.HasValue ? RawDurabilityConsumed.Value : 0; } }
        public int? RawDurabilityConsumed { get; set; }
        public bool AttuneToCrafter { get { return RawAttuneToCrafter.HasValue && RawAttuneToCrafter.Value; } }
        public bool? RawAttuneToCrafter { get; set; }
    }
}
