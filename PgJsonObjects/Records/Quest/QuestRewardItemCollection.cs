using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardItemCollection : List<IPgQuestRewardItem>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgQuestRewardItem CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestRewardItem(data, ref offset);
        }
    }
}
