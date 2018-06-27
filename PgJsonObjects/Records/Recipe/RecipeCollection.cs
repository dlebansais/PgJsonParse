using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCollection : List<IPgRecipe>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgRecipe CreateItem(byte[] data, ref int offset)
        {
            return new PgRecipe(data, ref offset);
        }
    }
}
