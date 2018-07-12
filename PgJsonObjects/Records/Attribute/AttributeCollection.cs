using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AttributeCollection : List<IPgAttribute>, ISerializableJsonObjectCollection
    {
        /*
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static PgAttribute CreateItem(byte[] data, ref int offset)
        {
            PgAttribute Result = new PgAttribute(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
