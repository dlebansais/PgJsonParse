namespace PgObjects
{
    public class PgQuestObjectiveRequirementHasMountInStable : PgQuestObjectiveRequirement
    {
        public int MinimumMountsNeeded { get { return RawMinimumMountsNeeded.HasValue ? RawMinimumMountsNeeded.Value : 0; } }
        public int? RawMinimumMountsNeeded { get; set; }
    }
}
