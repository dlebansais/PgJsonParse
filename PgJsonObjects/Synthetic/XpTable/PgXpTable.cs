using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgXpTable : MainPgObject<PgXpTable>, IPgXpTable
    {
        public PgXpTable(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 14;
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

        public override string Key { get { return GetString(0); } }
        public string InternalName { get { return GetString(4); } }
        public XpTableLevelCollection XpAmountList { get { return GetObjectList(8, ref _XpAmountList, XpTableLevelCollection.CreateItem, () => new XpTableLevelCollection()); } } private XpTableLevelCollection _XpAmountList;
        public XpTableEnum EnumName { get { return GetEnum<XpTableEnum>(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
