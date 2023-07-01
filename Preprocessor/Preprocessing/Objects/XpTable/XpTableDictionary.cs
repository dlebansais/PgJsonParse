namespace Preprocessor;

using System.Collections.Generic;

internal class XpTableDictionary : Dictionary<int, XpTable>, IDictionaryValueBuilder<XpTable, XpTable>
{
    public XpTable FromRaw(XpTable xpTable) => xpTable;
    public XpTable ToRaw(XpTable xpTable) => xpTable;
}
