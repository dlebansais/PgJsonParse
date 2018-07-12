using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemCollection : List<IPgItem>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static PgItem CreateItem(byte[] data, ref int offset)
        {
            PgItem Result = new PgItem(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
