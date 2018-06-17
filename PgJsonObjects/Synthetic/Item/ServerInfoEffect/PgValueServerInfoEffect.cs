namespace PgJsonObjects
{
    public class PgValueServerInfoEffect : GenericPgObject, IPgValueServerInfoEffect
    {
        public PgValueServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgValueServerInfoEffect(data, offset);
        }

        public int Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public int? RawValue { get { return GetInt(0); } }
    }
}
