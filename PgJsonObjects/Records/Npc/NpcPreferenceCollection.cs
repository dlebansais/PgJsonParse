using System.Collections.Generic;

namespace PgJsonObjects
{
    public class NpcPreferenceCollection : List<NpcPreference>, ISerializableJsonObjectCollection
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
