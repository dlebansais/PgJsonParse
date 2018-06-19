using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XpTableLevelCollection : List<XpTableLevel>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgXpTableLevel CreateItem(byte[] data, ref int offset)
        {
            return new PgXpTableLevel(data, ref offset);
        }
    }
}
