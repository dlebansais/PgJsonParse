namespace PgJsonObjects
{
    public class PgGameArea : GenericPgObject, IPgGameArea
    {
        public PgGameArea(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string FriendlyName { get { return GetString(0); } }
        public string ShortFriendlyName { get { return GetString(4); } }
        public MapAreaName KeyArea { get { return GetEnum<MapAreaName>(8); } }
    }
}
