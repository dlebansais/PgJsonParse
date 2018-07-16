using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeCostCollection : List<IPgRecipeCost>, IPgRecipeCostCollection, ISerializableJsonObjectCollection
    {
    }
}
