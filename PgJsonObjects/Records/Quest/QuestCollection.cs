using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestCollection : List<IPgQuest>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgQuest CreateItem(byte[] data, ref int offset)
        {
            return new PgQuest(data, ref offset);
        }
    }
}
