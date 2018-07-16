using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AttributeCollection : List<IPgAttribute>, IPgAttributeCollection, ISerializableJsonObjectCollection
    {
        public List<string> ToKeyList
        {
            get
            {
                List<string> Result = new List<string>();

                foreach (IPgAttribute Item in this)
                    Result.Add(Item.Key);

                return Result;
            }
        }
    }
}
