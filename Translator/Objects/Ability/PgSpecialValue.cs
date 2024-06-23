namespace PgObjects
{
    public class PgSpecialValue
    {
        public string Label { get; set; } = string.Empty;
        public string Suffix { get; set; } = string.Empty;
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
        public PgAttributeCollection AttributesThatDeltaBaseList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaList { get; set; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModList { get; set; } = new PgAttributeCollection();
        public DisplayType DisplayType { get; set; }
        public string SkipIfThisAttributeIsZero { get; set; } = string.Empty;
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        public bool? RawSkipIfZero { get; set; }
    }
}
