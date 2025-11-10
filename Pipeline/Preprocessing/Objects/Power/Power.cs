namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Power
{
    public Power(int key)
    {
        Key = key;
    }

    public Power(int key, RawPower rawPower)
        : this(key)
    {
        InternalName = rawPower.InternalName;
        IsUnavailable = rawPower.IsUnavailable;
        Prefix = rawPower.Prefix;
        Skill = rawPower.Skill;
        Slots = rawPower.Slots;
        Suffix = rawPower.Suffix;

        if (rawPower.Tiers is null)
            throw new PreprocessorException(this);

        UnsortedTiers = rawPower.Tiers.ToArray();

        List<PowerTier> TierList = new(rawPower.Tiers);
        TierList.Sort(SortByLevel);
        Tiers = TierList.ToArray();
    }

    private static int SortByLevel(PowerTier tier1, PowerTier tier2)
    {
        return tier1.Tier - tier2.Tier;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    public string? InternalName { get; set; }

    public bool? IsUnavailable { get; set; }

    public string? Prefix { get; set; }

    public string? Skill { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? Slots { get; set; }

    public string? Suffix { get; set; }

    [Navigate(nameof(PowerTier.Key))]
    public PowerTier[] Tiers { get; set; } = Array.Empty<PowerTier>();

    public RawPower ToRawPower()
    {
        RawPower Result = new();

        Result.InternalName = InternalName;
        Result.IsUnavailable = IsUnavailable;
        Result.Prefix = Prefix;
        Result.Skill = Skill;
        Result.Slots = Slots;
        Result.Suffix = Suffix;

        Result.Tiers = new();
        Result.Tiers.AddRange(UnsortedTiers);

        return Result;
    }

    private PowerTier[] UnsortedTiers = Array.Empty<PowerTier>();
}
