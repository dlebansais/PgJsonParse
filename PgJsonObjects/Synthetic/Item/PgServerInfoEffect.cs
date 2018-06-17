namespace PgJsonObjects
{
    public class PgServerInfoEffect : GenericPgObject<PgServerInfoEffect>, IPgServerInfoEffect
    {
        public PgServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgServerInfoEffect(data, offset);
        }

        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(0); } }
        public ServerInfoEffectType Type { get { return GetEnum<ServerInfoEffectType>(4); } }
    }
}
