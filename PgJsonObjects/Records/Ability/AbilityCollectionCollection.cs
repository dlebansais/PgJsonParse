using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityCollection : List<Ability>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgAbility CreateItem(byte[] data, int offset)
        {
            return new PgAbility(data, offset);
        }
    }
}
