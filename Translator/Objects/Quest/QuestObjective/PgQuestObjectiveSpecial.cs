namespace PgJsonObjects
{
    public class PgQuestObjectiveSpecial : PgQuestObjective
    {
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; private set; }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get; private set; }
        public string StringParam { get; private set; } = string.Empty;
        public string InteractionTarget { get; private set; } = string.Empty;
    }
}
