using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveInteractionFlag : PgQuestObjective<PgQuestObjectiveInteractionFlag>, IPgQuestObjectiveInteractionFlag
    {
        public PgQuestObjectiveInteractionFlag(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveInteractionFlag CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveInteractionFlag CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveInteractionFlag(data, ref offset);
        }

        public string InteractionFlag { get { return GetString(PropertiesOffset + 0); } }
        public string InteractionTarget { get { return GetString(PropertiesOffset + 4); } }

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

        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return null; } }
    }
}
