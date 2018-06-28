using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerEffectCollection : List<IGenericPgObject>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IGenericPgObject CreateItem(byte[] data, ref int offset)
        {
            int SimpleValue = BitConverter.ToInt32(data, offset);
            bool IsSimple = (SimpleValue == 0);

            if (IsSimple)
                return new PgPowerSimpleEffect(data, ref offset);
            else
                return new PgPowerAttributeLink(data, ref offset);
        }
    }
}
