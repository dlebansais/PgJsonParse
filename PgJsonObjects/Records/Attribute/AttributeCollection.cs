using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AttributeCollection : List<IPgAttribute>, ISerializableJsonObjectCollection
    {
        /*
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static PgAttribute CreateItem(byte[] data, ref int offset)
        {
            return new PgAttribute(data, ref offset);
        }
    }
}
