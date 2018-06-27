namespace PgJsonObjects
{
    public class PgXpTable : MainPgObject<PgXpTable>, IPgXpTable
    {
        public PgXpTable(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 10;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgXpTable CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgXpTable CreateNew(byte[] data, ref int offset)
        {
            return new PgXpTable(data, ref offset);
        }

        public string InternalName { get { return GetString(0); } }
        public XpTableLevelCollection XpAmountList { get { return GetObjectList(4, ref _XpAmountList, XpTableLevelCollection.CreateItem, () => new XpTableLevelCollection()); } } private XpTableLevelCollection _XpAmountList;
        public XpTableEnum EnumName { get { return GetEnum<XpTableEnum>(8); } }
    }
}
