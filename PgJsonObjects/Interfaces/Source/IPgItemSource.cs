namespace PgJsonObjects
{
    public interface IPgItemSource : IPgGenericSource
    {
        IPgItem Item { get; }
    }
}
