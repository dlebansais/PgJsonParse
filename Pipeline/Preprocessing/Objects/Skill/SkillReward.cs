namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SkillReward
{
    public SkillReward(RawSkillReward rawSkillReward)
    {
        Abilities = Preprocessor.ToSingleOrMultiple(rawSkillReward.Ability, (string s) => s, out AbilityFormat);
        BonusToSkill = rawSkillReward.BonusToSkill;
        Level = rawSkillReward.Level;
        Notes = rawSkillReward.Notes;
        Races = rawSkillReward.Races;
        Recipe = rawSkillReward.Recipe;
    }

    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? Abilities { get; set; }

    public string? BonusToSkill { get; set; }

    public int? Level { get; set; }

    public string? Notes { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? Races { get; set; }

    public string? Recipe { get; set; }

    public RawSkillReward ToRawSkillReward()
    {
        RawSkillReward Result = new();

        Result.Ability = Preprocessor.FromSingleOrMultiple(Abilities, (string s) => s, AbilityFormat);
        Result.BonusToSkill = BonusToSkill;
        Result.Level = Level;
        Result.Notes = Notes;
        Result.Races = Races;
        Result.Recipe = Recipe;

        return Result;
    }

    private JsonArrayFormat AbilityFormat;
}
