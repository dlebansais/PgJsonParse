namespace PgJsonObjects
{
    public class PgTemporaryServerInfoEffect : GenericPgObject, IPgTemporaryServerInfoEffect
    {
        public PgTemporaryServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgTemporaryServerInfoEffect(data, offset);
        }

        public ItemEffect Boost { get { return GetObject(4, ref _Boost); } } private ItemEffect _Boost;
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get { return (float)GetDouble(8); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(12); } }
    }
}
