using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCollection : List<Recipe>, ISerializableJsonObjectCollection
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
