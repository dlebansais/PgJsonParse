using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgXpTable
    {
        string InternalName { get; }
        List<XpTableLevel> XpAmountList { get; }
        XpTableEnum EnumName { get; }
    }
}
