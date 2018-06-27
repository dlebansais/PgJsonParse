using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityCollection : List<IPgAbility>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgAbility CreateItem(byte[] data, ref int offset)
        {
            return new PgAbility(data, ref offset);
        }
    }
}
