using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgSpecialValueCollection : List<IPgSpecialValue>, IPgSpecialValueCollection
    {
        public static PgSpecialValue CreateItem(byte[] data, ref int offset)
        {
            PgSpecialValue Result = new PgSpecialValue(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
