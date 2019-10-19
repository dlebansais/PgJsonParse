using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardCurrencyCollection : List<IPgQuestRewardCurrency>, IPgQuestRewardCurrencyCollection
    {
        public static PgQuestRewardCurrency CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestRewardCurrency(data, ref offset);
        }
    }
}
