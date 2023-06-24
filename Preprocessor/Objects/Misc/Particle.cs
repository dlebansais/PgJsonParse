namespace Preprocessor;

using System;
using System.Text.RegularExpressions;

internal abstract class Particle
{
    protected static string ParseColor(string content, string header)
    {
        if (header == string.Empty || content.StartsWith(header))
        {
            // Search for the #RRGGBB pattern.
            string ColorPattern = @$"^#(?:[0-9a-fA-F]{{3}}){{1,2}}$";
            Match ColorMatch = Regex.Match(content.Substring(header.Length), ColorPattern, RegexOptions.IgnoreCase);
            if (ColorMatch.Success)
            {
                return ColorMatch.Value.Substring(1);
            }
        }

        throw new InvalidCastException();
    }
}
