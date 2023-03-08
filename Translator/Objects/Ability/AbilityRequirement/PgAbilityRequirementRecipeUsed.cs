namespace PgObjects
{
    public class PgAbilityRequirementRecipeUsed : PgAbilityRequirement
    {
        public string? Recipe_Key { get; set; }
        public int MaxTimesUsed { get { return RawMaxTimesUsed.HasValue ? RawMaxTimesUsed.Value : 0; } }
        public int? RawMaxTimesUsed { get; set; }
    }
}
