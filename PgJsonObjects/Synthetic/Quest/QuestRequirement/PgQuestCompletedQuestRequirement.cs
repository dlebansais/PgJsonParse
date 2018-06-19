﻿namespace PgJsonObjects
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

        public QuestCollection QuestList { get { return GetObjectList(4, ref _QuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _QuestList;
    }
}
