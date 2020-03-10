using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardCollection : List<IPgQuestReward>, IPgQuestRewardCollection
    {
        public static PgQuestReward CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestReward(data, ref offset);
        }
    }
}
