using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgRecipe
    {
        string Description { get; }
        int IconId { get; }
        int? RawIconId { get; }
        List<RecipeItem> IngredientList { get; }
        string InternalName { get; }
        string Name { get; }
        List<RecipeItem> ResultItemList { get; }
        Skill Skill { get; }
        int SkillLevelReq { get; }
        int? RawSkillLevelReq { get; }
        List<RecipeResultEffect> ResultEffectList { get; }
        Skill SortSkill { get; }
        List<RecipeKeyword> KeywordList { get; }
        int UsageDelay { get; }
        int? RawUsageDelay { get; }
        string UsageDelayMessage { get; }
        RecipeAction ActionLabel { get; }
        RecipeUsageAnimation UsageAnimation { get; }
        List<AbilityRequirement> OtherRequirementList { get; }
        List<RecipeCost> CostList { get; }
        int NumResultItems { get; }
        int? RawNumResultItems { get; }
        string UsageAnimationEnd { get; }
        int ResetTimeInSeconds { get; }
        int? RawResetTimeInSeconds { get; }
        uint? DyeColor { get; }
        Skill RewardSkill { get; }
        int RewardSkillXp { get; }
        int? RawRewardSkillXp { get; }
        int RewardSkillXpFirstTime { get; }
        int? RawRewardSkillXpFirstTime { get; }
        Recipe SharesResetTimerWith { get; }
        string ItemMenuLabel { get; }
        string RawItemMenuCategory { get; }
        int ItemMenuCategoryLevel { get; }
        int? RawItemMenuCategoryLevel { get; }
        Recipe PrereqRecipe { get; }
        bool IsItemMenuKeywordReqSufficient { get; }
        bool? RawIsItemMenuKeywordReqSufficient { get; }
        ItemKeyword RecipeItemKeyword { get; }
    }
}
