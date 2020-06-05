namespace PgJsonObjects
{
    public class PgSpecialValue
    {
        public string Label { get; set; } = string.Empty;
        public string Suffix { get; set; } = string.Empty;
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
        public PgAttributeCollection AttributesThatDeltaList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModBaseList { get; } = new PgAttributeCollection();
        public DisplayType DisplayType { get; set; }
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        public bool? RawSkipIfZero { get; set; }
}
}
