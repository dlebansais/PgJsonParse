using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerTierCollection : List<IPgPowerTier>, IPgPowerTierCollection, ISerializableJsonObjectCollection
    {
    }
}
