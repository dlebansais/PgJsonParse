using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgValueServerInfoEffect : GenericPgObject<PgValueServerInfoEffect>, IPgServerInfoEffect, IPgValueServerInfoEffect
    {
        public PgValueServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgValueServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgValueServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgValueServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public int Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public int? RawValue { get { return GetInt(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
