using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgXpTable : MainPgObject<PgXpTable>, IPgXpTable
    {
        public PgXpTable(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 18;
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
        public IPgXpTableLevelCollection XpAmountList { get { return GetObjectList(8, ref _XpAmountList, PgXpTableLevelCollection.CreateItem, () => new PgXpTableLevelCollection()); } } private IPgXpTableLevelCollection _XpAmountList;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public XpTableEnum EnumName { get { return GetEnum<XpTableEnum>(16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<XpTableEnum>.ToString(EnumName, null, XpTableEnum.Internal_None) } },
            { "XpAmounts", new FieldParser() {
                Type = FieldType.SimpleIntegerArray,
                GetIntegerArray = GetXpAmounts } },
        }; } }

        private List<int> GetXpAmounts()
        {
            List<int> Result = new List<int>();

            foreach (IPgXpTableLevel Item in XpAmountList)
                Result.Add(Item.Xp);

            return Result;
        }
    }
}
