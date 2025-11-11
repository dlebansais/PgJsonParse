namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class DroppedAppearance
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public string? Appearance { get; set; }

    public string? Color { get; set; }

    public string? Cork { get; set; }

    public string? Food { get; set; }

    public string? Plate { get; set; }

    public string? Scale { get; set; }

    public string? Skin { get; set; }

    public string? SkinColor { get; set; }

    public void SetIsSkinInverted(bool isSkinInverted)
    {
        IsSkinInverted = isSkinInverted;
    }

    public bool GetIsSkinInverted()
    {
        return IsSkinInverted;
    }

    public void SetColorFormat(ColorFormat colorFormat)
    {
        ColorFormat = colorFormat;
    }

    public ColorFormat GetColorFormat()
    {
        return ColorFormat;
    }

    public void SetSkinColorFormat(ColorFormat skinColorFormat)
    {
        SkinColorFormat = skinColorFormat;
    }

    public ColorFormat GetSkinColorFormat()
    {
        return SkinColorFormat;
    }

    private bool IsSkinInverted;
    private ColorFormat ColorFormat = new ColorFormat(string.Empty, default, default, default, default);
    private ColorFormat SkinColorFormat = new ColorFormat(string.Empty, default, default, default, default);
}
