using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerTierCollection : List<IPgPowerTier>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static IPgPowerTier CreateItem(byte[] data, ref int offset)
        {
            return new PgPowerTier(data, ref offset);
        }
    }
}
