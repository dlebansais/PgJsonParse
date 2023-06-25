namespace PgObjects
{
    public class PgAIAbility
    {
        public string AbilityKey { get; set; } = string.Empty;
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get; set; }
        public float MinRange { get { return RawMinRange.HasValue ? RawMinRange.Value : 0; } }
        public float? RawMinRange { get; set; }
        public int MaxRange { get { return RawMaxRange.HasValue ? RawMaxRange.Value : 0; } }
        public int? RawMaxRange { get; set; }
        public AbilityCue Cue { get; set; }
        public int CueValue { get { return RawCueValue.HasValue ? RawCueValue.Value : 0; } }
        public int? RawCueValue { get; set; }
    }
}
