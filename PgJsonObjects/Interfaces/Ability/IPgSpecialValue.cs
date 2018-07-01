namespace PgJsonObjects
{
    public interface IPgSpecialValue
    {
        string Label { get; }
        string Suffix { get; }
        double Value { get; }
        double? RawValue { get; }
        bool DisplayAsPercent { get; }
        bool? RawDisplayAsPercent { get; }
        bool SkipIfZero { get; }
        bool? RawSkipIfZero { get; }
        AttributeCollection AttributesThatDeltaList { get; }
        AttributeCollection AttributesThatModList { get; }
        AttributeCollection AttributesThatModBaseList { get; }
        bool RawAttributesThatDeltaListIsEmpty { get; }
        bool RawAttributesThatModListIsEmpty { get; }
        bool RawAttributesThatModBaseListIsEmpty { get; }
    }
}
