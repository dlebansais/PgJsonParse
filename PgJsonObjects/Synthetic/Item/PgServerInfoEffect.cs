namespace PgJsonObjects
{
    public class PgServerInfoEffect : GenericPgObject, IPgServerInfoEffect
    {
        public PgServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(0); } }
        public ServerInfoEffectType Type { get { return GetEnum<ServerInfoEffectType>(4); } }
    }
}
