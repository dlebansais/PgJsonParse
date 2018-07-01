using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardCollection : List<IPgQuestReward>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static PgQuestReward CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestReward(data, ref offset);
        }
    }
}
