using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemBehaviorCollection : List<IPgItemBehavior>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static PgItemBehavior CreateItem(byte[] data, ref int offset)
        {
            PgItemBehavior Result = new PgItemBehavior(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
