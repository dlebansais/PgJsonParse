namespace Preprocessor;

public class RawPower
{
    public bool? IsUnavailable { get; set; }
    public string? Prefix { get; set; }
    public string? Skill { get; set; }
    public string[]? Slots { get; set; }
    public string? Suffix { get; set; }
    public PowerTierCollection? Tiers { get; set; }
}
