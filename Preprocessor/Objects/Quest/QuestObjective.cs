namespace Preprocessor;

using System;
using System.Collections.Generic;

internal class QuestObjective
{
    public QuestObjective(RawQuestObjective rawQuestObjective)
    {
        AbilityKeyword = rawQuestObjective.AbilityKeyword;
        AllowedFishingZone = rawQuestObjective.AllowedFishingZone;
        AnatomyType = rawQuestObjective.AnatomyType;
        BehaviorId = rawQuestObjective.BehaviorId;
        Description = rawQuestObjective.Description;
        FishConfig = rawQuestObjective.FishConfig;
        GroupId = rawQuestObjective.GroupId;
        InteractionFlags = rawQuestObjective.InteractionFlags;
        InternalName = rawQuestObjective.InternalName;
        IsHiddenUntilEarlierObjectivesComplete = rawQuestObjective.IsHiddenUntilEarlierObjectivesComplete;
        Item = rawQuestObjective.Item;
        ItemKeyword = rawQuestObjective.ItemKeyword;
        ItemName = rawQuestObjective.ItemName;
        MaxAmount = rawQuestObjective.MaxAmount;
        MaxFavorReceived = rawQuestObjective.MaxFavorReceived;
        MinAmount = rawQuestObjective.MinAmount;
        MinFavorReceived = rawQuestObjective.MinFavorReceived;
        MonsterTypeTag = rawQuestObjective.MonsterTypeTag;
        NumToDeliver = rawQuestObjective.NumToDeliver;
        Number = rawQuestObjective.Number;
        Requirements = Preprocessor.ToSingleOrMultiple<Requirement>(rawQuestObjective.Requirements, out IsSingleRequirements);
        ResultItemKeyword = rawQuestObjective.ResultItemKeyword;
        Skill = rawQuestObjective.Skill;
        StringParam = rawQuestObjective.StringParam;
        Target = Preprocessor.ToSingleOrMultiple<string>(rawQuestObjective.Target, out IsSingleTarget);
        Type = rawQuestObjective.Type;

        if (Type is string TypeString && TypeString == "Kill" && AbilityKeyword is not null)
        {
            IsKillWithAbility = true;

            List<Requirement> NewRequirements = new();

            if (Requirements is not null)
                NewRequirements.AddRange(Requirements);

            NewRequirements.Add(new Requirement() { T = "UseAbility", AbilityKeyword = AbilityKeyword });

            Requirements = NewRequirements.ToArray();
            AbilityKeyword = null;
        }
    }

    public string? AbilityKeyword { get; set; }
    public string? AllowedFishingZone { get; set; }
    public string? AnatomyType { get; set; }
    public string? BehaviorId { get; set; }
    public string? Description { get; set; }
    public string? FishConfig { get; set; }
    public int? GroupId { get; set; }
    public string[]? InteractionFlags { get; set; }
    public string? InternalName { get; set; }
    public bool? IsHiddenUntilEarlierObjectivesComplete { get; set; }
    public string? Item { get; set; }
    public string? ItemKeyword { get; set; }
    public string? ItemName { get; set; }
    public string? MaxAmount { get; set; }
    public string? MaxFavorReceived { get; set; }
    public string? MinAmount { get; set; }
    public string? MinFavorReceived { get; set; }
    public string? MonsterTypeTag { get; set; }
    public string? NumToDeliver { get; set; }
    public int? Number { get; set; }
    public Requirement[]? Requirements { get; set; }
    public string? ResultItemKeyword { get; set; }
    public string? Skill { get; set; }
    public string? StringParam { get; set; }
    public string[]? Target { get; set; }
    public string? Type { get; set; }

    public RawQuestObjective ToRawQuestObjective()
    {
        RawQuestObjective Result = new();

        Result.AbilityKeyword = AbilityKeyword;
        Result.AllowedFishingZone = AllowedFishingZone;
        Result.AnatomyType = AnatomyType;
        Result.BehaviorId = BehaviorId;
        Result.Description = Description;
        Result.FishConfig = FishConfig;
        Result.GroupId = GroupId;
        Result.InteractionFlags = InteractionFlags;
        Result.InternalName = InternalName;
        Result.IsHiddenUntilEarlierObjectivesComplete = IsHiddenUntilEarlierObjectivesComplete;
        Result.Item = Item;
        Result.ItemKeyword = ItemKeyword;
        Result.ItemName = ItemName;
        Result.MaxAmount = MaxAmount;
        Result.MaxFavorReceived = MaxFavorReceived;
        Result.MinAmount = MinAmount;
        Result.MinFavorReceived = MinFavorReceived;
        Result.MonsterTypeTag = MonsterTypeTag;
        Result.NumToDeliver = NumToDeliver;
        Result.Number = Number;
        Result.Requirements = Preprocessor.FromSingleOrMultiple(Requirements, IsSingleRequirements);
        Result.ResultItemKeyword = ResultItemKeyword;
        Result.Skill = Skill;
        Result.StringParam = StringParam;
        Result.Target = Preprocessor.FromSingleOrMultiple(Target, IsSingleTarget);
        Result.Type = Type;

        if (IsKillWithAbility)
        {
            List<Requirement> NewRequirements = new();

            if (Requirements is not null)
                NewRequirements.AddRange(Requirements);

            Result.AbilityKeyword = NewRequirements[NewRequirements.Count - 1].AbilityKeyword;

            if (NewRequirements.Count > 1)
            {
                NewRequirements.RemoveAt(NewRequirements.Count - 1);
                Result.Requirements = NewRequirements.ToArray();
            }
            else
                Result.Requirements = null;
        }

        return Result;
    }

    private bool IsSingleRequirements;
    private bool IsSingleTarget;
    private bool IsKillWithAbility;
}
