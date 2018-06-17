namespace PgJsonObjects
{
    public class PgSimpleServerInfoEffect : GenericPgObject, IPgSimpleServerInfoEffect
    {
        public PgSimpleServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string EffectParameter { get { return GetString(4); } }
    }
}
