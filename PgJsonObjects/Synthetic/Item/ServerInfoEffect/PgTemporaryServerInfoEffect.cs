namespace PgJsonObjects
{
    public class PgTemporaryServerInfoEffect : GenericPgObject<PgTemporaryServerInfoEffect>, IPgServerInfoEffect, IPgTemporaryServerInfoEffect
    {
        public PgTemporaryServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgTemporaryServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgTemporaryServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgTemporaryServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public IPgItemEffect Boost { get { return GetObject(8, ref _Boost, ItemEffect.CreateNew); } } private IPgItemEffect _Boost;
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get { return (float)GetDouble(12); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(16); } }
    }
}
