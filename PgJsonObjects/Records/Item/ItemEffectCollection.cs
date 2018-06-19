using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemEffectCollection : List<ItemEffect>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IGenericPgObject CreateItem(byte[] data, ref int offset)
        {
            bool IsSimple = (BitConverter.ToInt32(data, offset) == 0);
            offset += 4;

            if (IsSimple)
                return new PgItemSimpleEffect(data, ref offset);
            else
                return new PgItemAttributeLink(data, ref offset);
        }
    }
}
