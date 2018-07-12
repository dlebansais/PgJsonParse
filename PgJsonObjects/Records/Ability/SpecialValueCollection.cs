using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SpecialValueCollection : List<IPgSpecialValue>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index];
        }*/

        public static PgSpecialValue CreateItem(byte[] data, ref int offset)
        {
            PgSpecialValue Result = new PgSpecialValue(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}
