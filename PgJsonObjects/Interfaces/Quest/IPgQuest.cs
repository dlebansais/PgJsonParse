using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuest : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        string InternalName { get; }
        string Name { get; }
        string Description { get; }
        int Version { get; }
        int? RawVersion { get; }
        IPgQuestObjectiveCollection QuestObjectiveList { get; }
        IPgQuestRewardXpCollection RewardsXPList { get; }
        IPgQuestRewardItemCollection QuestRewardsItemList { get; }
        TimeSpan? RawReuseTime { get; }
        int RewardCombatXP { get; }
        int? RawRewardCombatXP { get; }
        IPgGameNpc FavorNpc { get; }
        string PrefaceText { get; }
        string SuccessText { get; }
        string MidwayText { get; }
        IPgAbility RewardAbility { get; }
        int RewardFavor { get; }
        int? RawRewardFavor { get; }
        IPgSkill RewardSkill { get; }
        int RewardSkillXp { get; }
        int? RawRewardSkillXp { get; }
        IPgRecipe RewardRecipe { get; }
        int RewardGuildXp { get; }
        int? RawRewardGuildXp { get; }
        int RewardGuildCredits { get; }
        int? RawRewardGuildCredits { get; }
        IPgQuestRewardItemCollection PreGiveItemList { get; }
        int TSysLevel { get; }
        int? RawTSysLevel { get; }
        int RewardGold { get; }
        int? RawRewardGold { get; }
        string RewardsNamedLootProfile { get; }
        IPgRecipeCollection PreGiveRecipeList { get; }
        List<QuestKeyword> KeywordList { get; }
        IPgEffectCollection RewardEffectList { get; }
        IPgLoreBook RewardLoreBook { get; }
        bool IsCancellable { get; }
        bool? RawIsCancellable { get; }
        bool IsAutoPreface { get; }
        bool? RawIsAutoPreface { get; }
        bool IsAutoWrapUp { get; }
        bool? RawIsAutoWrapUp { get; }
        bool IsGuildQuest { get; }
        bool? RawIsGuildQuest { get; }
        bool IsUnknownWorkOrderSkill { get; }
        bool? RawIsUnknownWorkOrderSkill { get; }
        MapAreaName DisplayedLocation { get; }
        Favor PrerequisiteFavorLevel { get; }
        QuestGroupingName GroupingName { get; }
        int NumExpectedParticipants { get; }
        int? RawNumExpectedParticipants { get; }
        int Level { get; }
        int? RawLevel { get; }
        IPgSkill WorkOrderSkill { get; }
        IPgQuestCollection FollowUpQuestList { get; }
        IPgQuestRequirementCollection QuestRequirementList { get; }
        IPgQuestRequirementCollection QuestRequirementToSustainList { get; }
        int? RawReuseTime_Minutes { get; }
        int? RawReuseTime_Hours { get; }
        int? RawReuseTime_Days { get; }
        string FavorNpcId { get; }
        string FavorNpcName { get; }
        MapAreaName FavorNpcArea { get; }
        bool IsQuestRewardsItemListEmpty { get; }
        bool? RawIsQuestRewardsItemListEmpty { get; }
        bool IsEmptyNpc { get; }
        bool? RawIsEmptyNpc { get; }
        bool IsRewardRecipeEmpty { get; }
        bool? RawIsRewardRecipeEmpty { get; }
        bool IsQuestRequirementListSimple { get; }
        bool? RawIsQuestRequirementListSimple { get; }
        bool IsQuestRequirementListNested { get; }
        bool? RawIsQuestRequirementListNested { get; }
        IPgQuestRewardCollection QuestRewardList { get; }
        List<string> RawRewardInteractionFlags { get; }
        IPgPlayerTitle RewardTitle { get; }
        IPgQuestRewardCurrencyCollection RewardsCurrencyList { get; }
        IPgQuestRewardItemCollection QuestMidwayGiveItemList { get; }
        IPgGameNpc QuestCompleteNpc { get; }
        string QuestCompleteNpcName { get; }
        IPgQuestRewardFavorCollection RewardsFavorList { get; }
        IPgQuestRewardLevelCollection RewardsLevelList { get; }
    }
}
