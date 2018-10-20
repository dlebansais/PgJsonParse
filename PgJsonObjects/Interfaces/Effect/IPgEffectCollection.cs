using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgEffectCollection : IList<IPgEffect>, IPgCollection, IPgBackLinkableCollection<IPgEffect>
    {
    }
}
