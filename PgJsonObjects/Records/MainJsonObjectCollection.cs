using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MainJsonObjectCollection<TJson, TPg, TI> : List<TI>, IMainJsonObjectCollection<TI>
        where TJson : MainJsonObject<TJson>, TI, new()
        where TPg: IMainPgObject, TI
    {
        /*
        public IMainPgObject CreateItem(byte[] data, ref int offset)
        {
            return GenericPgObject<TPg>.CreateObject<TPg>(data, ref offset);
        }*/
    }
}
