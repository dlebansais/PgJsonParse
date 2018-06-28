using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IMainPgObjectCollection : IList
    {
    }

    public interface IMainPgObjectCollection<TI> : IList<TI>, IMainPgObjectCollection
    {
    }
}
