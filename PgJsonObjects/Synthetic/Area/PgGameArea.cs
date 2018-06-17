namespace PgJsonObjects
{
    public class PgGameArea : MainPgObject<PgGameArea>, IPgGameArea
    {
        public PgGameArea(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgGameArea CreateItem(byte[] data, int offset)
        {
            return new PgGameArea(data, offset);
        }

        public string FriendlyName { get { return GetString(0); } }
        public string ShortFriendlyName { get { return GetString(4); } }
        public MapAreaName KeyArea { get { return GetEnum<MapAreaName>(8); } }
    }
}
