namespace PgJsonObjects
{
    public interface IPgIntervalServerInfoEffect
    {
        int LowValue { get; }
        int? RawLowValue { get; }
        int HighValue { get; }
        int? RawHighValue { get; }
    }
}
