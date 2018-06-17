namespace PgJsonObjects
{
    public class PgPotionServerInfoEffect : GenericPgObject<PgPotionServerInfoEffect>, IPgPotionServerInfoEffect
    {
        public PgPotionServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgPotionServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgPotionServerInfoEffect(data, offset);
        }

        public string EffectString { get { return GetString(0); } }
    }
}
