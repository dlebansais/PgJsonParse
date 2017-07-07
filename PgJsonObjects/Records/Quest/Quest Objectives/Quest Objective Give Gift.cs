namespace PgJsonObjects
{
    public class QuestObjectiveGiveGift : QuestObjective
    {
        #region Init
        public QuestObjectiveGiveGift(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, float? RawMinFavorReceived, float? RawMaxFavorReceived)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawMinFavorReceived = RawMinFavorReceived;
            this.RawMaxFavorReceived = RawMaxFavorReceived;
        }
        #endregion

        #region Properties
        public float MinFavorReceived { get { return RawMinFavorReceived.HasValue ? RawMinFavorReceived.Value : 0; } }
        public float? RawMinFavorReceived { get; private set; }
        public float MaxFavorReceived { get { return RawMaxFavorReceived.HasValue ? RawMaxFavorReceived.Value : 0; } }
        public float? RawMaxFavorReceived { get; private set; }
        #endregion
    }
}
