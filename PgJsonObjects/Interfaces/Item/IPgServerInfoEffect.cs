using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgServerInfoEffect
    {
        IList<IBackLinkable> GetLinkBack();
    }
}
