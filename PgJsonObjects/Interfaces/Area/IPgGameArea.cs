namespace PgJsonObjects
{
    public interface IPgGameArea
    {
        string FriendlyName { get; }
        string ShortFriendlyName { get; }
        MapAreaName KeyArea { get; }
    }
}
