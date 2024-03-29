﻿namespace Preprocessor;

using System.Drawing;
using System.Text.RegularExpressions;

public static class RgbColor
{
    public static string Parse(string content, string header, out ColorFormat format)
    {
        if (header != string.Empty && !content.StartsWith(header))
            throw new PreprocessorException();

        return ParseWithValidHeader(content, header, out format);
    }

    private static string ParseWithValidHeader(string content, string header, out ColorFormat format)
    {
        string ColorContent = content.Substring(header.Length);

        Color TryNamed = Color.FromName(ColorContent);
        if (TryNamed.ToArgb() != 0)
            return ParseColorName(ColorContent, TryNamed, out format);
        else
            return ParseColorRGB(ColorContent, out format);
    }

    private static string ParseColorName(string ColorContent, Color TryNamed, out ColorFormat format)
    {
        string ColorAsName = ColorContent;
        uint Value = (uint)TryNamed.ToArgb();
        Value &= 0x00FFFFFF;
        string HexValue = Value.ToString("X06");

        format = new ColorFormat(HexValue, ColorAsName, false, false, false);
        return format.NormalizedRGB;
    }

    private static string ParseColorRGB(string ColorContent, out ColorFormat format)
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

        if (!ColorMatch.Success)
            throw new PreprocessorException();

        format = new ColorFormat(ColorContent, null, HasSharp, HasAlpha, IsLowerCase);
        return format.NormalizedRGB;
    }
}
