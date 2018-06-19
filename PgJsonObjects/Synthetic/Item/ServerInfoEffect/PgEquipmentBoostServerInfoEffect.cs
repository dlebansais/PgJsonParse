namespace PgJsonObjects
{
    public class PgEquipmentBoostServerInfoEffect : GenericPgObject<PgEquipmentBoostServerInfoEffect>, IPgEquipmentBoostServerInfoEffect
    {
        public PgEquipmentBoostServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgEquipmentBoostServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgEquipmentBoostServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgEquipmentBoostServerInfoEffect(data, ref offset);
        }

        public IPgItemEffect Boost { get { return GetObject(4, ref _Boost, ItemEffect.CreateNew); } } private IPgItemEffect _Boost;
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get { return (float)GetDouble(8); } }
    }
}
