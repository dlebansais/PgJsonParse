namespace Preprocessor;

using System;
using System.Diagnostics;

public class KeywordValues : IEquatable<KeywordValues>
{
    public string? Keyword { get; set; }
    public decimal[]? Values { get; set; }

    public bool Equals(KeywordValues other)
    {
        Debug.Assert(Keyword == other.Keyword);

        if (Values is decimal[] ThisValues && other.Values is decimal[] OtherValues)
        {
            Debug.Assert(ThisValues.Length == OtherValues.Length);

            for (int j = 0; j < ThisValues.Length; j++)
                Debug.Assert(ThisValues[j] == OtherValues[j]);
        }

        return true;
    }
}
