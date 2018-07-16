using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XpTableLevelCollection : List<IPgXpTableLevel>, IPgXpTableLevelCollection, ISerializableJsonObjectCollection
    {
    }
}
