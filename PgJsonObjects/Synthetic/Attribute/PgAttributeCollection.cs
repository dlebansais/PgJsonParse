using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAttributeCollection : List<IPgAttribute>, IPgAttributeCollection
    {
        public static PgAttribute CreateItem(byte[] data, ref int offset)
        {
            PgAttribute Result = new PgAttribute(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
