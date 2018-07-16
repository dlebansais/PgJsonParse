using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemEffectCollection : List<IPgItemEffect>, IPgItemEffectCollection, ISerializableJsonObjectCollection
    {
    }
}
