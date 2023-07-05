namespace Preprocessor;

internal class PowerEffect
{
    public decimal? AttributeEffect { get; set; }
    public string? AttributeName { get; set; }
    public string? AttributeSkill { get; set; }
    public string? Description { get; set; }
    public int[]? IconIds { get; set; }

    public void SetIsSicEmFixed(bool isSicEmFixed)
    {
        IsSicEmFixed = isSicEmFixed;
    }

    public bool GetIsSicEmFixed()
    {
        return IsSicEmFixed;
    }

    private bool IsSicEmFixed;
}
