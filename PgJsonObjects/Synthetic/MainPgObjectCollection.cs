using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MainPgObjectCollection<TPg> : List<TPg>, IMainPgObjectCollection
        where TPg : IMainPgObject
    {
        public IMainPgObject CreateItem(byte[] data, ref int offset)
        {
            return GenericPgObject<TPg>.CreateObject<TPg>(data, ref offset);
        }
    }
}
