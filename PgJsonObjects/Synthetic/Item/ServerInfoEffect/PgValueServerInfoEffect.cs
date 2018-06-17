namespace PgJsonObjects
{
    public class PgValueServerInfoEffect : GenericPgObject<PgValueServerInfoEffect>, IPgValueServerInfoEffect
    {
        public PgValueServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgValueServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgValueServerInfoEffect(data, offset);
        }

        public int Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public int? RawValue { get { return GetInt(0); } }
    }
}
