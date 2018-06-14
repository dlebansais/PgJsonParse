using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgXpTable : GenericPgObject, IPgXpTable
    {
        public PgXpTable(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string InternalName { get { return GetString(0); } }
        public List<XpTableLevel> XpAmountList { get { return GetObjectList(4, ref _XpAmountList); } } private List<XpTableLevel> _XpAmountList;
        public XpTableEnum EnumName { get { return GetEnum<XpTableEnum>(8); } }
    }
}
