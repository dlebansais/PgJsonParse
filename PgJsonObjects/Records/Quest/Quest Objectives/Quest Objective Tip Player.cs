namespace PgJsonObjects
{
    public class QuestObjectiveTipPlayer : QuestObjective
    {
        #region Init
        public QuestObjectiveTipPlayer(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, int? RawMinAmount)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawMinAmount = RawMinAmount;
        }
        #endregion

        #region Properties
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; private set; }
        #endregion
    }
}
