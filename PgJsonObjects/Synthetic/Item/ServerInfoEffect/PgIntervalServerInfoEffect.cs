﻿namespace PgJsonObjects
{
    public class PgIntervalServerInfoEffect : GenericPgObject, IPgIntervalServerInfoEffect
    {
        public PgIntervalServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgIntervalServerInfoEffect(data, offset);
        }

        public int LowValue { get { return RawLowValue.HasValue ? RawLowValue.Value : 0; } }
        public int? RawLowValue { get { return GetInt(4); } }
        public int HighValue { get { return RawHighValue.HasValue ? RawHighValue.Value : 0; } }
        public int? RawHighValue { get { return GetInt(8); } }
    }
}
