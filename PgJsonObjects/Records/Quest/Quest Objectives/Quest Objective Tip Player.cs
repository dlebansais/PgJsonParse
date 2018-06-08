using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveTipPlayer : QuestObjective
    {
        #region Init
        public QuestObjectiveTipPlayer(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, int? RawMinAmount)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawMinAmount = RawMinAmount;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
            { "MinAmount", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawMinAmount.ToString() } },
        }; } }
        #endregion

        #region Properties
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; private set; }
        #endregion
    }
}
