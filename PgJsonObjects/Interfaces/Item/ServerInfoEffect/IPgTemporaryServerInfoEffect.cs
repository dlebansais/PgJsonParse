namespace PgJsonObjects
{
    public interface IPgTemporaryServerInfoEffect
    {
        IPgItemEffect Boost { get; }
        float AttributeEffect { get; }
        float? RawAttributeEffect { get; }
        int Duration { get; }
        int? RawDuration { get; }
    }
}
