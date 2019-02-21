using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveInteractionFlag : QuestObjective, IPgQuestObjectiveInteractionFlag
    {
        #region Init
        public QuestObjectiveInteractionFlag(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, string InteractionFlag, string InteractionTarget)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
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
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionFlag } },
        }; } }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            AddString(InteractionFlag, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(InteractionTarget, data, ref offset, BaseOffset, 4, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 8, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
