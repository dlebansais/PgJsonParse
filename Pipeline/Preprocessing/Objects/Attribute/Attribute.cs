namespace Preprocessor;

using System.Collections.Generic;

public class Attribute
{
    public Attribute(RawAttribute rawAttribute)
    {
        DefaultValue = rawAttribute.DefaultValue;
        DisplayRule = rawAttribute.DisplayRule;
        DisplayType = rawAttribute.DisplayType;
        IconIds = ParseIconIds(rawAttribute.IconIds, rawAttribute.Label);
        IsHidden = rawAttribute.IsHidden;
        Label = rawAttribute.Label;
        Tooltip = rawAttribute.Tooltip;
    }

    private static int[]? ParseIconIds(int[]? rawIconIds, string? rawLabel)
    {
        if (rawIconIds is null || rawLabel is null)
            return null;

        List<int> Result = new();

        foreach (int IconId in rawIconIds)
        {
            if (IconId == 3402 && rawLabel.StartsWith("Gripjaw"))
                Result.Add(3042);
            else
                Result.Add(IconId);
        }

        return Result.ToArray();
    }

    public decimal? DefaultValue { get; set; }
    public string? DisplayRule { get; set; }
    public string? DisplayType { get; set; }
    public int[]? IconIds { get; set; }
    public bool? IsHidden { get; set; }
    public string? Label { get; set; }
    public string? Tooltip { get; set; }

    public RawAttribute ToRawAttribute()
    {
        RawAttribute Result = new();

        Result.DefaultValue = DefaultValue;
        Result.DisplayRule = DisplayRule;
        Result.DisplayType = DisplayType;
        Result.IconIds = ToRawIconIds(IconIds, Label);
        Result.IsHidden = IsHidden;
        Result.Label = Label;
        Result.Tooltip = Tooltip;

        return Result;
    }

    private static int[]? ToRawIconIds(int[]? iconIds, string? label)
    {
        if (iconIds is null || label is null)
            return null;

        List<int> Result = new();

        foreach (int IconId in iconIds)
        {
            if (IconId == 3042 && label.StartsWith("Gripjaw"))
                Result.Add(3402);
            else
                Result.Add(IconId);
        }

        return Result.ToArray();
    }
}
