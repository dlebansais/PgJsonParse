using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardFavorCollection : List<IPgQuestRewardFavor>, IPgQuestRewardFavorCollection
    {
        public static PgQuestRewardFavor CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestRewardFavor(data, ref offset);
        }
    }
}
