namespace PgJsonObjects
{
    public class PgIntervalServerInfoEffect : GenericPgObject<PgIntervalServerInfoEffect>, IPgServerInfoEffect, IPgIntervalServerInfoEffect
    {
        public PgIntervalServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgIntervalServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgIntervalServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgIntervalServerInfoEffect(data, ref offset);
        }

        public int LowValue { get { return RawLowValue.HasValue ? RawLowValue.Value : 0; } }
        public int? RawLowValue { get { return GetInt(4); } }
        public int HighValue { get { return RawHighValue.HasValue ? RawHighValue.Value : 0; } }
        public int? RawHighValue { get { return GetInt(8); } }
    }
}
