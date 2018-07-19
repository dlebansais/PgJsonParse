using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveSpecial : PgQuestObjective<PgQuestObjectiveSpecial>, IPgQuestObjectiveSpecial
    {
        public PgQuestObjectiveSpecial(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveSpecial CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveSpecial CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveSpecial(data, ref offset);
        }

        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(PropertiesOffset + 0); } }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get { return GetInt(PropertiesOffset + 4); } }
        public string StringParam { get { return GetString(PropertiesOffset + 8); } }
        public string InteractionTarget { get { return GetString(PropertiesOffset + 12); } }
        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return GetObject(PropertiesOffset + 16, ref _QuestObjectiveRequirement, PgQuestObjectiveRequirement.CreateNew); } } private IPgQuestObjectiveRequirement _QuestObjectiveRequirement;

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
                GetObject = () => QuestObjectiveRequirement as IObjectContentGenerator } },
        }; } }
    }
}
