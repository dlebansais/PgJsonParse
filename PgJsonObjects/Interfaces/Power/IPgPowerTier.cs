using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgPowerTier
    {
        List<PowerEffect> EffectList { get; }
    }
}
