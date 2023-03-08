namespace PgObjects
{
    using System;
    using System.Collections.Generic;

    public class PgRecipe : PgObject
    {
        public string Description { get; set; } = string.Empty;
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public int BoolValues { get; set; }
        public PgRecipeItemCollection IngredientList { get; set; } = new PgRecipeItemCollection();
        public string InternalName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public PgRecipeItemCollection ResultItemList { get; set; } = new PgRecipeItemCollection();
        public string? Skill_Key { get; set; }
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get; set; }
        public PgRecipeResultEffectCollection ResultEffectList { get; set; } = new PgRecipeResultEffectCollection();
        public string? SortSkill_Key { get; set; }
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
        public string? RewardSkill_Key { get; set; }
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
        public string? SharesResetTimerWith_Key { get; set; }
        public string ItemMenuLabel { get; set; } = string.Empty;
        public ItemKeyword RecipeItemKeyword { get; set; }
        public const int IsItemMenuKeywordReqSufficientNotNull = 1 << 0;
        public const int IsItemMenuKeywordReqSufficientIsTrue = 1 << 1;
        public bool IsItemMenuKeywordReqSufficient { get { return (BoolValues & (IsItemMenuKeywordReqSufficientNotNull + IsItemMenuKeywordReqSufficientIsTrue)) == (IsItemMenuKeywordReqSufficientNotNull + IsItemMenuKeywordReqSufficientIsTrue); } }
        public bool? RawIsItemMenuKeywordReqSufficient { get { return ((BoolValues & IsItemMenuKeywordReqSufficientNotNull) != 0) ? (BoolValues & IsItemMenuKeywordReqSufficientIsTrue) != 0 : null; } }
        public void SetIsItemMenuKeywordReqSufficient(bool value) { BoolValues |= (BoolValues & ~(IsItemMenuKeywordReqSufficientNotNull + IsItemMenuKeywordReqSufficientIsTrue)) | ((value ? IsItemMenuKeywordReqSufficientIsTrue : 0) + IsItemMenuKeywordReqSufficientNotNull); }
        public string ItemMenuCategory { get; set; } = string.Empty;
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get; set; }
        public string? PrereqRecipe_Key { get; set; }
        public List<ItemKeyword> ValidationIngredientKeywordList { get; set; } = new List<ItemKeyword>();
        public PgRecipeItemCollection ProtoResultItemList { get; set; } = new PgRecipeItemCollection();
        public const int RewardAllowBonusXpNotNull = 1 << 2;
        public const int RewardAllowBonusXpIsTrue = 1 << 3;
        public bool RewardAllowBonusXp { get { return (BoolValues & (RewardAllowBonusXpNotNull + RewardAllowBonusXpIsTrue)) == (RewardAllowBonusXpNotNull + RewardAllowBonusXpIsTrue); } }
        public bool? RawRewardAllowBonusXp { get { return ((BoolValues & RewardAllowBonusXpNotNull) != 0) ? (BoolValues & RewardAllowBonusXpIsTrue) != 0 : null; } }
        public void SetRewardAllowBonusXp(bool value) { BoolValues |= (BoolValues & ~(RewardAllowBonusXpNotNull + RewardAllowBonusXpIsTrue)) | ((value ? RewardAllowBonusXpIsTrue : 0) + RewardAllowBonusXpNotNull); }
        public PgSourceCollection SourceList { get; set; } = new PgSourceCollection();
        public string? RequiredAttributeNonZero_Key { get; set; }
        public PgRecipeParticle? LoopParticle { get; set; }
        public PgRecipeParticle? Particle { get; set; }
        public int MaxUses { get { return RawMaxUses.HasValue ? RawMaxUses.Value : 0; } }
        public int? RawMaxUses { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name; } }
        public override string ToString() { return Name; }
    }
}
