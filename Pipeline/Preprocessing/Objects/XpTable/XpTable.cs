namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class XpTable : IHasKey<int>
{
    public XpTable(int key)
    {
        Key = key;
    }

    public XpTable(int key, RawXpTable rawXpTable)
        : this(key)
    {
        InternalName = rawXpTable.InternalName;
        XpAmounts = rawXpTable.XpAmounts;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    public string? InternalName { get; set; }

    [Column(MapType = typeof(string))]
    public int[]? XpAmounts { get; set; }

    public RawXpTable ToRawXpTable()
    {
        RawXpTable Result = new();

        Result.InternalName = InternalName;
        Result.XpAmounts = XpAmounts;

        return Result;
    }
}
