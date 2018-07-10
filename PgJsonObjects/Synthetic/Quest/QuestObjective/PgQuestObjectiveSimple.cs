using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveSimple : PgQuestObjective<PgQuestObjectiveSimple>, IPgQuestObjectiveSimple
    {
        public PgQuestObjectiveSimple(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveSimple CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveSimple CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveSimple(data, ref offset);
        }

        public string InteractionTarget { get { return GetString(PropertiesOffset + 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionTarget } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
        }; } }
    }
}
