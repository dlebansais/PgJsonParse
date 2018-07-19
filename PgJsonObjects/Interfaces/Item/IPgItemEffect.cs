using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgItemEffect
    {
        string AsEffectString();
        IList<IBackLinkable> GetLinkBack();
    }
}
