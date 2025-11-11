namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class StockDye
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public string? Color1 { get; set; }

    public string? Color2 { get; set; }

    public string? Color3 { get; set; }

    public bool IsGlowEnabled { get; set; }

    public void SetColorFormat(int index, ColorFormat colorFormat)
    {
        ColorFormats[index] = colorFormat;
    }

    public ColorFormat? GetColorFormat(int index)
    {
        return ColorFormats[index];
    }

    private ColorFormat?[] ColorFormats = new ColorFormat?[3];
}
