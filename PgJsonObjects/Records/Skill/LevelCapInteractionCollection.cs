using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LevelCapInteractionCollection : List<LevelCapInteraction>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgLevelCapInteraction CreateItem(byte[] data, ref int offset)
        {
            return new PgLevelCapInteraction(data, ref offset);
        }
    }
}
