﻿namespace PgJsonObjects
{
    public class PgSimpleServerInfoEffect : GenericPgObject<PgSimpleServerInfoEffect>, IPgSimpleServerInfoEffect
    {
        public PgSimpleServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgSimpleServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgSimpleServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgSimpleServerInfoEffect(data, ref offset);
        }

        public string EffectParameter { get { return GetString(4); } }
    }
}
