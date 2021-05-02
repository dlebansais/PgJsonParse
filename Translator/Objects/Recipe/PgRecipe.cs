namespace PgObjects
{
    using System;
    using System.Collections.Generic;

    public class PgRecipe : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public PgRecipeItemCollection IngredientList { get; set; } = new PgRecipeItemCollection();
        public string InternalName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public PgRecipeItemCollection ResultItemList { get; set; } = new PgRecipeItemCollection();
        public PgSkill Skill { get; set; } = null!;
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get; set; }
        public PgRecipeResultEffectCollection ResultEffectList { get; set; } = new PgRecipeResultEffectCollection();
        public PgSkill SortSkill { get; set; } = null!;
        public List<RecipeKeyword> KeywordList { get; set; } = new List<RecipeKeyword>();
        public RecipeAction ActionLabel { get; set; }
        public float UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public float? RawUsageDelay { get; set; }
        public string UsageDelayMessage { get; set; } = string.Empty;
        public RecipeUsageAnimation UsageAnimation { get; set; }
        public PgAbilityRequirementCollection OtherRequirementList { get; set; } = new PgAbilityRequirementCollection();
        public PgRecipeCostCollection CostList { get; set; } = new PgRecipeCostCollection();
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get; set; }
        public string UsageAnimationEnd { get; set; } = string.Empty;
        public TimeSpan ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : TimeSpan.Zero; } }
        public TimeSpan? RawResetTime { get; set; }
        public uint? DyeColor { get; set; }
        public PgSkill RewardSkill { get; set; } = null!;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get; set; }
        public int RewardSkillXpDropOffLevel { get { return RawRewardSkillXpDropOffLevel.HasValue ? RawRewardSkillXpDropOffLevel.Value : 0; } }
        public int? RawRewardSkillXpDropOffLevel { get; set; }
        public float RewardSkillXpDropOffPct { get { return RawRewardSkillXpDropOffPct.HasValue ? (float)Math.Round(RawRewardSkillXpDropOffPct.Value, 2) : 0; } }
        public float? RawRewardSkillXpDropOffPct { get; set; }
        public int RewardSkillXpDropOffRate { get { return RawRewardSkillXpDropOffRate.HasValue ? RawRewardSkillXpDropOffRate.Value : 0; } }
        public int? RawRewardSkillXpDropOffRate { get; set; }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get; set; }
        public PgRecipe SharesResetTimerWith { get; set; } = null!;
        public string ItemMenuLabel { get; set; } = string.Empty;
        public ItemKeyword RecipeItemKeyword { get; set; }
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get; set; }
        public string ItemMenuCategory { get; set; } = string.Empty;
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get; set; }
        public PgRecipe PrereqRecipe { get; set; } = null!;
        public List<ItemKeyword> ValidationIngredientKeywordList { get; set; } = new List<ItemKeyword>();
        public PgRecipeItemCollection ProtoResultItemList { get; set; } = new PgRecipeItemCollection();
        public bool RewardAllowBonusXp { get { return RawRewardAllowBonusXp.HasValue && RawRewardAllowBonusXp.Value; } }
        public bool? RawRewardAllowBonusXp { get; set; }
        public PgSourceCollection SourceList { get; set; } = new PgSourceCollection();

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name; } }
        public override string ToString() { return Name; }
    }
}
