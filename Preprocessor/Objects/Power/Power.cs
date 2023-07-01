namespace Preprocessor;

internal class Power
{
    public Power(RawPower rawPower)
    {
        IsUnavailable = rawPower.IsUnavailable;
        Prefix = rawPower.Prefix;
        Skill = rawPower.Skill;
        Slots = rawPower.Slots;
        Suffix = rawPower.Suffix;
        Tiers = rawPower.Tiers;
    }

    public bool? IsUnavailable { get; set; }
    public string? Prefix { get; set; }
    public string? Skill { get; set; }
    public string[]? Slots { get; set; }
    public string? Suffix { get; set; }
    public PowerTierDictionary? Tiers { get; set; }

    public RawPower ToRawPower()
    {
        RawPower Result = new();

        Result.IsUnavailable = IsUnavailable;
        Result.Prefix = Prefix;
        Result.Skill = Skill;
        Result.Slots = Slots;
        Result.Suffix = Suffix;
        Result.Tiers = Tiers;

        return Result;
    }
}
