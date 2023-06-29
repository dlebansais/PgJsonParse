namespace PgObjects
{
    public class PgQuestRequirementAreaEventOn : PgQuestRequirement
    {
        public MapAreaName AreaName { get; set; }
        public string? Skill_Key { get; set; }
    }
}
