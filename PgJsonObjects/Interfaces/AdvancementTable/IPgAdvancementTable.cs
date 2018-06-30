namespace PgJsonObjects
{
    public interface IPgAdvancementTable : IJsonKey, IObjectContentGenerator
    {
        string InternalName { get; }
    }
}
