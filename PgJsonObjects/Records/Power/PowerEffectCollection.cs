using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerEffectCollection : List<PowerEffect>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IGenericPgObject CreateItem(byte[] data, int offset)
        {
            bool IsSimple = (BitConverter.ToInt32(data, offset) == 0);
            offset += 4;

            if (IsSimple)
                return new PgPowerSimpleEffect(data, offset);
            else
                return new PgPowerAttributeLink(data, offset);
        }
    }
}
