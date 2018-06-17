using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeResultEffectCollection : List<RecipeResultEffect>
    {
        ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }
    }
}
