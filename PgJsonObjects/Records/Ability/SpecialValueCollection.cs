using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SpecialValueCollection : List<SpecialValue>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index];
        }

        public static PgSpecialValue CreateItem(byte[] data, ref int offset)
        {
            return new PgSpecialValue(data, ref offset);
        }
    }
}
