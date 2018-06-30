namespace PgJsonObjects
{
    public interface IPgGameArea : IJsonKey, IObjectContentGenerator
    {
        string FriendlyName { get; }
        string ShortFriendlyName { get; }
        MapAreaName KeyArea { get; }
    }
}
