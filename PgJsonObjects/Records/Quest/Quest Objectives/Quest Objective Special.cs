namespace PgJsonObjects
{
    public class QuestObjectiveSpecial : QuestObjective
    {
        #region Init
        public QuestObjectiveSpecial(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, int? RawMinAmount, int? RawMaxAmount, string StringParam)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawMinAmount = RawMinAmount;
            this.RawMaxAmount = RawMaxAmount;
            this.StringParam = StringParam;
        }
        #endregion

        #region Properties
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; private set; }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get; private set; }
        public string StringParam { get; private set; }
        #endregion
    }
}
