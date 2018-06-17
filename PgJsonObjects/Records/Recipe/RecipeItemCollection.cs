using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeItemCollection : List<RecipeItem>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgRecipeItem CreateItem(byte[] data, int offset)
        {
            return new PgRecipeItem(data, offset);
        }
    }
}
