using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardFavor : SerializableJsonObject, IPgQuestRewardFavor
    {
        //public IPgGameNpc Npc { get; set; }
        public int Favor { get { return RawFavor.HasValue ? RawFavor.Value : 0; } }
        public int? RawFavor { get; set; }

        public string RawNpcName { get; set; }
        public bool IsNpcParsed { get; set; }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(RawNpcName, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddInt(RawFavor, data, ref offset, BaseOffset, 4);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 8, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
