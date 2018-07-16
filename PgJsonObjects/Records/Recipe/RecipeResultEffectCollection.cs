using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeResultEffectCollection : List<IPgRecipeResultEffect>, IPgRecipeResultEffectCollection, ISerializableJsonObjectCollection
    {
    }
}
