namespace PgObjects
{
    public class PgQuestObjectiveSpecial : PgQuestObjective
    {
        public string Target { get; set; } = string.Empty;
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; set; }
        public string StringParam { get; set; } = string.Empty;
        public string? AnatomySkill_Key { get; set; }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
