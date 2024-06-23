namespace Preprocessor;

public class SpecialValue
{
    public string[]? AttributesThatDelta { get; set; }
    public string[]? AttributesThatDeltaBase { get; set; }
    public string[]? AttributesThatMod { get; set; }
    public string? DisplayType { get; set; }
    public string? Label { get; set; }
    public string? SkipIfThisAttributeIsZero { get; set; }
    public bool? SkipIfZero { get; set; }
    public string? Suffix { get; set; }
    public decimal? Value { get; set; }
}
