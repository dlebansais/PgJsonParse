using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementCollection : List<IPgAbilityRequirement>, IPgAbilityRequirementCollection, ISerializableJsonObjectCollection
    {
    }
}
