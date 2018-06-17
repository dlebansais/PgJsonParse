using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCostCollection : List<RecipeCost>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgRecipeCost CreateItem(byte[] data, int offset)
        {
            return new PgRecipeCost(data, offset);
        }
    }
}
