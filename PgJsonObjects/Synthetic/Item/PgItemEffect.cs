using System;

namespace PgJsonObjects
{
    public abstract class PgItemEffect<TPg> : GenericPgObject<TPg>, IPgItemEffect
        where TPg : IDeserializablePgObject
    {
        public PgItemEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public abstract string AsEffectString();
    }
}
