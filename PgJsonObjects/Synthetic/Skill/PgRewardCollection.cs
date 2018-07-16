using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRewardCollection : List<IPgReward>, IPgRewardCollection
    {
        public static PgReward CreateItem(byte[] data, ref int offset)
        {
            return new PgReward(data, ref offset);
        }
    }
}
