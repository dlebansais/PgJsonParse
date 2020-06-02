namespace PgJsonObjects
{
    public class PgAIAbility
    {
        public int? RawMinLevel { get; set; }
        public int? RawMaxLevel { get; set; }
        public int? RawMinDistance { get; set; }
        public int? RawMinRange { get; set; }
        public int? RawMaxRange { get; set; }
        public int? RawCueValue { get; set; }
        public AbilityCue Cue { get; set; }
    }
}
