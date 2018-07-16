using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemBehaviorCollection : List<IPgItemBehavior>, IPgItemBehaviorCollection, ISerializableJsonObjectCollection
    {
        public static PgItemBehavior CreateItem(byte[] data, ref int offset)
        {
            PgItemBehavior Result = new PgItemBehavior(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
