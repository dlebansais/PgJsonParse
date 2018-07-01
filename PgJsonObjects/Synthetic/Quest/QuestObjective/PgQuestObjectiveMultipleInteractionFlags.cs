using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveMultipleInteractionFlags : PgQuestObjective<PgQuestObjectiveMultipleInteractionFlags>, IPgQuestObjectiveMultipleInteractionFlags
    {
        public PgQuestObjectiveMultipleInteractionFlags(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveMultipleInteractionFlags CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveMultipleInteractionFlags CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveMultipleInteractionFlags(data, ref offset);
        }

        public List<string> InteractionFlagList { get { return GetStringList(PropertiesOffset + 0, ref _InteractionFlagList); } } private List<string> _InteractionFlagList;
        public IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return GetObject(PropertiesOffset + 4, ref _QuestObjectiveRequirement, PgQuestObjectiveRequirement.CreateNew); } } private IPgQuestObjectiveRequirement _QuestObjectiveRequirement;

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
            { "InteractionFlags", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => InteractionFlagList } },
        }; } }
    }
}
