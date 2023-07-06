namespace Preprocessor;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

public class Quest
{
    public Quest(RawQuest rawQuest)
    {
        Description = rawQuest.Description;
        DisplayedLocation = Area.FromRawAreaName(rawQuest.DisplayedLocation, out OriginalDisplayedLocation);
        FavorNpc = rawQuest.FavorNpc;
        FollowUpQuests = rawQuest.FollowUpQuests;
        GroupingName = rawQuest.GroupingName;
        InternalName = rawQuest.InternalName;
        IsAutoPreface = rawQuest.IsAutoPreface;
        IsAutoWrapUp = rawQuest.IsAutoWrapUp;
        IsCancellable = rawQuest.IsCancellable;
        IsGuildQuest = rawQuest.IsGuildQuest;
        Keywords = rawQuest.Keywords;
        Level = rawQuest.Level;
        MidwayGiveItems = rawQuest.MidwayGiveItems;
        MidwayText = rawQuest.MidwayText;
        Name = rawQuest.Name;
        NumberOfExpectedParticipants = rawQuest.NumExpectedParticipants;
        Objectives = ParseObjectives(rawQuest.Objectives);
        PreGiveEffects = ParsePreGiveEffects(rawQuest.PreGiveEffects);
        PreGiveItems = rawQuest.PreGiveItems;
        PreGiveRecipes = rawQuest.PreGiveRecipes;
        PrefaceText = rawQuest.PrefaceText;
        PrerequisiteFavorLevel = rawQuest.PrerequisiteFavorLevel;
        QuestNpc = rawQuest.QuestNpc;
        Requirements = Preprocessor.ToSingleOrMultiple(rawQuest.Requirements, (RawRequirement rawRequirement) => new Requirement(rawRequirement), out RequirementsFormat);
        RequirementsToSustain = Preprocessor.ToSingleOrMultiple(rawQuest.RequirementsToSustain, (RawRequirement rawRequirement) => new Requirement(rawRequirement), out RequirementsToSustainFormat);
        ReuseTime = ParseReuseTime(rawQuest.ReuseTime_Days, rawQuest.ReuseTime_Hours, rawQuest.ReuseTime_Minutes);
        SuccessText = rawQuest.SuccessText;
        TSysLevel = rawQuest.TSysLevel;
        Version = rawQuest.Version;
        WorkOrderSkill = rawQuest.WorkOrderSkill;

        MergeSpecificRewards(rawQuest.Rewards, rawQuest.Reward_Favor, rawQuest.Rewards_Favor, rawQuest.Rewards_NamedLootProfile, rawQuest.Rewards_Effects, rawQuest.Rewards_Items);
    }

    private static QuestObjective[]? ParseObjectives(RawQuestObjective[]? rawQuestObjectives)
    {
        if (rawQuestObjectives is null)
            return null;

        List<QuestObjective> Result = new();
        foreach (RawQuestObjective RawQuestObjective in rawQuestObjectives)
            Result.Add(new QuestObjective(RawQuestObjective));

        return Result.ToArray();
    }

    private static QuestPreGive[]? ParsePreGiveEffects(string[]? rawPreGiveEffects)
    {
        if (rawPreGiveEffects is null)
            return null;

        List<QuestPreGive> Result = new();
        foreach (string RawPreGiveEffect in rawPreGiveEffects)
            Result.Add(ParsePreGiveEffect(RawPreGiveEffect));

        return Result.ToArray();
    }

    private static QuestPreGive ParsePreGiveEffect(string rawPreGiveEffect)
    {
        QuestPreGive Result;

        string EffectName;
        string EffectParameter;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(rawPreGiveEffect, ParameterPattern, RegexOptions.IgnoreCase);
        if (ParameterMatch.Success)
        {
            EffectName = rawPreGiveEffect.Substring(0, ParameterMatch.Index);
            EffectParameter = ParameterMatch.Value.Substring(1, ParameterMatch.Value.Length - 2);
        }
        else
        {
            EffectName = rawPreGiveEffect;
            EffectParameter = string.Empty;
        }

        switch (EffectName)
        {
            case "DeleteWarCacheMapFog":
                Result = new QuestPreGive() { T = "Effect", Description = "Delete War Cache Map Fog" };
                break;
            case "DeleteWarCacheMapPins":
                Result = new QuestPreGive() { T = "Effect", Description = "Delete War Cache Map Pins" };
                break;
            case "CreateIlmariWarCacheMap":
                Result = ParsePreGiveEffectCreateIlmariWarCacheMap(EffectParameter);
                break;
            case "SetInteractionFlag":
                Result = new QuestPreGive() { T = "SetInteractionFlag", InteractionFlag = EffectParameter };
                break;
            case "ClearInteractionFlag":
                Result = new QuestPreGive() { T = "ClearInteractionFlag", InteractionFlag = EffectParameter };
                break;
            case "LearnAbility":
                Result = new QuestPreGive() { T = "Ability", Ability = EffectParameter };
                break;
            default:
                throw new PreprocessorException();
        }

        return Result;
    }

    private static QuestPreGive ParsePreGiveEffectCreateIlmariWarCacheMap(string effectParameter)
    {
        string[] ParameterSplitted = effectParameter.Split(',');
        if (ParameterSplitted.Length != 2)
            throw new PreprocessorException();

        string Item = ParameterSplitted[0].Trim();
        string QuestGroup = ParameterSplitted[1].Trim();

        return new QuestPreGive() { T = "Item", Item = Item, QuestGroup = QuestGroup };
    }

    private static Time? ParseReuseTime(int? rawDays, int? rawHours, int? rawMinutes)
    {
        if (rawDays is null && rawHours is null && rawMinutes is null)
            return null;
        else
            return new Time() { Days = rawDays, Hours = rawHours, Minutes = rawMinutes };
    }

    private void MergeSpecificRewards(QuestReward[]? rawRewards, int? rawRewardFavor, int? rawRewardsFavor, string? rawRewardNamedLootProfile, string[]? rawRewardEffects, QuestRewardItem[]? rawRewardItems)
    {
        List<QuestReward> RewardList = new();

        if (rawRewards is not null)
            RewardList.AddRange(rawRewards.ToList());

        if (rawRewardFavor is not null)
        {
            HasRewardFavor = true;
            RewardList.Add(new QuestReward() { T = "Favor", Favor = rawRewardFavor });
        }

        if (rawRewardsFavor is not null)
        {
            HasRewardsFavor = true;
            RewardList.Add(new QuestReward() { T = "Favor", Favor = rawRewardsFavor });
        }

        if (rawRewardNamedLootProfile is not null)
        {
            HasRewardNamedLootProfile = true;
            RewardList.Add(new QuestReward() { T = "NamedLootProfile", NamedLootProfile = rawRewardNamedLootProfile });
        }

        if (rawRewardEffects is not null)
        {
            RewardEffectCount = rawRewardEffects.Length;

            List<QuestReward> RewardEffectList = new();
            foreach (string RewardEffect in rawRewardEffects)
            {
                QuestReward NewQuestReward = ParseRewardEffect(RewardEffect);
                RewardEffectList.Insert(0, NewQuestReward);
            }

            RewardList.AddRange(RewardEffectList);
        }

        if (rawRewardItems is not null)
        {
            RewardItemCount = rawRewardItems.Length;

            List<QuestReward> RewardItemList = new();
            foreach (QuestRewardItem RewardItem in rawRewardItems)
            {
                QuestReward NewQuestReward = new() { T = "Item", Item = RewardItem.Item, StackSize = RewardItem.StackSize };
                RewardItemList.Insert(0, NewQuestReward);
            }

            RewardList.AddRange(RewardItemList);
        }

        if (RewardList.Count > 0)
            Rewards = RewardList.ToArray();
        else
            Rewards = null;
    }

    private static QuestReward ParseRewardEffect(string rewardEffect)
    {
        string EffectName;
        string EffectParameter;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(rewardEffect, ParameterPattern, RegexOptions.IgnoreCase);
        if (ParameterMatch.Success)
        {
            EffectName = rewardEffect.Substring(0, ParameterMatch.Index);
            EffectParameter = ParameterMatch.Value.Substring(1, ParameterMatch.Value.Length - 2);
        }
        else
        {
            EffectName = rewardEffect;
            EffectParameter = string.Empty;
        }

        switch (EffectName)
        {
            case "SetInteractionFlag":
                return new QuestReward() { T = "InteractionFlag", InteractionFlag = EffectParameter };
            case "EnsureLoreBookKnown":
                return new QuestReward() { T = "LoreBook", LoreBook = EffectParameter };
            case "BestowTitle":
                return ParseRewardEffectBestowTitle(EffectParameter);
            case "BestowRecipe":
                return new QuestReward() { T = "Recipe", Recipe = EffectParameter };
            case "LearnAbility":
                return new QuestReward() { T = "Ability", Ability = EffectParameter };
            case "AdvanceScriptedQuestObjective":
                return ParseRewardEffectAdvanceScriptedQuestObjective(EffectParameter);
            case "GiveXP":
                return ParseRewardEffectGiveXP(EffectParameter);
            case "DeltaNpcFavor":
                return ParseRewardEffectDeltaNpcFavor(EffectParameter);
            case "RaiseSkillToLevel":
                return ParseRewardEffectRaiseSkillToLevel(EffectParameter);
            case "DispelFaeBombSporeBuff":
                return new QuestReward() { T = EffectName };
            default:
                return new QuestReward() { T = "Effect", Effect = EffectName };
        }
    }

    private static QuestReward ParseRewardEffectBestowTitle(string title)
    {
        int TitleKey = TitleToKeyMap[title];
        return new QuestReward() { T = "Title", Title = TitleKey };
    }

    private static QuestReward ParseRewardEffectAdvanceScriptedQuestObjective(string questObjective)
    {
        string[] Patterns = new string[]
        {
            "_Complete",
            "_Done",
        };

        QuestReward Result = new();
        string Npc;
        bool IsPatternFound = false;

        foreach (string Pattern in Patterns)
            if (questObjective.EndsWith(Pattern))
            {
                IsPatternFound = true;

                Npc = questObjective.Substring(0, questObjective.Length - Pattern.Length);

                QuestReward NewQuestReward = new QuestReward() { T = "ScriptedQuestObjective", Npc = Npc };
                NewQuestReward.SetObjectiveCompleteOrDone(Pattern);

                Result = NewQuestReward;
                break;
            }

        if (!IsPatternFound)
            throw new PreprocessorException();

        return Result;
    }

    private static QuestReward ParseRewardEffectGiveXP(string xpReward)
    {
        string[] XpSplitted = xpReward.Split(',');
        if (XpSplitted.Length != 2)
            throw new PreprocessorException();

        string Skill = XpSplitted[0].Trim();
        int Xp = int.Parse(XpSplitted[1]);

        return new QuestReward() { T = "SkillXp", Skill = Skill, Xp = Xp };
    }

    private static QuestReward ParseRewardEffectDeltaNpcFavor(string npcFavor)
    {
        string[] FavorSplitted = npcFavor.Split(',');
        if (FavorSplitted.Length != 2)
            throw new PreprocessorException();

        string Npc = FavorSplitted[0].Trim();
        int Favor = int.Parse(FavorSplitted[1]);

        return new QuestReward() { T = "Favor", Npc = Npc, Favor = Favor };
    }

    private static QuestReward ParseRewardEffectRaiseSkillToLevel(string levelReward)
    {
        string[] LevelSplitted = levelReward.Split(',');
        if (LevelSplitted.Length != 2)
            throw new PreprocessorException();

        string Skill = LevelSplitted[0].Trim();
        int Level = int.Parse(LevelSplitted[1]);

        return new QuestReward() { T = "SkillLevel", Skill = Skill, Level = Level };
    }

    public string? Description { get; set; }
    public string? DisplayedLocation { get; set; }
    public string? FavorNpc { get; set; }
    public string[]? FollowUpQuests { get; set; }
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
    public int? NumberOfExpectedParticipants { get; set; }
    public QuestObjective[]? Objectives { get; set; }
    public QuestPreGive[]? PreGiveEffects { get; set; }
    public QuestRewardItem[]? PreGiveItems { get; set; }
    public string[]? PreGiveRecipes { get; set; }
    public string? PrefaceText { get; set; }
    public string? PrerequisiteFavorLevel { get; set; }
    public string? QuestNpc { get; set; }
    public Requirement[]? Requirements { get; set; }
    public Requirement[]? RequirementsToSustain { get; set; }
    public Time? ReuseTime { get; set; }
    public QuestReward[]? Rewards { get; set; }
    public string? SuccessText { get; set; }
    public int? TSysLevel { get; set; }
    public int? Version { get; set; }
    public string? WorkOrderSkill { get; set; }

    public RawQuest ToRawQuest()
    {
        RawQuest Result = new();

        Result.Description = Description;
        Result.DisplayedLocation = Area.ToRawAreaName(DisplayedLocation, OriginalDisplayedLocation);
        Result.FavorNpc = FavorNpc;
        Result.FollowUpQuests = FollowUpQuests;
        Result.GroupingName = GroupingName;
        Result.InternalName = InternalName;
        Result.IsAutoPreface = IsAutoPreface;
        Result.IsAutoWrapUp = IsAutoWrapUp;
        Result.IsCancellable = IsCancellable;
        Result.IsGuildQuest = IsGuildQuest;
        Result.Keywords = Keywords;
        Result.Level = Level;
        Result.MidwayGiveItems = MidwayGiveItems;
        Result.MidwayText = MidwayText;
        Result.Name = Name;
        Result.NumExpectedParticipants = NumberOfExpectedParticipants;
        Result.Objectives = ToRawQuestObjectives(Objectives);
        Result.PreGiveEffects = ToRawPreGiveEffects(PreGiveEffects);
        Result.PreGiveItems = PreGiveItems;
        Result.PreGiveRecipes = PreGiveRecipes;
        Result.PrefaceText = PrefaceText;
        Result.PrerequisiteFavorLevel = PrerequisiteFavorLevel;
        Result.QuestNpc = QuestNpc;
        Result.Requirements = Preprocessor.FromSingleOrMultiple(Requirements, (Requirement requirement) => requirement.ToRawRequirement(), RequirementsFormat);
        Result.RequirementsToSustain = Preprocessor.FromSingleOrMultiple(RequirementsToSustain, (Requirement requirement) => requirement.ToRawRequirement(), RequirementsToSustainFormat);
        (Result.ReuseTime_Days, Result.ReuseTime_Hours, Result.ReuseTime_Minutes) = SplitReuseTime(ReuseTime);
        Result.SuccessText = SuccessText;
        Result.TSysLevel = TSysLevel;
        Result.Version = Version;
        Result.WorkOrderSkill = WorkOrderSkill;

        (Result.Rewards, Result.Reward_Favor, Result.Rewards_Favor, Result.Rewards_NamedLootProfile, Result.Rewards_Effects, Result.Rewards_Items) = SplitSpecificRewards();

        return Result;
    }

    private static RawQuestObjective[]? ToRawQuestObjectives(QuestObjective[]? questObjectives)
    {
        if (questObjectives is null)
            return null;

        List<RawQuestObjective> Result = new();

        foreach (QuestObjective QuestObjective in questObjectives)
            Result.Add(QuestObjective.ToRawQuestObjective());

        return Result.ToArray();
    }

    private static string[]? ToRawPreGiveEffects(QuestPreGive[]? preGiveEffects)
    {
        if (preGiveEffects is null)
            return null;

        List<string> Result = new();

        foreach (QuestPreGive PreGiveEffect in preGiveEffects)
            Result.Add(ToRawPreGiveEffect(PreGiveEffect));

        return Result.ToArray();
    }

    private static string ToRawPreGiveEffect(QuestPreGive effect)
    {
        string Result = string.Empty;

        switch (effect.T)
        {
            case "Effect":
                if (effect.Description == "Delete War Cache Map Fog")
                    Result = "DeleteWarCacheMapFog";
                else
                    Result = "DeleteWarCacheMapPins";
                break;
            case "Item":
                Result = $"CreateIlmariWarCacheMap({effect.Item},{effect.QuestGroup})";
                break;
            case "SetInteractionFlag":
                Result = $"SetInteractionFlag({effect.InteractionFlag})";
                break;
            case "ClearInteractionFlag":
                Result = $"ClearInteractionFlag({effect.InteractionFlag})";
                break;
            case "Ability":
                Result = $"LearnAbility({effect.Ability})";
                break;
        }

        Debug.Assert(Result != string.Empty);

        return Result;
    }

    private static (int?, int?, int?) SplitReuseTime(Time? time)
    {
        if (time is null)
            return (null, null, null);
        else
            return (time.Days, time.Hours, time.Minutes);
    }

    private (QuestReward[]?, int?, int?, string?, string[]?, QuestRewardItem[]?) SplitSpecificRewards()
    {
        if (Rewards is null)
            return (null, null, null, null, null, null);

        int RewardIndex = Rewards.Length - 1;

        QuestRewardItem[]? RawRewardQuestItems;
        if (RewardItemCount > 0)
        {
            RawRewardQuestItems = new QuestRewardItem[RewardItemCount];

            for (int i = 0; i < RewardItemCount; i++)
            {
                RawRewardQuestItems[i] = new QuestRewardItem() { Item = Rewards[RewardIndex].Item, StackSize = Rewards[RewardIndex].StackSize };
                RewardIndex--;
            }
        }
        else
            RawRewardQuestItems = null;

        string[]? RawRewardEffects;
        if (RewardEffectCount > 0)
        {
            RawRewardEffects = new string[RewardEffectCount];

            for (int i = 0; i < RewardEffectCount; i++)
                RawRewardEffects[i] = ToRawRewardEffect(Rewards[RewardIndex--]);
        }
        else
            RawRewardEffects = null;

        string? RawRewardNamedLootProfile;
        if (HasRewardNamedLootProfile)
            RawRewardNamedLootProfile = Rewards[RewardIndex--].NamedLootProfile;
        else
            RawRewardNamedLootProfile = null;

        int? RawRewardsFavor;
        if (HasRewardsFavor)
            RawRewardsFavor = Rewards[RewardIndex--].Favor;
        else
            RawRewardsFavor = null;

        int? RawRewardFavor;
        if (HasRewardFavor)
            RawRewardFavor = Rewards[RewardIndex--].Favor;
        else
            RawRewardFavor = null;

        QuestReward[]? RawRewards;
        if (RewardIndex >= 0)
        {
            RawRewards = new QuestReward[RewardIndex + 1];
            for (int i = 0; i <=  RewardIndex; i++)
                RawRewards[i] = Rewards[i];
        }
        else
            RawRewards = null;

        return (RawRewards, RawRewardFavor, RawRewardsFavor, RawRewardNamedLootProfile, RawRewardEffects, RawRewardQuestItems);
    }

    private static string ToRawRewardEffect(QuestReward reward)
    {
        switch (reward.T)
        {
            case "InteractionFlag":
                return $"SetInteractionFlag({reward.InteractionFlag})";
            case "LoreBook":
                return $"EnsureLoreBookKnown({reward.LoreBook})";
            case "Title":
                return $"BestowTitle({RewardToBestowedTitle(reward.Title!.Value)})";
            case "Recipe":
                return $"BestowRecipe({reward.Recipe})";
            case "Ability":
                return $"LearnAbility({reward.Ability})";
            case "ScriptedQuestObjective":
                return $"AdvanceScriptedQuestObjective({reward.Npc}{reward.GetObjectiveCompleteOrDone()})";
            case "SkillXp":
                return $"GiveXP({reward.Skill},{reward.Xp})";
            case "Favor":
                return $"DeltaNpcFavor({reward.Npc},{reward.Favor})";
            case "SkillLevel":
                return $"RaiseSkillToLevel({reward.Skill},{reward.Level})";
            case "DispelFaeBombSporeBuff":
                return reward.T;
            default:
                return reward.Effect!;
        }
    }

    private static string RewardToBestowedTitle(int title)
    {
        Dictionary<int, string> KeyToTitleMap = TitleToKeyMap.ToDictionary(x => x.Value, x => x.Key);
        return KeyToTitleMap[title];
    }

    private static readonly Dictionary<string, int> TitleToKeyMap = new Dictionary<string, int>()
    {
        { "Event_Halloween_CultistOfZhiaLian", 5009 },
        { "Event_Halloween_SeniorCultistOfZhiaLian", 5010 },
        { "IncredibleGoblinKissAss", 5012 },
        { "GuideEvent_CivilServant", 5210 },
        { "GuideEvent_IKnowBunFu", 5211 },
        { "GuideEvent_SaviorOfTheGoats", 5208 },
        { "GuideEvent_AntiSaviorOfTheGoats", 5216 },
        { "Event_Halloween_HeartBeater", 5015 },
        { "Event_Halloween_Riiiiiiiiiii", 5017 },
        { "GuideEvent_TurkeyWrangler", 5212 },
        { "GuideEvent_ALittleFruity", 5219 },
        { "Warsmith", 5018 },
        { "GuideEvent_LikeABoss", 5222 },
        { "Event_TurkeyKiller", 5223 },
        { "GuideEvent_EggsellentHunter", 5209 },
        { "Event_Halloween_NotAfraidOfLungs", 5024 },
    };

    private string? OriginalDisplayedLocation;
    private JsonArrayFormat RequirementsFormat;
    private JsonArrayFormat RequirementsToSustainFormat;
    private bool HasRewardFavor;
    private bool HasRewardsFavor;
    private bool HasRewardNamedLootProfile;
    private int RewardEffectCount;
    private int RewardItemCount;
}
