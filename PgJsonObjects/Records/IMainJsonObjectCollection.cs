using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IMainJsonObjectCollection : IList
    {
    }

    public interface IMainJsonObjectCollection<TI> : IList<TI>, IMainJsonObjectCollection
    {
    }
}
