﻿using System.Collections.Generic;

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
        ItemKeys = ParseItemKeys(rawRecipeItem.ItemKeys);
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

    private static string[]? ParseItemKeys(string[]? rawItemKeys)
    {
        if (rawItemKeys is null)
            return null;

        List<string> Result = new();
        foreach (string RawItemKey in rawItemKeys)
            Result.Add(ParseItemKey(RawItemKey));

        return Result.ToArray();
    }

    private static string ParseItemKey(string rawItemKey)
    {
        string Result = rawItemKey;

        Result = Result.Replace(":", "_");

        if (Result.StartsWith("!"))
            Result = "Not" + Result.Substring(1);

        return Result;
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
        Result.ItemKeys = ToRawItemKeys(ItemKeys);
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

    private static string[]? ToRawItemKeys(string[]? itemKeys)
    {
        if (itemKeys is null)
            return null;

        List<string> Result = new();
        foreach (string ItemKey in itemKeys)
            Result.Add(ToRawItemKeys(ItemKey));

        return Result.ToArray();
    }

    private static string ToRawItemKeys(string itemKey)
    {
        string Result = itemKey;

        Result = Result.Replace("_", ":");

        if (Result.StartsWith("Not"))
            Result = "!" + Result.Substring(3);

        return Result;
    }
}
