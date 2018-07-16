using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemBehaviorCollection : List<IPgItemBehavior>, IPgItemBehaviorCollection
    {
        public static PgItemBehavior CreateItem(byte[] data, ref int offset)
        {
            PgItemBehavior Result = new PgItemBehavior(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
