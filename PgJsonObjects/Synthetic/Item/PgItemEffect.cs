using System;
using System.Collections.Generic;

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
        public abstract IList<IBackLinkable> GetLinkBack();
    }
}
