namespace Preprocessor;

using System;

public class KeywordValues : IEquatable<KeywordValues>
{
    public string? Keyword { get; set; }
    public decimal[]? Values { get; set; }

    public bool Equals(KeywordValues other)
    {
        if (Keyword != other.Keyword)
            return false;

        if (Values is decimal[] ThisValues && other.Values is decimal[] OtherValues)
        {
            if (ThisValues.Length != OtherValues.Length)
                return false;

            for (int j = 0; j < ThisValues.Length; j++)
                if (ThisValues[j] != OtherValues[j])
                    return false;
        }
        else if (Values is not null || other.Values is not null)
            return false;

        return true;
    }
}
