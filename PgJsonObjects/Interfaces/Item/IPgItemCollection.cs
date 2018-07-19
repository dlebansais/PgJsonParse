using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgItemCollection : IList<IPgItem>, IPgCollection, IPgBackLinkableCollection<IPgItem>
    {
    }
}
