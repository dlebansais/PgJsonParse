using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgXpTableLevelCollection : List<IPgXpTableLevel>, IPgXpTableLevelCollection
    {
        public static PgXpTableLevel CreateItem(byte[] data, ref int offset)
        {
            return new PgXpTableLevel(data, ref offset);
        }
    }
}
