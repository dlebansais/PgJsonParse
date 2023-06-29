namespace Preprocessor;

internal class RecipeItem
{
    public RecipeItem(RawRecipeItem rawRecipeItem)
    {
        AttuneToCrafter = rawRecipeItem.AttuneToCrafter;
        ChanceToConsume = rawRecipeItem.ChanceToConsume;
        Description = rawRecipeItem.Desc;
        DurabilityConsumed = rawRecipeItem.DurabilityConsumed;
        ItemCode = rawRecipeItem.ItemCode;
        ItemKeys = rawRecipeItem.ItemKeys;
        PercentChance = rawRecipeItem.PercentChance;
        StackSize = rawRecipeItem.StackSize;
    }

    public bool? AttuneToCrafter { get; set; }
    public decimal? ChanceToConsume { get; set; }
    public string? Description { get; set; }
    public decimal? DurabilityConsumed { get; set; }
    public int? ItemCode { get; set; }
    public string[]? ItemKeys { get; set; }
    public decimal? PercentChance { get; set; }
    public int StackSize { get; set; }

    public RawRecipeItem ToRawRecipeItem()
    {
        RawRecipeItem Result = new();

        Result.AttuneToCrafter = AttuneToCrafter;
        Result.ChanceToConsume = ChanceToConsume;
        Result.Desc = Description;
        Result.DurabilityConsumed = DurabilityConsumed;
        Result.ItemCode = ItemCode;
        Result.ItemKeys = ItemKeys;
        Result.PercentChance = PercentChance;
        Result.StackSize = StackSize;

        return Result;
    }
}
