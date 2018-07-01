using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeResultEffectCollection : List<IPgRecipeResultEffect>, ISerializableJsonObjectCollection
    {
        /*ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static PgRecipeResultEffect CreateItem(byte[] data, ref int offset)
        {
            return new PgRecipeResultEffect(data, ref offset);
        }
    }
}
