namespace PgJsonObjects
{
    public class PgEquipmentBoostServerInfoEffect : GenericPgObject, IPgEquipmentBoostServerInfoEffect
    {
        public PgEquipmentBoostServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public ItemEffect Boost { get { return GetObject(4, ref _Boost); } } private ItemEffect _Boost;
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get { return (float)GetDouble(8); } }
    }
}
