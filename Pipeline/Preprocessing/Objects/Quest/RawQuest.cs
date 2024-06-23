namespace Preprocessor;

public class RawQuest
{
    public bool? CheckRequirementsToSustainOnBestow { get; set; }
    public string? Description { get; set; }
    public string? DisplayedLocation { get; set; }
    public string? FavorNpc { get; set; }
    public string[]? FollowUpQuests { get; set; }
    public bool? ForceBookOnWrapUp { get; set; }
    public string? GroupingName { get; set; }
    public string? InternalName { get; set; }
    public bool? IsAutoPreface { get; set; }
    public bool? IsAutoWrapUp { get; set; }
    public bool? IsCancellable { get; set; }
    public bool? IsGuildQuest { get; set; }
    public string[]? Keywords { get; set; }
    public int? Level { get; set; }
    public QuestRewardItem[]? MidwayGiveItems { get; set; }
    public string? MidwayText { get; set; }
    public string? Name { get; set; }
    public int? NumExpectedParticipants { get; set; }
    public RawQuestObjective[]? Objectives { get; set; }
    public string[]? PreGiveEffects { get; set; }
    public QuestRewardItem[]? PreGiveItems { get; set; }
    public string[]? PreGiveRecipes { get; set; }
    public string? PrefaceText { get; set; }
    public string? PrerequisiteFavorLevel { get; set; }
    public string[]? QuestFailEffects { get; set; }
    public string? QuestNpc { get; set; }
    public object? Requirements { get; set; }
    public object? RequirementsToSustain { get; set; }
    public int? ReuseTime_Days { get; set; }
    public int? ReuseTime_Hours { get; set; }
    public int? ReuseTime_Minutes { get; set; }
    public int? Reward_Favor { get; set; }
    public QuestReward[]? Rewards { get; set; }
    public string? Rewards_Description { get; set; }
    public string[]? Rewards_Effects { get; set; }
    public int? Rewards_Favor { get; set; }
    public QuestRewardItem[]? Rewards_Items { get; set; }
    public string? Rewards_NamedLootProfile { get; set; }
    public string? SuccessText { get; set; }
    public int? TSysLevel { get; set; }
    public int? Version { get; set; }
    public string? WorkOrderSkill { get; set; }
}
