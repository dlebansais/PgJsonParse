namespace Preprocessor;

using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class KeywordValues : IEquatable<KeywordValues>
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public string? Keyword { get; set; }

    [Column(MapType = typeof(string))]
    public decimal[]? Values { get; set; }

    public bool Equals(KeywordValues? other)
    {
        Debug.Assert(other is null || Keyword == other.Keyword);

        if (Values is decimal[] ThisValues && other?.Values is decimal[] OtherValues)
        {
            Debug.Assert(ThisValues.Length == OtherValues.Length);

            for (int j = 0; j < ThisValues.Length; j++)
                Debug.Assert(ThisValues[j] == OtherValues[j]);
        }

        return true;
    }
}
