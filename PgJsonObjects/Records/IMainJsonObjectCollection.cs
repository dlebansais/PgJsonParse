using System.Collections;

namespace PgJsonObjects
{
    public interface IMainJsonObjectCollection : IList
    {
        IMainPgObject CreateItem(byte[] data, ref int offset);
    }
}
