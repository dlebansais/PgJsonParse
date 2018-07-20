namespace PgJsonObjects
{
    public interface IPgAdvancementTable : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        int Id { get; }
        string InternalName { get; }
        string FriendlyName { get; }
    }
}
