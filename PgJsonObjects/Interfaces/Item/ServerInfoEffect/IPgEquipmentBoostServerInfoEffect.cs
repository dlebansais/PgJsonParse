namespace PgJsonObjects
{
    public interface IPgEquipmentBoostServerInfoEffect
    {
        IPgItemEffect Boost { get; }
        float AttributeEffect { get; }
        float? RawAttributeEffect { get; }
    }
}
