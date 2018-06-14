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
    }
}
