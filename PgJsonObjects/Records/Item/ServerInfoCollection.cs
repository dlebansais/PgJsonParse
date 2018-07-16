using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ServerInfoCollection : List<IPgServerInfo>, IPgServerInfoCollection, ISerializableJsonObjectCollection
    {
        public static IPgServerInfoCollection CreateSingleOrEmptyList(IPgServerInfo item)
        {
            ServerInfoCollection Result = new ServerInfoCollection();
            if (item != null)
                Result.Add(item);

            return Result;
        }
    }
}
