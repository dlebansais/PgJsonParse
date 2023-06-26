namespace Preprocessor;

internal class StockDye
{
    public string? Color1 { get; set; }
    public string? Color2 { get; set; }
    public string? Color3 { get; set; }
    public bool IsGlowEnabled { get; set; }

    public void SetColorAsName(int index, string? colorAsName)
    {
        ColorAsName[index] = colorAsName;
    }

    public string? GetColorAsName(int index)
    {
        return ColorAsName[index];
    }

    public void SetColorHasAlpha(int index, bool colorHasAlpha)
    {
        ColorHasAlpha[index] = colorHasAlpha;
    }

    public bool GetColorHasAlpha(int index)
    {
        return ColorHasAlpha[index];
    }

    private string?[] ColorAsName = new string?[3];
    private bool[] ColorHasAlpha = new bool[3];
}
