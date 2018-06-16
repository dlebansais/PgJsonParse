using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuest
    {
        string InternalName { get; }
        string Name { get; }
        string Description { get; }
        int Version { get; }
        int? RawVersion { get; }
        List<QuestObjective> QuestObjectiveList { get; }
        List<QuestRewardXp> RewardsXPList { get; }
        List<QuestRewardItem> QuestRewardsItemList { get; }
        TimeSpan? RawReuseTime { get; }
        int RewardCombatXP { get; }
        int? RawRewardCombatXP { get; }
        GameNpc FavorNpc { get; }
        string PrefaceText { get; }
        string SuccessText { get; }
        string MidwayText { get; }
        Ability RewardAbility { get; }
        int RewardFavor { get; }
        int? RawRewardFavor { get; }
        Skill RewardSkill { get; }
        int RewardSkillXp { get; }
        int? RawRewardSkillXp { get; }
        Recipe RewardRecipe { get; }
        int RewardGuildXp { get; }
        int? RawRewardGuildXp { get; }
        int RewardGuildCredits { get; }
        int? RawRewardGuildCredits { get; }
        List<QuestRewardItem> PreGiveItemList { get; }
        int TSysLevel { get; }
        int? RawTSysLevel { get; }
        int RewardGold { get; }
        int? RawRewardGold { get; }
        string RewardsNamedLootProfile { get; }
        List<Recipe> PreGiveRecipeList { get; }
        List<QuestKeyword> KeywordList { get; }
        Effect RewardEffect { get; }
        LoreBook RewardLoreBook { get; }
        bool IsCancellable { get; }
        bool? RawIsCancellable { get; }
        bool IsAutoPreface { get; }
        bool? RawIsAutoPreface { get; }
        bool IsAutoWrapUp { get; }
        bool? RawIsAutoWrapUp { get; }
        bool IsGuildQuest { get; }
        bool? RawIsGuildQuest { get; }
        MapAreaName DisplayedLocation { get; }
        Favor PrerequisiteFavorLevel { get; }
        QuestGroupingName GroupingName { get; }
        int NumExpectedParticipants { get; }
        int? RawNumExpectedParticipants { get; }
        int Level { get; }
        int? RawLevel { get; }
        Skill WorkOrderSkill { get; }
        List<Quest> FollowUpQuestList { get; }
        List<QuestRequirement> QuestRequirementList { get; }
        List<QuestRequirement> QuestRequirementToSustainList { get; }
    }
}
