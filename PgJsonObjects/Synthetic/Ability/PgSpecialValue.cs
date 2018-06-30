using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgSpecialValue : GenericPgObject<PgSpecialValue>, IPgSpecialValue
    {
        public PgSpecialValue(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgSpecialValue CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgSpecialValue CreateNew(byte[] data, ref int offset)
        {
            return new PgSpecialValue(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Label { get { return GetString(4); } }
        public string Suffix { get { return GetString(8); } }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get { return GetDouble(12); } }
        public bool DisplayAsPercent { get { return RawDisplayAsPercent.HasValue && RawDisplayAsPercent.Value; } }
        public bool? RawDisplayAsPercent { get { return GetBool(16, 0); } }
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        public bool? RawSkipIfZero { get { return GetBool(16, 2); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
