namespace PgObjects
{
    public class PgQuestObjectiveTipPlayer : PgQuestObjective
    {
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; set; }
    }
}
