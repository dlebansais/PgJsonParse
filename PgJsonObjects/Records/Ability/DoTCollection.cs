using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DoTCollection : List<DoT>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index];
        }

        public static PgDoT CreateItem(byte[] data, int offset)
        {
            return new PgDoT(data, offset);
        }
    }
}
