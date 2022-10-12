namespace PgObjects
{
    public class PgAbilityRequirementTimeOfDay : PgAbilityRequirement
    {
        public int MinHour { get { return RawMinHour.HasValue ? RawMinHour.Value : 0; } }
        public int? RawMinHour { get; set; }
        public int MaxHour { get { return RawMaxHour.HasValue ? RawMaxHour.Value : 0; } }
        public int? RawMaxHour { get; set; }
    }
}
