using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestCollection : List<Quest>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgQuest CreateItem(byte[] data, int offset)
        {
            return new PgQuest(data, offset);
        }
    }
}
