namespace PgJsonObjects
{
    public interface IPgEquipmentBoostServerInfoEffect
    {
        ItemEffect Boost { get; }
        float AttributeEffect { get; }
        float? RawAttributeEffect { get; }
    }
}
