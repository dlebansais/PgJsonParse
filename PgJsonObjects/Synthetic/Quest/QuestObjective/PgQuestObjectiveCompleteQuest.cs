using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveCompleteQuest : PgQuestObjective<PgQuestObjectiveCompleteQuest>, IPgQuestObjectiveCompleteQuest
    {
        public PgQuestObjectiveCompleteQuest(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveCompleteQuest CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveCompleteQuest CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveCompleteQuest(data, ref offset);
        }

        public string Target { get { return GetString(PropertiesOffset + 0); } }

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
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Target } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return new List<IBackLinkable>();
        }

        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return null; } }
    }
}
