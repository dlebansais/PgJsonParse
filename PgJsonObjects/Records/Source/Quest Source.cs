using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestSource : GenericSource<QuestSource>, IPgQuestSource
    {
        #region Init
        public QuestSource(IPgQuest Quest)
        {
            this.Quest = Quest;
        }

        protected override int Type { get { return (int)SourceType.Quest; } }
        #endregion

        #region Properties
        public IPgQuest Quest { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            SerializeJsonObjectInternalProlog(data, ref offset);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Quest as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
