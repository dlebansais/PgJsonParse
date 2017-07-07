using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveMultipleInteractionFlags : QuestObjective
    {
        #region Init
        public QuestObjectiveMultipleInteractionFlags(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, List<string> InteractionFlagList)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.InteractionFlagList = InteractionFlagList;
        }
        #endregion

        #region Properties
        public List<string> InteractionFlagList { get; private set; }
        #endregion
    }
}
