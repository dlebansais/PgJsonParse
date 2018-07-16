using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerEffectCollection : List<IPgPowerEffect>, IPgPowerEffectCollection, ISerializableJsonObjectCollection
    {
    }
}
