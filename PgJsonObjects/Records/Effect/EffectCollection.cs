using System.Collections.Generic;

namespace PgJsonObjects
{
    public class EffectCollection : List<IPgEffect>, IPgEffectCollection, ISerializableJsonObjectCollection
    {
    }
}
