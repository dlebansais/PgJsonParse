using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgGuildQuestCompletedQuestRequirement : GenericPgObject<PgGuildQuestCompletedQuestRequirement>, IPgGuildQuestCompletedQuestRequirement
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

        public override string Key { get { return GetString(4); } }
        public QuestCollection QuestList { get { return GetObjectList(8, ref _QuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _QuestList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
