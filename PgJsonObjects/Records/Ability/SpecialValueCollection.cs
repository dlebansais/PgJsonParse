using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SpecialValueCollection : List<IPgSpecialValue>, IPgSpecialValueCollection, ISerializableJsonObjectCollection
    {
    }
}
