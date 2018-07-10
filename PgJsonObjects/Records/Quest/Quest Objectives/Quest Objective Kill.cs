using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveKill : QuestObjective, IPgQuestObjectiveKill
    {
        #region Init
        public QuestObjectiveKill(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, QuestObjectiveKillTarget Target, string AbilityKeyword, EffectKeyword EffectRequirement, QuestObjectiveRequirement QuestObjectiveRequirement)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
        {
            this.Target = Target;
            this.AbilityKeyword = AbilityKeyword;
            this.EffectRequirement = EffectRequirement;
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
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => QuestObjectiveRequirement } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveKillTarget>.ToString(Target, TextMaps.KillTargetStringMap, QuestObjectiveKillTarget.Internal_None) } },
            { "AbilityKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => AbilityKeyword } },
            { "HasEffectKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<EffectKeyword>.ToString(EffectRequirement, null, EffectKeyword.Internal_None) } },
        }; } }
        #endregion

        #region Properties
        public string AbilityKeyword { get; private set; }
        public QuestObjectiveKillTarget Target { get; private set; }
        public EffectKeyword EffectRequirement { get; private set; }
        public bool HasEffectRequirement { get { return EffectRequirement != EffectKeyword.Internal_None; } }
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddString(AbilityKeyword, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddEnum(Target, data, ref offset, BaseOffset, 4);
            AddEnum(EffectRequirement, data, ref offset, BaseOffset, 6);
            AddObject(QuestObjectiveRequirement as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
