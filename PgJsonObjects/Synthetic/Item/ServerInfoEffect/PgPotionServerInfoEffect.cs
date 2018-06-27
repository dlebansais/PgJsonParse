namespace PgJsonObjects
{
    public class PgPotionServerInfoEffect : GenericPgObject<PgPotionServerInfoEffect>, IPgServerInfoEffect, IPgPotionServerInfoEffect
    {
        public PgPotionServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPotionServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPotionServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgPotionServerInfoEffect(data, ref offset);
        }

        public string EffectString { get { return GetString(0); } }
    }
}
