namespace PgJsonObjects
{
    public class PgQuestRequirementRace : PgQuestRequirement
    {
        public Race AllowedRace { get; set; }
        public Race DisallowedRace { get; set; }
    }
}
