namespace PgJsonObjects
{
    public class QuestObjectiveInteractionFlag : QuestObjective
    {
        #region Init
        public QuestObjectiveInteractionFlag(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, string InteractionFlag)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.InteractionFlag = InteractionFlag;
        }
        #endregion

        #region Properties
        public string InteractionFlag { get; private set; }
        #endregion
    }
}
