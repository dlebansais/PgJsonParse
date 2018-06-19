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

        public static IPgItemEffect CreateNew(byte[] data, ref int offset)
        {
            int Type = BitConverter.ToInt32(data, offset);
            if (Type != 0)
                return PgItemAttributeLink.CreateNew(data, ref offset);
            else
                return new PgItemSimpleEffect(data, ref offset);
        }
    }
}
