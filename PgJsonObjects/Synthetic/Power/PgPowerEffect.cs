using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class PgPowerEffect<TPg> : GenericPgObject<TPg>, IPgPowerEffect
        where TPg : IDeserializablePgObject
    {
        public PgPowerEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public abstract string AsEffectString();
        public abstract IList<IBackLinkable> GetLinkBack();
    }
}
