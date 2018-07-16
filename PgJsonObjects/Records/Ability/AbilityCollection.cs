using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityCollection : List<IPgAbility>, IPgAbilityCollection, ISerializableJsonObjectCollection
    {
    }
}
