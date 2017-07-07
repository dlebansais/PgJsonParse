namespace PgJsonObjects
{
    public class QuestObjectiveKill : QuestObjective
    {
        #region Init
        public QuestObjectiveKill(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, QuestObjectiveKillTarget Target, string AbilityKeyword)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.Target = Target;
            this.AbilityKeyword = AbilityKeyword;
        }
        #endregion

        #region Properties
        public QuestObjectiveKillTarget Target { get; private set; }
        public string AbilityKeyword { get; private set; }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (Target != QuestObjectiveKillTarget.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.QuestObjectiveKillTargetTextMap[Target]);
                AddWithFieldSeparator(ref Result, AbilityKeyword);

                return Result;
            }
        }
        #endregion
    }
}
