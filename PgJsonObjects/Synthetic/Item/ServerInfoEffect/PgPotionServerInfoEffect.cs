namespace PgJsonObjects
{
    public class PgPotionServerInfoEffect : GenericPgObject, IPgPotionServerInfoEffect
    {
        public PgPotionServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgPotionServerInfoEffect(data, offset);
        }

        public string EffectString { get { return GetString(0); } }
    }
}
