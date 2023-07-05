namespace Preprocessor;

using System;
using System.Drawing;
using System.Text.RegularExpressions;

public static class RgbColor
{
    public static string Parse(string content, string header, out ColorFormat format)
    {
        if (header == string.Empty || content.StartsWith(header))
        {
            string ColorContent = content.Substring(header.Length);

            Color TryNamed = Color.FromName(ColorContent);
            if (TryNamed.ToArgb() != 0)
            {
                string ColorAsName = ColorContent;
                uint Value = (uint)TryNamed.ToArgb();
                Value &= 0x00FFFFFF;
                string HexValue = Value.ToString("X06");

                format = new ColorFormat(HexValue, ColorAsName, false, false, false);
                return format.NormalizedRGB;
            }
            else
            {
                bool HasSharp = false;
                bool HasAlpha = false;
                bool IsLowerCase = false;

                if (ColorContent.Length > 0 && ColorContent[0] == '#')
                {
                    ColorContent = ColorContent.Substring(1);
                    HasSharp = true;
                }

                if (ColorContent.Length == 8 && ColorContent.EndsWith("FF"))
                {
                    ColorContent = ColorContent.Substring(0, 6);
                    HasAlpha = true;
                }

                // Search for the RRGGBB pattern.
                string UpperCaseColorPattern = @$"(?:[0-9A-F]{{3}}){{1,2}}$";
                string LowerCaseColorPattern = @$"(?:[0-9a-f]{{3}}){{1,2}}$";
                Match ColorMatch;

                ColorMatch = Regex.Match(ColorContent, UpperCaseColorPattern);
                if (!ColorMatch.Success)
                {
                    IsLowerCase = true;
                    ColorMatch = Regex.Match(ColorContent, LowerCaseColorPattern);
                }

                if (ColorMatch.Success)
                {
                    format = new ColorFormat(ColorContent, null, HasSharp, HasAlpha, IsLowerCase);
                    return format.NormalizedRGB;
                }
            }
        }

        throw new InvalidCastException();
    }
}
