using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DoTCollection : List<IPgDoT>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index];
        }*/

        public static PgDoT CreateItem(byte[] data, ref int offset)
        {
            return new PgDoT(data, ref offset);
        }
    }
}
