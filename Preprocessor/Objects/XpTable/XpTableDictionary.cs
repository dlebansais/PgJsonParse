namespace Preprocessor;

using System.Collections.Generic;

internal class XpTableDictionary : Dictionary<int, XpTable>, IDictionaryValueBuilder<XpTable, XpTable>
{
    public XpTable FromRaw(XpTable item) => item;
    public XpTable ToRaw(XpTable item) => item;
}
