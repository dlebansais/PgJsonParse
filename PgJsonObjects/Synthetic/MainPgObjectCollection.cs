using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MainPgObjectCollection<TPg, TI> : List<TI>, IMainPgObjectCollection<TI>
        where TPg : IMainPgObject, TI
    {
        /*
        public IMainPgObject CreateItem(byte[] data, ref int offset)
        {
            return GenericPgObject<TPg>.CreateObject<TPg>(data, ref offset);
        }*/
    }
}
