namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class PowerEffect
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public decimal? AttributeEffect { get; set; }

    public string? AttributeName { get; set; }

    public string? AttributeSkill { get; set; }

    public string? Description { get; set; }

    [Column(MapType = typeof(string))]
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
