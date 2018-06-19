using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RewardCollection : List<Reward>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgReward CreateItem(byte[] data, ref int offset)
        {
            return new PgReward(data, ref offset);
        }
    }
}
