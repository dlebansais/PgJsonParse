using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgGameArea : MainPgObject<PgGameArea>, IPgGameArea
    {
        public PgGameArea(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 18;
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
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public MapAreaName KeyArea { get { return GetEnum<MapAreaName>(16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
