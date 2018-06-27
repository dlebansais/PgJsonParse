using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardXpCollection : List<IPgQuestRewardXp>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgQuestRewardXp CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestRewardXp(data, ref offset);
        }
    }
}
