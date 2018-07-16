using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCollection : List<IPgRecipe>, IPgRecipeCollection, ISerializableJsonObjectCollection
    {
    }
}
