namespace PgJsonObjects
{
    public interface IPgAIAbility
    {
        int? RawMinLevel { get; }
        int? RawMaxLevel { get; }
        int? RawMinDistance { get; }
        int? RawMinRange { get; }
        int? RawMaxRange { get; }
        int? RawCueValue { get; }
        AbilityCue Cue { get; }
    }
}
