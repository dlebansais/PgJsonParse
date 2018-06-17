namespace PgJsonObjects
{
    public interface IPgServerInfoEffect
    {
        int Level { get; }
        int? RawLevel { get; }
        ServerInfoEffectType Type { get; }
    }
}
