namespace Preprocessor;

public class RawRecipe
{
    public string? ActionLabel { get; set; }
    public Cost[]? Costs { get; set; }
    public string? Description { get; set; }
    public string? DyeColor { get; set; }
    public int IconId { get; set; }
    public object? Ingredients { get; set; }
    public string? InternalName { get; set; }
    public bool? IsItemMenuKeywordReqSufficient { get; set; }
    public string? ItemMenuCategory { get; set; }
    public int? ItemMenuCategoryLevel { get; set; }
    public string? ItemMenuKeywordReq { get; set; }
    public string? ItemMenuLabel { get; set; }
    public string[]? Keywords { get; set; }
    public string? LoopParticle { get; set; }
    public int? MaxUses { get; set; }
    public string? Name { get; set; }
    public int? NumResultItems { get; set; }
    public object? OtherRequirements { get; set; }
    public string? Particle { get; set; }
    public string? PrereqRecipe { get; set; }
    public object? ProtoResultItems { get; set; }
    public int? ResetTimeInSeconds { get; set; }
    public string? RequiredAttributeNonZero { get; set; }
    public string[]? ResultEffects { get; set; }
    public string[]? ResultEffectsThatCanFail { get; set; }
    public object? ResultItems { get; set; }
    public bool? RewardAllowBonusXp { get; set; }
    public string? RewardSkill { get; set; }
    public int? RewardSkillXp { get; set; }
    public int? RewardSkillXpDropOffLevel { get; set; }
    public decimal? RewardSkillXpDropOffPct { get; set; }
    public int? RewardSkillXpDropOffRate { get; set; }
    public int? RewardSkillXpFirstTime { get; set; }
    public string? SharesResetTimerWith { get; set; }
    public string? Skill { get; set; }
    public int? SkillLevelReq { get; set; }
    public string? SortSkill { get; set; }
    public string? UsageAnimation { get; set; }
    public string? UsageAnimationEnd { get; set; }
    public decimal? UsageDelay { get; set; }
    public string? UsageDelayMessage { get; set; }
    public string[]? ValidationIngredientKeywords { get; set; }
}
