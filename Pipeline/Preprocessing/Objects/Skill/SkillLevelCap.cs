namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SkillLevelCap
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public bool IsPerformanceSkill { get; set; }

    public int Level { get; set; }

    public string? Skill { get; set; }

    public int? SkillCap { get; set; }
}
