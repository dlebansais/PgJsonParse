using System.Collections.Generic;

namespace PgJsonObjects
{
    public class NpcPreferenceCollection : List<IPgNpcPreference>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgNpcPreference CreateItem(byte[] data, ref int offset)
        {
            return new PgNpcPreference(data, ref offset);
        }
    }
}
