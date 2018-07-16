using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AttributeCollection : List<IPgAttribute>, IPgAttributeCollection, ISerializableJsonObjectCollection
    {
    }
}
