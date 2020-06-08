namespace PgJsonObjects
{
    public class PgQuestObjective
    {
        public QuestObjectiveType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Number { get { return RawNumber.HasValue ? RawNumber.Value : 1; } }
        public bool HasNumber { get { return RawNumber.HasValue && RawNumber.Value != 1; } }
        public int? RawNumber { get; set; }
        public bool MustCompleteEarlierObjectivesFirst { get { return RawMustCompleteEarlierObjectivesFirst.HasValue && RawMustCompleteEarlierObjectivesFirst.Value; } }
        public bool? RawMustCompleteEarlierObjectivesFirst;
        public int? MinHour { get; set; }
        public int? MaxHour { get; set; }
        public bool GroupId { get { return RawGroupId.HasValue && RawGroupId.Value != 1; } }
        public int? RawGroupId { get; set; }
        public string InternalName { get; set; } = string.Empty;
    }
}
