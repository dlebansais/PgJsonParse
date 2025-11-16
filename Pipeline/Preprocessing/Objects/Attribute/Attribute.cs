namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Attribute : IHasKey<string>
{
    public Attribute(string key)
    {
        Key = key;
    }

    public Attribute(string key, RawAttribute rawAttribute)
        : this(key)
    {
        DefaultValue = rawAttribute.DefaultValue;
        DisplayRule = rawAttribute.DisplayRule;
        DisplayType = rawAttribute.DisplayType;
        IconIds = rawAttribute.IconIds;
        IsHidden = rawAttribute.IsHidden;
        Label = rawAttribute.Label;
        Tooltip = rawAttribute.Tooltip;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public string Key { get; set; }

    public decimal? DefaultValue { get; set; }

    public string? DisplayRule { get; set; }

    public string? DisplayType { get; set; }

    [Column(MapType = typeof(string))]
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
        Result.IconIds = IconIds;
        Result.IsHidden = IsHidden;
        Result.Label = Label;
        Result.Tooltip = Tooltip;

        return Result;
    }
}
