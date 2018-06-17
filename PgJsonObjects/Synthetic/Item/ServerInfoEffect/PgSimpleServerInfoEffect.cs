namespace PgJsonObjects
{
    public class PgSimpleServerInfoEffect : GenericPgObject, IPgSimpleServerInfoEffect
    {
        public PgSimpleServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgSimpleServerInfoEffect(data, offset);
        }

        public string EffectParameter { get { return GetString(4); } }
    }
}
