using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardXpCollection : List<IPgQuestRewardXp>, IPgQuestRewardXpCollection
    {
        public static PgQuestRewardXp CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestRewardXp(data, ref offset);
        }
    }
}
