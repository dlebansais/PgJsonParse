namespace Preprocessor;

public class XpTable
{
    public XpTable(RawXpTable rawXpTable)
    {
        if (rawXpTable.InternalName == "Cooking-unused")
            InternalName = "CookingUnused";
        else
            InternalName = rawXpTable.InternalName;

        XpAmounts = rawXpTable.XpAmounts;
    }

    public string? InternalName { get; set; }
    public int[]? XpAmounts { get; set; }

    public RawXpTable ToRawXpTable()
    {
        RawXpTable Result = new();

        if (InternalName == "CookingUnused")
            Result.InternalName = "Cooking-unused";
        else
            Result.InternalName = InternalName;

        Result.XpAmounts = XpAmounts;

        return Result;
    }
}
