using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgRecipe : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        string Description { get; }
        int IconId { get; }
        int? RawIconId { get; }
        IPgRecipeItemCollection IngredientList { get; }
        string InternalName { get; }
        string Name { get; }
        IPgRecipeItemCollection ResultItemList { get; }
        IPgSkill Skill { get; }
        int SkillLevelReq { get; }
        int? RawSkillLevelReq { get; }
        IPgRecipeResultEffectCollection ResultEffectList { get; }
        IPgSkill SortSkill { get; }
        List<RecipeKeyword> KeywordList { get; }
        double UsageDelay { get; }
        double? RawUsageDelay { get; }
        string UsageDelayMessage { get; }
        RecipeAction ActionLabel { get; }
        RecipeUsageAnimation UsageAnimation { get; }
        IPgAbilityRequirementCollection OtherRequirementList { get; }
        IPgRecipeCostCollection CostList { get; }
        int NumResultItems { get; }
        int? RawNumResultItems { get; }
        string UsageAnimationEnd { get; }
        int ResetTimeInSeconds { get; }
        int? RawResetTimeInSeconds { get; }
        uint? DyeColor { get; }
        IPgSkill RewardSkill { get; }
        int RewardSkillXp { get; }
        int? RawRewardSkillXp { get; }
        int RewardSkillXpDropOffLevel { get; }
        int? RawRewardSkillXpDropOffLevel { get; }
        double RewardSkillXpDropOffPct { get; }
        double? RawRewardSkillXpDropOffPct { get; }
        int RewardSkillXpDropOffRate { get; }
        int? RawRewardSkillXpDropOffRate { get; }
        int RewardSkillXpFirstTime { get; }
        int? RawRewardSkillXpFirstTime { get; }
        IPgRecipe SharesResetTimerWith { get; }
        string ItemMenuLabel { get; }
        string RawItemMenuCategory { get; }
        int ItemMenuCategoryLevel { get; }
        int? RawItemMenuCategoryLevel { get; }
        IPgRecipe PrereqRecipe { get; }
        bool IsItemMenuKeywordReqSufficient { get; }
        bool? RawIsItemMenuKeywordReqSufficient { get; }
        bool IngredientListIsEmpty { get; }
        bool? RawIngredientListIsEmpty { get; }
        bool ResultItemListIsEmpty { get; }
        bool? RawResultItemListIsEmpty { get; }
        bool RewardAllowBonusXp { get; }
        bool? RawRewardAllowBonusXp { get; }
        ItemKeyword RecipeItemKeyword { get; }
        List<ItemKeyword> ValidationIngredientKeywordList { get; }
        IPgRecipeItemCollection ProtoResultItemList { get; }
        IPgGenericSourceCollection SourceList { get; }
        double? PerfectCottonRatio { get; }
    }
}
