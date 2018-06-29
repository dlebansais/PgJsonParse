namespace PgJsonObjects
{
    public class PgGameArea : MainPgObject<PgGameArea>, IPgGameArea
    {
        public PgGameArea(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 10;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgGameArea CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgGameArea CreateNew(byte[] data, ref int offset)
        {
            PgGameArea Item = new PgGameArea(data, ref offset);
            string FriendlyName = Item.FriendlyName;
            string ShortFriendlyName = Item.ShortFriendlyName;
            return Item;
        }

        public override string Key { get { return GetString(0); } }
        public string FriendlyName { get { return GetString(4); } }
        public string ShortFriendlyName { get { return GetString(8); } }
        public MapAreaName KeyArea { get { return GetEnum<MapAreaName>(12); } }
    }
}
