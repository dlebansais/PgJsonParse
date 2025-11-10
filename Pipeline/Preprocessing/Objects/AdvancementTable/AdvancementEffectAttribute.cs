namespace Preprocessor;

using FreeSql.DataAnnotations;

public class AdvancementEffectAttribute
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Key { get; set; }

    public string? Attribute { get; set; }

    public decimal? Value { get; set; }
}
