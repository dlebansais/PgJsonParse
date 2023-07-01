namespace Preprocessor;

internal class RawRecipeItem
{
    public bool? AttuneToCrafter { get; set; }
    public decimal? ChanceToConsume { get; set; }
    public string? Desc { get; set; }
    public decimal? DurabilityConsumed { get; set; }
    public int? ItemCode { get; set; }
    public string[]? ItemKeys { get; set; }
    public decimal? PercentChance { get; set; }
    public int StackSize { get; set; }
}
