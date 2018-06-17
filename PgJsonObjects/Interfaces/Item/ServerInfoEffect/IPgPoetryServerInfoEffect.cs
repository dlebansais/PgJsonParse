namespace PgJsonObjects
{
    public interface IPgPoetryServerInfoEffect
    {
        int PoetryXpValue { get; }
        int? RawPoetryXpValue { get; }
        int RecitalXpValue { get; }
        int? RawRecitalXpValue { get; }
    }
}
