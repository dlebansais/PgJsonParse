namespace Preprocessor;

using System.Collections.Generic;

public class Power
{
    public Power(RawPower rawPower)
    {
        IsUnavailable = rawPower.IsUnavailable;
        Prefix = rawPower.Prefix;
        Skill = rawPower.Skill;
        Slots = rawPower.Slots;
        Suffix = rawPower.Suffix;

        if (rawPower.Tiers is not null)
        {
            UnsortedTiers = rawPower.Tiers.ToArray();

            List<PowerTier> TierList = new(rawPower.Tiers);
            TierList.Sort(SortByLevel);
            Tiers = TierList.ToArray();
        }
        else
            Tiers = null;
    }

    private static int SortByLevel(PowerTier tier1, PowerTier tier2)
    {
        return tier1.Tier - tier2.Tier;
    }

    public bool? IsUnavailable { get; set; }
    public string? Prefix { get; set; }
    public string? Skill { get; set; }
    public string[]? Slots { get; set; }
    public string? Suffix { get; set; }
    public PowerTier[]? Tiers { get; set; }

    public RawPower ToRawPower()
    {
        RawPower Result = new();

        Result.IsUnavailable = IsUnavailable;
        Result.Prefix = Prefix;
        Result.Skill = Skill;
        Result.Slots = Slots;
        Result.Suffix = Suffix;

        if (UnsortedTiers is not null)
        {
            Result.Tiers = new();
            Result.Tiers.AddRange(UnsortedTiers);
        }
        else
            Result.Tiers = null;

        return Result;
    }

    private PowerTier[]? UnsortedTiers;
}
