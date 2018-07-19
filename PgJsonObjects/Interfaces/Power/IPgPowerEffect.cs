using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgPowerEffect
    {
        string AsEffectString();
        IList<IBackLinkable> GetLinkBack();
    }
}
