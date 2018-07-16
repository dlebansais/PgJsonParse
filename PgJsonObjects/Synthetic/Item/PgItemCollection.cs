using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemCollection : List<IPgItem>, IPgItemCollection
    {
        public static PgItem CreateItem(byte[] data, ref int offset)
        {
            PgItem Result = new PgItem(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
