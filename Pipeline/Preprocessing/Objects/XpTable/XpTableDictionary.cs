﻿namespace Preprocessor;

using System.Collections.Generic;

public class XpTableDictionary : Dictionary<int, XpTable>, IDictionaryValueBuilder<XpTable, RawXpTable>
{
    public XpTable FromRaw(RawXpTable rawXpTable) => new XpTable(rawXpTable);
    public RawXpTable ToRaw(XpTable xpTable) => xpTable.ToRawXpTable();
}
