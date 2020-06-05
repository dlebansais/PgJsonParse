namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgRecipeItem
    {
        public PgItem Item { get; set; }
        public int ItemCode { get { return RawItemCode.HasValue ? RawItemCode.Value : 0; } }
        public int? RawItemCode { get; set; }
        public int StackSize { get { return RawStackSize.HasValue ? (RawStackSize.Value > 0 ? RawStackSize.Value : 1) : 0; } }
        public int? RawStackSize { get; set; }
        public float PercentChance { get { return RawPercentChance.HasValue ? RawPercentChance.Value : 1.0F; } }
        public float? RawPercentChance { get; set; }
        public List<RecipeItemKey> ItemKeyList { get; } = new List<RecipeItemKey>();
        public PgItemCollection MatchingKeyItemList { get; } = new PgItemCollection();
        public string Desc { get; set; } = string.Empty;
        public float ChanceToConsume { get { return RawChanceToConsume.HasValue ? RawChanceToConsume.Value : 1.0F; } }
        public float? RawChanceToConsume { get; set; }
        public float DurabilityConsumed { get { return RawDurabilityConsumed.HasValue ? RawDurabilityConsumed.Value : 0; } }
        public float? RawDurabilityConsumed { get; set; }
        public bool AttuneToCrafter { get { return RawAttuneToCrafter.HasValue && RawAttuneToCrafter.Value; } }
        public bool? RawAttuneToCrafter { get; set; }
    }
}
