using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestCompletedQuestRequirement : GenericPgObject<PgQuestCompletedQuestRequirement>, IPgQuestCompletedQuestRequirement
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

        public override string Key { get { return GetString(4); } }
        public QuestCollection QuestList { get { return GetObjectList(8, ref _QuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _QuestList;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
