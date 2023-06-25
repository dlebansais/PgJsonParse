namespace Preprocessor;

using System;
using System.Drawing;
using System.Text.RegularExpressions;

internal abstract class Particle
{
    public static string ParseColor(string content, string header, out string? colorAsName)
    {
        colorAsName = null;

        if (header == string.Empty || content.StartsWith(header))
        {
            string ColorContent = content.Substring(header.Length);

            Color TryNamed = Color.FromName(ColorContent);
            if (TryNamed.ToArgb() != 0)
            {
                colorAsName = ColorContent;
                uint Value = (uint)TryNamed.ToArgb();
                Value &= 0x00FFFFFF;
                string HexValue = Value.ToString("X06");

                return HexValue;
            }

            // Search for the #RRGGBB pattern.
            string ColorPattern = @$"^#(?:[0-9a-fA-F]{{3}}){{1,2}}$";
            Match ColorMatch = Regex.Match(ColorContent, ColorPattern, RegexOptions.IgnoreCase);
            if (ColorMatch.Success)
            {
                return ColorMatch.Value.Substring(1);
            }
        }

        throw new InvalidCastException();
    }
}
