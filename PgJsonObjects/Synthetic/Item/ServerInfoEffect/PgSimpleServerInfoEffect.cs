namespace PgJsonObjects
{
    public class PgSimpleServerInfoEffect : GenericPgObject<PgSimpleServerInfoEffect>, IPgSimpleServerInfoEffect
    {
        public PgSimpleServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgSimpleServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgSimpleServerInfoEffect(data, offset);
        }

        public string EffectParameter { get { return GetString(4); } }
    }
}
