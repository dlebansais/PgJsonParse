using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgBackLinkableCollection : IList
    {
    }

    public interface IPgBackLinkableCollection<TI> : IList<TI>, IPgBackLinkableCollection
        where TI: IBackLinkable
    {
    }
}
