namespace PgObjects
{
    public class PgConditionalKeyword
    {
        public bool IsDefault { get { return RawIsDefault.HasValue && RawIsDefault.Value; } }
        public bool? RawIsDefault { get; set; }
        public EffectKeyword EffectKeywordMustExist { get; set; }
        public EffectKeyword EffectKeywordMustNotExist { get; set; }
        public AbilityKeyword Keyword { get; set; }
    }
}
