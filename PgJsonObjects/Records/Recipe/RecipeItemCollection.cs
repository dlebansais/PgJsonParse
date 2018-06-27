using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeItemCollection : List<IPgRecipeItem>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgRecipeItem CreateItem(byte[] data, ref int offset)
        {
            return new PgRecipeItem(data, ref offset);
        }
    }
}
