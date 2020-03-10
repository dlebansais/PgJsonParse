using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardItemCollection : List<IPgQuestRewardItem>, IPgQuestRewardItemCollection
    {
        public static PgQuestRewardItem CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestRewardItem(data, ref offset);
        }
    }
}
