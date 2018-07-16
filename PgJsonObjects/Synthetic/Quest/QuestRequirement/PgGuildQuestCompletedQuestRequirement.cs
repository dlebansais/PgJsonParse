using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgGuildQuestCompletedQuestRequirement : PgQuestRequirement<PgGuildQuestCompletedQuestRequirement>, IPgGuildQuestCompletedQuestRequirement
    {
        public PgGuildQuestCompletedQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgGuildQuestCompletedQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgGuildQuestCompletedQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgGuildQuestCompletedQuestRequirement(data, ref offset);
        }

        public IPgQuestCollection QuestList { get { return GetObjectList(PropertiesOffset + 0, ref _QuestList, PgQuestCollection.CreateItem, () => new PgQuestCollection()); } } private IPgQuestCollection _QuestList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Quest", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestList.Count > 0 ? QuestList[0].InternalName : null } },
        }; } }
    }
}
