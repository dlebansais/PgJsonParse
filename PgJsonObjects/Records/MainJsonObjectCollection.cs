using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MainJsonObjectCollection<TJson, TPg> : List<TJson>, IMainJsonObjectCollection
        where TJson : MainJsonObject<TJson>, new()
        where TPg: IMainPgObject
    {
        public IMainPgObject CreateItem(byte[] data, ref int offset)
        {
            return GenericPgObject<TPg>.CreateObject<TPg>(data, ref offset);
        }
    }
}
