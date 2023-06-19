namespace Preprocessor;

internal class Skill
{
    public SkillAdvancementHintCollection? AdvancementHints { get; set; }
    public string? AdvancementTable { get; set; }
    public bool? AuxCombat { get; set; }
    public bool? Combat { get; set; }
    public string? Description { get; set; }
    public int? GuestLevelCap { get; set; }
    public bool? HideWhenZero { get; set; }
    public int? Id { get; set; }
    public SkillLevelCapCollection? InteractionFlagLevelCaps { get; set; }
    public bool? IsFakeCombatSkill { get; set; }
    public bool? IsUmbrellaSkill { get; set; }
    public int? MaxBonusLevels { get; set; }
    public string? Name { get; set; }
    public string[]? Parents { get; set; }
    public string[]? RecipeIngredientKeywords { get; set; }
    public string[]? _RecipeIngredientKeywords { get; set; }
    public SkillRewardCollection? Rewards { get; set; }
    public bool? SkillLevelDisparityApplies { get; set; }
    public bool? SkipBonusLevelsIfSkillUnlearned { get; set; }
    public string[]? TSysCompatibleCombatSkills { get; set; }
    public string? XpTable { get; set; }
}
