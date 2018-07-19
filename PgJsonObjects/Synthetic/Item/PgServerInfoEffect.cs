using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class PgServerInfoEffect : GenericPgObject<PgServerInfoEffect>, IPgServerInfoEffect
    {
        public PgServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        /*
        protected override PgServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgServerInfoEffect(data, ref offset);
        }*/

        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(0); } }
        public ServerInfoEffectType Type { get { return GetEnum<ServerInfoEffectType>(4); } }

        public IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }
    }
}
