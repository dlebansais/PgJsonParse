using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveInteractionFlag : QuestObjective
    {
        #region Init
        public QuestObjectiveInteractionFlag(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, string InteractionFlag, string InteractionTarget)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.InteractionFlag = InteractionFlag;
            this.InteractionTarget = InteractionTarget;
        }
        #endregion

        #region Properties
        public string InteractionFlag { get; private set; }
        public string InteractionTarget { get; private set; }
        #endregion

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionTarget } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawMustCompleteEarlierObjectivesFirst = value,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionFlag } },
        }; } }
    }
}
