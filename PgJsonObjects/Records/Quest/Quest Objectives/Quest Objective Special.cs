using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveSpecial : QuestObjective, IPgQuestObjectiveSpecial
    {
        #region Init
        public QuestObjectiveSpecial(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, int? RawMinAmount, int? RawMaxAmount, string StringParam, string InteractionTarget, QuestObjectiveRequirement QuestObjectiveRequirement)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawMinAmount = RawMinAmount;
            this.RawMaxAmount = RawMaxAmount;
            this.StringParam = StringParam;
            this.InteractionTarget = InteractionTarget;
            this.QuestObjectiveRequirement = QuestObjectiveRequirement;
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
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionTarget } },
            { "MinAmount", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawMinAmount.ToString() } },
            { "MaxAmount", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawMaxAmount.ToString() } },
            { "StringParam", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringParam } },
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => QuestObjectiveRequirement } },
        }; } }
        #endregion

        #region Properties
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get; private set; }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get; private set; }
        public string StringParam { get; private set; }
        public string InteractionTarget { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            AddInt(RawMinAmount, data, ref offset, BaseOffset, 0);
            AddInt(RawMaxAmount, data, ref offset, BaseOffset, 4);
            AddString(StringParam, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddString(InteractionTarget, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddEnum(QuestObjectiveRequirement, data, ref offset, BaseOffset, 16);

            FinishSerializing(data, ref offset, BaseOffset, 18, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
