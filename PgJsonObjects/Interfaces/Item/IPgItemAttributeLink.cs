namespace PgJsonObjects
{
    public interface IPgItemAttributeLink
    {
        float AttributeEffect { get; }
        IPgAttribute Link { get; }
    }
}
