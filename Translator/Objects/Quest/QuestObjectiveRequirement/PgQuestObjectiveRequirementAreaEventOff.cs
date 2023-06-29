namespace PgObjects
{
    public class PgQuestObjectiveRequirementAreaEventOff : PgQuestObjectiveRequirement
    {
        public MapAreaName AreaName { get; set; }
        public bool Daytime { get { return RawDaytime.HasValue && RawDaytime.Value; } }
        public bool? RawDaytime { get; set; }
    }
}
