using System.Collections.Generic;

namespace PgJsonObjects
{
    public class NpcPreferenceCollection : List<IPgNpcPreference>, IPgNpcPreferenceCollection, ISerializableJsonObjectCollection
    {
    }
}
