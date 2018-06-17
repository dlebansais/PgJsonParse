using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RewardCollection : List<Reward>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgReward CreateItem(byte[] data, int offset)
        {
            return new PgReward(data, offset);
        }
    }
}
