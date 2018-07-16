using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemCollection : List<IPgItem>, IPgItemCollection, ISerializableJsonObjectCollection
    {
    }
}
