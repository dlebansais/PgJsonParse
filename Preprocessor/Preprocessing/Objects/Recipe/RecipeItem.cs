namespace Preprocessor;

internal class RecipeItem
{
    public RecipeItem(RawRecipeItem rawRecipeItem)
    {
        AttuneToCrafter = rawRecipeItem.AttuneToCrafter;
        ChanceToConsume = ParsePercentage(rawRecipeItem.ChanceToConsume);
        Description = rawRecipeItem.Desc;
        DurabilityConsumed = ParsePercentage(rawRecipeItem.DurabilityConsumed);
        ItemCode = rawRecipeItem.ItemCode;
        ItemKeys = rawRecipeItem.ItemKeys;
        PercentChance = ParsePercentage(rawRecipeItem.PercentChance);
        StackSize = rawRecipeItem.StackSize;
    }

    private static int? ParsePercentage(decimal? content)
    {
        if (content is null)
            return null;
        else
            return (int)(content * 100);
    }

    public bool? AttuneToCrafter { get; set; }
    public int? ChanceToConsume { get; set; }
    public string? Description { get; set; }
    public int? DurabilityConsumed { get; set; }
    public int? ItemCode { get; set; }
    public string[]? ItemKeys { get; set; }
    public int? PercentChance { get; set; }
    public int StackSize { get; set; }

    public RawRecipeItem ToRawRecipeItem()
    {
        RawRecipeItem Result = new();

        Result.AttuneToCrafter = AttuneToCrafter;
        Result.ChanceToConsume = ToRawPercentage(ChanceToConsume);
        Result.Desc = Description;
        Result.DurabilityConsumed = ToRawPercentage(DurabilityConsumed);
        Result.ItemCode = ItemCode;
        Result.ItemKeys = ItemKeys;
        Result.PercentChance = ToRawPercentage(PercentChance);
        Result.StackSize = StackSize;

        return Result;
    }

    private static decimal? ToRawPercentage(int? percentage)
    {
        if (percentage is null)
            return null;
        else
            return ((decimal)percentage) / 100;
    }
}
