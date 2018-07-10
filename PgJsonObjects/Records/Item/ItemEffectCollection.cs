using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemEffectCollection : List<IPgItemEffect>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IPgItemEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static IPgItemEffect CreateNew(byte[] data, ref int offset)
        {
            int Type = BitConverter.ToInt32(data, offset);
            bool IsSimple = (Type == 0);

            if (IsSimple)
                return PgItemSimpleEffect.CreateNew(data, ref offset);
            else
                return PgItemAttributeLink.CreateNew(data, ref offset);
        }
    }
}
