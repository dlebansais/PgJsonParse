namespace PgJsonObjects
{
    public interface IPgItemAttributeLink
    {
        string AttributeName { get; }
        float AttributeEffect { get; }
        IPgAttribute Link { get; }
    }
}
