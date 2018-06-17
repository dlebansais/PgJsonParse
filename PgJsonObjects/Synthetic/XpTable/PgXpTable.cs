namespace PgJsonObjects
{
    public class PgXpTable : GenericPgObject, IPgXpTable
    {
        public PgXpTable(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string InternalName { get { return GetString(0); } }
        public XpTableLevelCollection XpAmountList { get { return GetObjectList(4, ref _XpAmountList, (byte[] data, int offset) => new PgXpTableLevel(data, offset), () => new XpTableLevelCollection()); } } private XpTableLevelCollection _XpAmountList;
        public XpTableEnum EnumName { get { return GetEnum<XpTableEnum>(8); } }
    }
}
