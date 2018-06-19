using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCostCollection : List<RecipeCost>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgRecipeCost CreateItem(byte[] data, ref int offset)
        {
            return PgRecipeCost.CreateNew(data, ref offset);
        }
    }
}
