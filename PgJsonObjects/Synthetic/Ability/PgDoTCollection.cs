using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgDoTCollection : List<IPgDoT>, IPgDoTCollection
    {
        public static PgDoT CreateItem(byte[] data, ref int offset)
        {
            PgDoT Result = new PgDoT(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
