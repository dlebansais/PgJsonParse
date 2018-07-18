namespace PgJsonObjects
{
    public interface IPgItemAttributeLink
    {
        string AttributeName { get; }
        float AttributeEffect { get; }
        IPgAttribute Link { get; }
        FloatFormat AttributeEffectFormat { get; }

        string FriendlyNameAndEffect { get; }
        string FriendlyName { get; }
        string FriendlyEffect { get; }
    }
}
