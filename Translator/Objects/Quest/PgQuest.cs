namespace PgJsonObjects
{
    using System;
    using System.Collections.Generic;

    public class PgQuest
    {
        public string InternalName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get; set; }
        public PgQuestObjectiveCollection QuestObjectiveList { get; } = new PgQuestObjectiveCollection();
        public PgQuestRewardXpCollection RewardsXPList { get; } = new PgQuestRewardXpCollection();
        public PgQuestRewardItemCollection QuestRewardsItemList { get; } = new PgQuestRewardItemCollection();
        public TimeSpan? RawReuseTime { get; set; }
        public int RewardCombatXP { get { return RawRewardCombatXP.HasValue ? RawRewardCombatXP.Value : 0; } }
        public int? RawRewardCombatXP { get; set; }
        public PgNpc FavorNpc { get; set; }
        public string PrefaceText { get; set; } = string.Empty;
        public string SuccessText { get; set; } = string.Empty;
        public string MidwayText { get; set; } = string.Empty;
        public PgAbility RewardAbility { get; set; }
        public int RewardFavor { get { return RawRewardFavor.HasValue ? RawRewardFavor.Value : 0; } }
        public int? RawRewardFavor { get; set; }
        public PgSkill RewardSkill { get; set; }
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get; set; }
        public PgRecipe RewardRecipe { get; set; }
        public int RewardGuildXp { get { return RawRewardGuildXp.HasValue ? RawRewardGuildXp.Value : 0; } }
        public int? RawRewardGuildXp { get; set; }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get; set; }
        public PgQuestRewardItemCollection PreGiveItemList { get; } = new PgQuestRewardItemCollection();
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        public int? RawTSysLevel { get; set; }
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        public int? RawRewardGold { get; set; }
        public string RewardsNamedLootProfile { get; set; } = string.Empty;
        public PgRecipeCollection PreGiveRecipeList { get; set; } = new PgRecipeCollection();
        public List<QuestKeyword> KeywordList { get; } = new List<QuestKeyword>();
        public PgEffectCollection RewardEffectList { get; set; } = new PgEffectCollection();
        public PgLoreBook RewardLoreBook { get; set; }
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get; set; }
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        public bool? RawIsAutoPreface { get; set; }
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        public bool? RawIsAutoWrapUp { get; set; }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        public bool? RawIsGuildQuest { get; set; }
        public bool IsUnknownWorkOrderSkill { get { return RawIsUnknownWorkOrderSkill.HasValue && RawIsUnknownWorkOrderSkill.Value; } }
        public bool? RawIsUnknownWorkOrderSkill { get; set; }
        public MapAreaName DisplayedLocation { get; set; }
        public Favor PrerequisiteFavorLevel { get; set; }
        public QuestGroupingName GroupingName { get; set; }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public PgSkill WorkOrderSkill { get; set; }
        public PgQuestCollection FollowUpQuestList { get; set; } = new PgQuestCollection();
        public PgQuestRequirementCollection QuestRequirementList { get; set; } = new PgQuestRequirementCollection();
        public PgQuestRequirementCollection QuestRequirementToSustainList { get; set; } = new PgQuestRequirementCollection();
        public int? RawReuseTime_Minutes { get; set; }
        public int? RawReuseTime_Hours { get; set; }
        public int? RawReuseTime_Days { get; set; }
        public string FavorNpcId { get; set; } = string.Empty;
        public string FavorNpcName { get; set; } = string.Empty;
        public MapAreaName FavorNpcArea { get; set; }
        public bool IsQuestRewardsItemListEmpty { get { return RawIsQuestRewardsItemListEmpty.HasValue && RawIsQuestRewardsItemListEmpty.Value; } }
        public bool? RawIsQuestRewardsItemListEmpty { get; set; }
        public bool IsEmptyNpc { get { return RawIsEmptyNpc.HasValue && RawIsEmptyNpc.Value; } }
        public bool? RawIsEmptyNpc { get; set; }
        public bool IsRewardRecipeEmpty { get { return RawIsRewardRecipeEmpty.HasValue && RawIsRewardRecipeEmpty.Value; } }
        public bool? RawIsRewardRecipeEmpty { get; set; }
        public bool IsQuestRequirementListSimple { get { return RawIsQuestRequirementListSimple.HasValue && RawIsQuestRequirementListSimple.Value; } }
        public bool? RawIsQuestRequirementListSimple { get; set; }
        public bool IsQuestRequirementListNested { get { return RawIsQuestRequirementListNested.HasValue && RawIsQuestRequirementListNested.Value; } }
        public bool? RawIsQuestRequirementListNested { get; set; }
        public bool IsLearnAbility { get { return RawIsLearnAbility.HasValue && RawIsLearnAbility.Value; } }
        public bool? RawIsLearnAbility { get; set; }
        public PgSkill RewardXpSkill { get; set; }
        public PgQuestRewardCollection QuestRewardList { get; set; } = new PgQuestRewardCollection();
        public List<string> RawRewardInteractionFlags { get; } = new List<string>();
        public PgPlayerTitle RewardTitle { get; set; }
        public PgQuestRewardCurrencyCollection RewardsCurrencyList { get; } = new PgQuestRewardCurrencyCollection();
        public PgQuestRewardItemCollection QuestMidwayGiveItemList { get; } = new PgQuestRewardItemCollection();
        public PgNpc QuestCompleteNpc { get; set; }
        public string QuestCompleteNpcName { get; set; } = string.Empty;
        public PgQuestRewardFavorCollection RewardsFavorList { get; } = new PgQuestRewardFavorCollection();
        public PgQuestRewardLevelCollection RewardsLevelList { get; } = new PgQuestRewardLevelCollection();
    }
}
