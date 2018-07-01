using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveKill : PgQuestObjective<PgQuestObjectiveKill>, IPgQuestObjectiveKill
    {
        public PgQuestObjectiveKill(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveKill CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveKill CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveKill(data, ref offset);
        }

        public string AbilityKeyword { get { return GetString(PropertiesOffset + 0); } }
        public QuestObjectiveKillTarget Target { get { return GetEnum<QuestObjectiveKillTarget>(PropertiesOffset + 4); } }
        public EffectKeyword EffectRequirement { get { return GetEnum<EffectKeyword>(PropertiesOffset + 6); } }
        public IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return GetObject(PropertiesOffset + 8, ref _QuestObjectiveRequirement, PgQuestObjectiveRequirement.CreateNew); } } private IPgQuestObjectiveRequirement _QuestObjectiveRequirement;

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
                GetObject = () => QuestObjectiveRequirement as IObjectContentGenerator } },
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
    }
}
