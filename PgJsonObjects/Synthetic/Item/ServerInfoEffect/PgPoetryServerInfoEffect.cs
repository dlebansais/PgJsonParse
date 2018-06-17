namespace PgJsonObjects
{
    public class PgPoetryServerInfoEffect : GenericPgObject<PgPoetryServerInfoEffect>, IPgPoetryServerInfoEffect
    {
        public PgPoetryServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgPoetryServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgPoetryServerInfoEffect(data, offset);
        }

        public int PoetryXpValue { get { return RawPoetryXpValue.HasValue ? RawPoetryXpValue.Value : 0; } }
        public int? RawPoetryXpValue { get { return GetInt(4); } }
        public int RecitalXpValue { get { return RawRecitalXpValue.HasValue ? RawRecitalXpValue.Value : 0; } }
        public int? RawRecitalXpValue { get { return GetInt(8); } }
    }
}
