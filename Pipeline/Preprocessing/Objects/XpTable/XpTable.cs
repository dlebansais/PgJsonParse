﻿namespace Preprocessor;

public class XpTable
{
    public XpTable(RawXpTable rawXpTable)
    {
        InternalName = rawXpTable.InternalName;
        XpAmounts = rawXpTable.XpAmounts;
    }

    public string? InternalName { get; set; }
    public int[]? XpAmounts { get; set; }

    public RawXpTable ToRawXpTable()
    {
        RawXpTable Result = new();

        Result.InternalName = InternalName;
        Result.XpAmounts = XpAmounts;

        return Result;
    }
}
