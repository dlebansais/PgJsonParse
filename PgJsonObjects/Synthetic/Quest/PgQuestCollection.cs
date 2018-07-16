using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestCollection : List<IPgQuest>, IPgQuestCollection
    {
        public static PgQuest CreateItem(byte[] data, ref int offset)
        {
            return new PgQuest(data, ref offset);
        }
    }
}
