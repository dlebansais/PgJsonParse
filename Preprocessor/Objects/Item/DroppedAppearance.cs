namespace Preprocessor;

internal class DroppedAppearance
{
    public string? Appearance { get; set; }
    public string? Color { get; set; }
    public string? Cork { get; set; }
    public string? Food { get; set; }
    public string? Plate { get; set; }
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

    public void SetColorAsName(string? colorAsName)
    {
        ColorAsName = colorAsName;
    }

    public string? GetColorAsName()
    {
        return ColorAsName;
    }

    public void SetSkinColorAsName(string? colorAsName)
    {
        SkinColorAsName = colorAsName;
    }

    public string? GetSkinColorAsName()
    {
        return SkinColorAsName;
    }

    private bool IsSkinInverted;
    private string? ColorAsName;
    private string? SkinColorAsName;
}
