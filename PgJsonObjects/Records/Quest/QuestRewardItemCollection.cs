using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardItemCollection : List<QuestRewardItem>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgQuestRewardItem CreateItem(byte[] data, int offset)
        {
            return new PgQuestRewardItem(data, offset);
        }
    }
}
