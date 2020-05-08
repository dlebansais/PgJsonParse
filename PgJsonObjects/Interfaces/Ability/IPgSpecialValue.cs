namespace PgJsonObjects
{
    public interface IPgSpecialValue
    {
        string Label { get; }
        string Suffix { get; set; }
        double Value { get; }
        double? RawValue { get; }
        bool DisplayAsPercent { get; }
        bool? RawDisplayAsPercent { get; }
        bool SkipIfZero { get; }
        bool? RawSkipIfZero { get; }
        IPgAttributeCollection AttributesThatDeltaList { get; }
        IPgAttributeCollection AttributesThatModList { get; }
        IPgAttributeCollection AttributesThatModBaseList { get; }
        bool RawAttributesThatDeltaListIsEmpty { get; }
        bool RawAttributesThatModListIsEmpty { get; }
        bool RawAttributesThatModBaseListIsEmpty { get; }
    }
}
