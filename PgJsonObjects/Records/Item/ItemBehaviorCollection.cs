using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemBehaviorCollection : List<ItemBehavior>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgItemBehavior CreateItem(byte[] data, int offset)
        {
            return new PgItemBehavior(data, offset);
        }
    }
}
