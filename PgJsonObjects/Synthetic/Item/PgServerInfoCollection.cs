using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgServerInfoCollection : List<IPgServerInfo>, IPgServerInfoCollection
    {
        public static PgServerInfo CreateItem(byte[] data, ref int offset)
        {
            return new PgServerInfo(data, ref offset);
        }

        public static IPgServerInfoCollection CreateSingleOrEmptyList(IPgServerInfo item)
        {
            PgServerInfoCollection Result = new PgServerInfoCollection();
            if (item != null)
                Result.Add(item);

            return Result;
        }
    }
}
