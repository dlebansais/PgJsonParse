namespace PgJsonObjects
{
    public class PgSpecialValue : GenericPgObject<PgSpecialValue>, IPgSpecialValue
    {
        public PgSpecialValue(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgSpecialValue CreateItem(byte[] data, int offset)
        {
            return new PgSpecialValue(data, offset);
        }

        public string Label { get { return GetString(0); } }
        public string Suffix { get { return GetString(4); } }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get { return GetDouble(8); } }
        public bool DisplayAsPercent { get { return RawDisplayAsPercent.HasValue && RawDisplayAsPercent.Value; } }
        public bool? RawDisplayAsPercent { get { return GetBool(12, 0); } }
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        public bool? RawSkipIfZero { get { return GetBool(12, 2); } }
    }
}
