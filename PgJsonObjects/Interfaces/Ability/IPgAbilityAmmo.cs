namespace PgJsonObjects
{
    public interface IPgAbilityAmmo : IJsonKey, IObjectContentGenerator
    {
        ItemKeyword Keyword { get; }
        int Count { get; }
        int? RawCount { get; }
    }
}
