namespace Preprocessor;

using System.Collections.Generic;

public class XpTableDictionary : Dictionary<int, XpTable>, IDictionaryValueBuilderInt<XpTable, RawXpTable>
{
    public XpTable FromRaw(int key, RawXpTable rawXpTable) => new(key, rawXpTable);
    public RawXpTable ToRaw(XpTable xpTable) => xpTable.ToRawXpTable();
}
