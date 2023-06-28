namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal class Quest
{
    public Quest(RawQuest rawQuest)
    {
        Description = rawQuest.Description;
        DisplayedLocation = rawQuest.DisplayedLocation;
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
        Requirements = Preprocessor.ToSingleOrMultiple<Requirement>(rawQuest.Requirements, out RequirementsFormat);
        RequirementsToSustain = Preprocessor.ToSingleOrMultiple<Requirement>(rawQuest.RequirementsToSustain, out RequirementsToSustainFormat);
        ReuseTime = ParseReuseTime(rawQuest.ReuseTime_Days, rawQuest.ReuseTime_Hours, rawQuest.ReuseTime_Minutes);
        Rewards_Items = rawQuest.Rewards_Items;
        SuccessText = rawQuest.SuccessText;
        TSysLevel = rawQuest.TSysLevel;
        Version = rawQuest.Version;
        WorkOrderSkill = rawQuest.WorkOrderSkill;

        MergeSpecificRewards(rawQuest.Rewards, rawQuest.Reward_Favor, rawQuest.Rewards_Favor, rawQuest.Rewards_NamedLootProfile, rawQuest.Rewards_Effects);
    }

    private static QuestObjective[]? ParseObjectives(RawQuestObjective[]? rawQuestObjectives)
    {
        if (rawQuestObjectives is null)
            return null;

        QuestObjective[] Result = new QuestObjective[rawQuestObjectives.Length];
        for (int i = 0; i < rawQuestObjectives.Length; i++)
            Result[i] = new QuestObjective(rawQuestObjectives[i]);

        return Result;
    }

    private static QuestPreGive[]? ParsePreGiveEffects(string[]? content)
    {
        if (content is null)
            return null;

        QuestPreGive[] Result = new QuestPreGive[content.Length];
        for (int i = 0; i < content.Length; i++)
            Result[i] = ParsePreGiveEffect(content[i]);

        return Result;
    }

    private static QuestPreGive ParsePreGiveEffect(string effect)
    {
        string EffectName;
        string EffectParameter;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(effect, ParameterPattern, RegexOptions.IgnoreCase);
        if (ParameterMatch.Success)
        {
            EffectName = effect.Substring(0, ParameterMatch.Index);
            EffectParameter = ParameterMatch.Value.Substring(1, ParameterMatch.Value.Length - 2);
        }
        else
        {
            EffectName = effect;
            EffectParameter = string.Empty;
        }

        switch (EffectName)
        {
            case "DeleteWarCacheMapFog":
                return new QuestPreGive() { T = "Effect", Description = "Delete War Cache Map Fog" };
            case "DeleteWarCacheMapPins":
                return new QuestPreGive() { T = "Effect", Description = "Delete War Cache Map Pins" };
            case "CreateIlmariWarCacheMap":
                return ParsePreGiveEffectCreateIlmariWarCacheMap(EffectParameter);
            case "SetInteractionFlag":
                return new QuestPreGive() { T = "SetInteractionFlag", InteractionFlag = EffectParameter };
            case "ClearInteractionFlag":
                return new QuestPreGive() { T = "ClearInteractionFlag", InteractionFlag = EffectParameter };
            case "LearnAbility":
                return new QuestPreGive() { T = "Ability", Ability = EffectParameter };
            default:
                throw new InvalidCastException();
        }
    }

    private static QuestPreGive ParsePreGiveEffectCreateIlmariWarCacheMap(string effectParameter)
    {
        string[] ParameterSplitted = effectParameter.Split(',');
        if (ParameterSplitted.Length != 2)
            throw new InvalidCastException();

        string Item = ParameterSplitted[0].Trim();
        string QuestGroup = ParameterSplitted[1].Trim();

        return new QuestPreGive() { T = "Item", Item = Item, QuestGroup = QuestGroup };
    }

    private static Time ParseReuseTime(int? rawDays, int? rawHours, int? rawMinutes)
    {
        return new Time() { Days = rawDays, Hours = rawHours, Minutes = rawMinutes };
    }

    private void MergeSpecificRewards(QuestReward[]? rawRewards, int? rawRewardFavor, int? rawRewardsFavor, string? rawRewardNamedLootProfile, string[]? rawRewardEffects)
    {
        List<QuestReward> RewardList = new();

        if (rawRewards is not null)
            RewardList.AddRange(rawRewards.ToList());

        if (rawRewardFavor is not null)
        {
            HasRewardFavor = true;

            if (rawRewardFavor == 0)
                HasRewardFavorZero = true;
            else
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

        string Npc;

        foreach (string Pattern in Patterns)
            if (questObjective.EndsWith(Pattern))
            {
                Npc = questObjective.Substring(0, questObjective.Length - Pattern.Length);

                QuestReward NewQuestReward = new QuestReward() { T = "ScriptedQuestObjective", Npc = Npc };
                NewQuestReward.SetObjectiveCompleteOrDone(Pattern);

                return NewQuestReward;
            }

        throw new InvalidCastException();
    }

    private static QuestReward ParseRewardEffectGiveXP(string xpReward)
    {
        string[] XpSplitted = xpReward.Split(',');
        if (XpSplitted.Length != 2)
            throw new InvalidCastException();

        string Skill = XpSplitted[0].Trim();
        int Xp = int.Parse(XpSplitted[1]);

        return new QuestReward() { T = "SkillXp", Skill = Skill, Xp = Xp };
    }

    private static QuestReward ParseRewardEffectDeltaNpcFavor(string npcFavor)
    {
        string[] FavorSplitted = npcFavor.Split(',');
        if (FavorSplitted.Length != 2)
            throw new InvalidCastException();

        string Npc = FavorSplitted[0].Trim();
        int Favor = int.Parse(FavorSplitted[1]);

        return new QuestReward() { T = "Favor", Npc = Npc, Favor = Favor };
    }

    private static QuestReward ParseRewardEffectRaiseSkillToLevel(string levelReward)
    {
        string[] LevelSplitted = levelReward.Split(',');
        if (LevelSplitted.Length != 2)
            throw new InvalidCastException();

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
    public QuestRewardItem[]? Rewards_Items { get; set; }
    public string? SuccessText { get; set; }
    public int? TSysLevel { get; set; }
    public int? Version { get; set; }
    public string? WorkOrderSkill { get; set; }

    public RawQuest ToRawQuest()
    {
        RawQuest Result = new();

        Result.Description = Description;
        Result.DisplayedLocation = DisplayedLocation;
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
        Result.Requirements = Preprocessor.FromSingleOrMultiple(Requirements, RequirementsFormat);
        Result.RequirementsToSustain = Preprocessor.FromSingleOrMultiple(RequirementsToSustain, RequirementsToSustainFormat);
        (Result.ReuseTime_Days, Result.ReuseTime_Hours, Result.ReuseTime_Minutes) = SplitReuseTime(ReuseTime);
        Result.Rewards_Items = Rewards_Items;
        Result.SuccessText = SuccessText;
        Result.TSysLevel = TSysLevel;
        Result.Version = Version;
        Result.WorkOrderSkill = WorkOrderSkill;

        (Result.Rewards, Result.Reward_Favor, Result.Rewards_Favor, Result.Rewards_NamedLootProfile, Result.Rewards_Effects) = SplitSpecificRewards();

        return Result;
    }

    private static RawQuestObjective[]? ToRawQuestObjectives(QuestObjective[]? questObjectives)
    {
        if (questObjectives is null)
            return null;

        RawQuestObjective[] Result = new RawQuestObjective[questObjectives.Length];
        for (int i = 0; i < questObjectives.Length; i++)
            Result[i] = questObjectives[i].ToRawQuestObjective();

        return Result;
    }

    private static string[]? ToRawPreGiveEffects(QuestPreGive[]? questPreGive)
    {
        if (questPreGive is null)
            return null;

        string[] Result = new string[questPreGive.Length];
        for (int i = 0; i < questPreGive.Length; i++)
            Result[i] = ToRawPreGiveEffect(questPreGive[i]);

        return Result;
    }

    private static string ToRawPreGiveEffect(QuestPreGive effect)
    {
        switch (effect.T)
        {
            case "Effect":
                if (effect.Description == "Delete War Cache Map Fog")
                    return "DeleteWarCacheMapFog";
                else if (effect.Description == "Delete War Cache Map Pins")
                    return "DeleteWarCacheMapPins";
                else
                    throw new InvalidCastException();
            case "Item":
                return $"CreateIlmariWarCacheMap({effect.Item},{effect.QuestGroup})";
            case "SetInteractionFlag":
                return $"SetInteractionFlag({effect.InteractionFlag})";
            case "ClearInteractionFlag":
                return $"ClearInteractionFlag({effect.InteractionFlag})";
            case "Ability":
                return $"LearnAbility({effect.Ability})";
            default:
                throw new InvalidCastException();
        }
    }

    private static (int?, int?, int?) SplitReuseTime(Time? time)
    {
        if (time is null)
            return (null, null, null);
        else
            return (time.Days, time.Hours, time.Minutes);
    }

    private (QuestReward[]?, int?, int?, string?, string[]?) SplitSpecificRewards()
    {
        if (Rewards is null)
        {
            if (HasRewardFavorZero)
                return (null, 0, null, null, null);
            else
                return (null, null, null, null, null);
        }

        int RewardIndex = Rewards.Length - 1;

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
        {
            if (HasRewardFavorZero)
                RawRewardFavor = 0;
            else
                RawRewardFavor = Rewards[RewardIndex--].Favor;
        }
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

        return (RawRewards, RawRewardFavor, RawRewardsFavor, RawRewardNamedLootProfile, RawRewardEffects);
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
                return $"BestowTitle({RewardToBestowedTitle(reward.Title ?? throw new NullReferenceException())})";
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
                return reward.T ?? throw new NullReferenceException();
            default:
                return $"{reward.Effect}";
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

    private JsonArrayFormat RequirementsFormat;
    private JsonArrayFormat RequirementsToSustainFormat;
    private bool HasRewardFavor;
    private bool HasRewardFavorZero;
    private bool HasRewardsFavor;
    private bool HasRewardNamedLootProfile;
    private int RewardEffectCount;
}
