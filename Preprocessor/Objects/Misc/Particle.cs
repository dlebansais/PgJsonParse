namespace Preprocessor;

using System;
using System.Drawing;
using System.Text.RegularExpressions;

internal abstract class Particle
{
    public static string ParseColor(string content, string header, out string? colorAsName, out bool hasSharp, out bool hasAlpha)
    {
        colorAsName = null;
        hasSharp = false;
        hasAlpha = false;

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

            if (ColorContent.Length > 0 && ColorContent[0] == '#')
            {
                ColorContent = ColorContent.Substring(1);
                hasSharp = true;
            }

            if (ColorContent.Length == 8 && ColorContent.EndsWith("FF"))
            {
                ColorContent = ColorContent.Substring(0, 6);
                hasAlpha = true;
            }

            // Search for the RRGGBB pattern.
            string ColorPattern = @$"(?:[0-9a-fA-F]{{3}}){{1,2}}$";
            Match ColorMatch = Regex.Match(ColorContent, ColorPattern, RegexOptions.IgnoreCase);
            if (ColorMatch.Success)
                return ColorMatch.Value;
        }

        throw new InvalidCastException();
    }
}
