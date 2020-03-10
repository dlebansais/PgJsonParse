using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardLevelCollection : List<IPgQuestRewardLevel>, IPgQuestRewardLevelCollection
    {
        public static PgQuestRewardLevel CreateItem(byte[] data, ref int offset)
        {
            return new PgQuestRewardLevel(data, ref offset);
        }
    }
}
