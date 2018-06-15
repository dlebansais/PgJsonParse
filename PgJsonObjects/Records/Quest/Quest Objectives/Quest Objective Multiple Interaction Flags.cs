using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveMultipleInteractionFlags : QuestObjective, IPgQuestObjectiveMultipleInteractionFlags
    {
        #region Init
        public QuestObjectiveMultipleInteractionFlags(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, List<string> InteractionFlagList)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.InteractionFlagList = InteractionFlagList;
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
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => QuestObjectiveRequirement } },
            { "InteractionFlags", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => InteractionFlagList } },
        }; } }
        #endregion

        #region Properties
        public List<string> InteractionFlagList { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddStringList(InteractionFlagList, data, ref offset, BaseOffset, 0, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
