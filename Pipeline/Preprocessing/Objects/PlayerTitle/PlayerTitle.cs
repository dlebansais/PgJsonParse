namespace Preprocessor;

using System.Text.RegularExpressions;

public class PlayerTitle
{
    public PlayerTitle(RawPlayerTitle rawPlayerTitle)
    {
        Keywords = rawPlayerTitle.Keywords;
        (Title, Color, ColorFormat) = ParseTitle(rawPlayerTitle.Title);
        Tooltip = rawPlayerTitle.Tooltip;
    }

    private const string ColorTagStart = "<color=";
    private const string ColorTagEnd = "</color>";

    private static (string?, string?, ColorFormat?) ParseTitle(string? rawTitle)
    {
        if (rawTitle is null)
            return (null, null, null);

        // Search for the <color=...> pattern.
        string ColorTagPattern = @$"{ColorTagStart}([a-zA-Z0-9#]+)>";
        Match ColorTagMatch;

        ColorTagMatch = Regex.Match(rawTitle, ColorTagPattern);
        if (ColorTagMatch.Success && rawTitle.EndsWith(ColorTagEnd))
        {
            string MatchValue = ColorTagMatch.Value;
            int MatchLength = ColorTagMatch.Length;

            string Title = rawTitle.Substring(MatchLength, rawTitle.Length - MatchLength - ColorTagEnd.Length);
            string RawColor = MatchValue.Substring(ColorTagStart.Length, MatchValue.Length - ColorTagStart.Length - 1);

            string Color = RgbColor.Parse(RawColor, string.Empty, out ColorFormat ColorFormat);

            return (Title, Color, ColorFormat);
        }

        return (rawTitle, null, null);
    }

    public string? Color { get; set; }
    public string[]? Keywords { get; set; }
    public string? Title { get; set; }
    public string? Tooltip { get; set; }

    public RawPlayerTitle ToRawPlayerTitle()
    {
        RawPlayerTitle Result = new();

        Result.Keywords = Keywords;
        Result.Title = ToRawTitle(Title, ColorFormat);
        Result.Tooltip = Tooltip;

        return Result;
    }

    private static string? ToRawTitle(string? title, ColorFormat? colorFormat)
    {
        if (title is null)
            return null;

        if (colorFormat is null)
            return title;

        return $"{ColorTagStart}{colorFormat}>{title}{ColorTagEnd}";
    }

    private ColorFormat? ColorFormat;
}
