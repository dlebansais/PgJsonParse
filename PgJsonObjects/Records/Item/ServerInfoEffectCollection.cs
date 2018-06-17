using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ServerInfoEffectCollection : List<ServerInfoEffect>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static PgServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgServerInfoEffect(data, offset);
        }
    }
}
