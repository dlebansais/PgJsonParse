namespace PgObjects
{
    public class PgQuestRequirementQuestCompleted : PgQuestRequirement
    {
        public PgQuestCollection QuestList { get; set; } = new PgQuestCollection();
        public bool Daytime { get { return RawDaytime.HasValue && RawDaytime.Value; } }
        public bool? RawDaytime { get; set; }
    }
}
