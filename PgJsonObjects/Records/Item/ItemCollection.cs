using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemCollection : List<Item>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgItem CreateItem(byte[] data, int offset)
        {
            return new PgItem(data, offset);
        }
    }
}
