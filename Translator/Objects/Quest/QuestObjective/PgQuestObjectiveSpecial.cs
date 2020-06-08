namespace PgJsonObjects
{
    public class PgQuestObjectiveSpecial : PgQuestObjective
    {
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; set; }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get; set; }
        public string StringParam { get; set; } = string.Empty;
        public string InteractionTarget { get; set; } = string.Empty;
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; }
    }
}
