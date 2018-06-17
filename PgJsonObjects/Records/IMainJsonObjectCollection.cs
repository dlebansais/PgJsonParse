using System.Collections;

namespace PgJsonObjects
{
    public interface IMainJsonObjectCollection : ICollection
    {
        IMainJsonObject GetAt(int index);
    }
}
