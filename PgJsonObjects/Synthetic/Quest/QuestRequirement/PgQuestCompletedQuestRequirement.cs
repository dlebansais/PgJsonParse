using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestCompletedQuestRequirement : PgQuestRequirement<PgQuestCompletedQuestRequirement>, IPgQuestCompletedQuestRequirement
    {
        public PgQuestCompletedQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestCompletedQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestCompletedQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestCompletedQuestRequirement(data, ref offset);
        }

        public IPgQuestCollection QuestList { get { return GetObjectList(PropertiesOffset + 0, ref _QuestList, PgQuestCollection.CreateItem, () => new PgQuestCollection()); } } private IPgQuestCollection _QuestList;
        private IList<IPgQuest> QuestListAsList { get { return QuestList; } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Quest", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestListAsList.Count > 0 ? QuestListAsList[0].InternalName : null } },
        }; } }
    }
}
