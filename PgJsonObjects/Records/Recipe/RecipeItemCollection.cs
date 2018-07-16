using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeItemCollection : List<IPgRecipeItem>, IPgRecipeItemCollection, ISerializableJsonObjectCollection
    {
    }
}
