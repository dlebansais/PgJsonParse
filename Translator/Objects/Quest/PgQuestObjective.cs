namespace PgObjects
{
    public abstract class PgQuestObjective
    {
        public string Description { get; set; } = string.Empty;
        public bool MustCompleteEarlierObjectivesFirst { get { return RawMustCompleteEarlierObjectivesFirst.HasValue && RawMustCompleteEarlierObjectivesFirst.Value; } }
        public bool? RawMustCompleteEarlierObjectivesFirst { get; set; }
        public bool GroupId { get { return RawGroupId.HasValue && RawGroupId.Value != 1; } }
        public int? RawGroupId { get; set; }
        public int Number { get { return RawNumber.HasValue ? RawNumber.Value : 1; } }
        public int? RawNumber { get; set; }
        public string InternalName { get; set; } = string.Empty;
    }
}
