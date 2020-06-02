namespace PgJsonObjects
{
    using System;
    using System.Collections.Generic;

    public class PgRecipe
    {
        public string Description { get; set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public PgRecipeItemCollection IngredientList { get; } = new PgRecipeItemCollection();
        public string InternalName { get; set; }
        public string Name { get; set; }
        public PgRecipeItemCollection ResultItemList { get; } = new PgRecipeItemCollection();
        public PgSkill Skill { get; set; }
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get; set; }
        public PgRecipeResultEffectCollection ResultEffectList { get; } = new PgRecipeResultEffectCollection();
        public PgSkill SortSkill { get; set; }
        public List<RecipeKeyword> KeywordList { get; } = new List<RecipeKeyword>();
        public double UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public double? RawUsageDelay { get; set; }
        public string UsageDelayMessage { get; set; }
        public RecipeAction ActionLabel { get; set; }
        public RecipeUsageAnimation UsageAnimation { get; set; }
        public PgAbilityRequirementCollection OtherRequirementList { get; } = new PgAbilityRequirementCollection();
        public PgRecipeCostCollection CostList { get; } = new PgRecipeCostCollection();
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get; set; }
        public string UsageAnimationEnd { get; set; }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        public int? RawResetTimeInSeconds { get; set; }
        public uint? DyeColor { get; set; }
        public PgSkill RewardSkill { get; set; }
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get; set; }
        public int RewardSkillXpDropOffLevel { get { return RawRewardSkillXpDropOffLevel.HasValue ? RawRewardSkillXpDropOffLevel.Value : 0; } }
        public int? RawRewardSkillXpDropOffLevel { get; set; }
        public double RewardSkillXpDropOffPct { get { return RawRewardSkillXpDropOffPct.HasValue ? Math.Round(RawRewardSkillXpDropOffPct.Value, 2) : 0; } }
        public double? RawRewardSkillXpDropOffPct { get; set; }
        public int RewardSkillXpDropOffRate { get { return RawRewardSkillXpDropOffRate.HasValue ? RawRewardSkillXpDropOffRate.Value : 0; } }
        public int? RawRewardSkillXpDropOffRate { get; set; }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get; set; }
        public PgRecipe SharesResetTimerWith { get; set; }
        public string ItemMenuLabel { get; set; }
        public string RawItemMenuCategory { get; set; }
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get; set; }
        public PgRecipe PrereqRecipe { get; set; }
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get; set; }
        public bool IngredientListIsEmpty { get { return RawIngredientListIsEmpty.HasValue && RawIngredientListIsEmpty.Value; } }
        public bool? RawIngredientListIsEmpty { get; set; }
        public bool ResultItemListIsEmpty { get { return RawResultItemListIsEmpty.HasValue && RawResultItemListIsEmpty.Value; } }
        public bool? RawResultItemListIsEmpty { get; set; }
        public bool ProtoResultItemListIsEmpty { get { return RawProtoResultItemListIsEmpty.HasValue && RawProtoResultItemListIsEmpty.Value; } }
        public bool? RawProtoResultItemListIsEmpty { get; set; }
        public bool RewardAllowBonusXp { get { return RawRewardAllowBonusXp.HasValue && RawRewardAllowBonusXp.Value; } }
        public bool? RawRewardAllowBonusXp { get; set; }
        public ItemKeyword RecipeItemKeyword { get; set; }
        public List<ItemKeyword> ValidationIngredientKeywordList { get; } = new List<ItemKeyword>();
        public PgRecipeItemCollection ProtoResultItemList { get; } = new PgRecipeItemCollection();
    }
}
