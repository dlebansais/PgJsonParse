namespace PgJsonObjects
{
    public class PgQuestRequirementRace : PgQuestRequirement
    {
        public string AllowedRace { get; set; } = string.Empty;
        public string DisallowedRace { get; set; } = string.Empty;
    }
}
