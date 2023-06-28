namespace Preprocessor;

using System;
using System.Collections.Generic;

internal class QuestObjective
{
    private const string AreaHeader = "Area:";

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
        Requirements = Preprocessor.ToSingleOrMultiple<Requirement>(rawQuestObjective.Requirements, out RequirementsFormat);
        ResultItemKeyword = rawQuestObjective.ResultItemKeyword;
        Skill = rawQuestObjective.Skill;
        StringParam = rawQuestObjective.StringParam;
        Type = rawQuestObjective.Type;

        // For kill objectives, move AbilityKeyword to a requirement.
        if (Type is string TypeString && TypeString == "Kill" && AbilityKeyword is not null)
        {
            RequirementKillWithAbility = new Requirement() { T = "UseAbility", AbilityKeyword = AbilityKeyword };
            Requirements = AddRequirement(Requirements, RequirementKillWithAbility);
            AbilityKeyword = null;
        }

        // For targets in a specific area, move the area to a requirement.
        string[]? Targets = Preprocessor.ToSingleOrMultiple<string>(rawQuestObjective.Target, out TargetFormat);
        if (Targets is not null)
        {
            if (Targets.Length == 1)
                Target = Targets[0];
            else if (Targets.Length == 2)
            {
                Target = Targets[0].Trim();
                string TargetArea = Targets[1].Trim();

                if (TargetArea.StartsWith(AreaHeader))
                {
                    string AreaEvent = TargetArea.Substring(AreaHeader.Length);

                    RequirementTargetInArea = new Requirement() { T = "AreaEventOff", AreaEvent = AreaEvent };
                    Requirements = AddRequirement(Requirements, RequirementTargetInArea);
                }
                else
                    throw new InvalidCastException();
            }
            else
                throw new InvalidCastException();
        }
    }

    private static Requirement[]? AddRequirement(Requirement[]? requirements, Requirement newRequirement)
    {
        List<Requirement> NewRequirements = new();

        if (requirements is not null)
            NewRequirements.AddRange(requirements);

        NewRequirements.Add(newRequirement);

        return NewRequirements.ToArray();
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
    public string? Target { get; set; }
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
        Result.Requirements = Preprocessor.FromSingleOrMultiple(Requirements, RequirementsFormat);
        Result.ResultItemKeyword = ResultItemKeyword;
        Result.Skill = Skill;
        Result.StringParam = StringParam;
        Result.Type = Type;

        if (RequirementKillWithAbility is not null)
        {
            Result.AbilityKeyword = RequirementKillWithAbility.AbilityKeyword;
            Result.Requirements = RemoveRequirement(Result.Requirements, RequirementKillWithAbility);
        }

        string[]? Targets;
        if (Target is not null)
        {
            if (RequirementTargetInArea is not null)
            {
                string TargetArea = $"{AreaHeader}{RequirementTargetInArea.AreaEvent}";
                Targets = new string[] { Target, TargetArea };

                Result.Requirements = RemoveRequirement(Result.Requirements, RequirementTargetInArea);
            }
            else
                Targets = new string[] { Target };
        }
        else
            Targets = null;
        Result.Target = Preprocessor.FromSingleOrMultiple(Targets, TargetFormat);

        return Result;
    }

    private static Requirement[]? RemoveRequirement(object? requirements, Requirement removedRequirement)
    {
        List<Requirement> NewRequirements = new();

        if (requirements is Requirement[] ExistingRequirements)
            NewRequirements.AddRange(ExistingRequirements);

        NewRequirements.Remove(removedRequirement);

        if (NewRequirements.Count > 0)
            return NewRequirements.ToArray();
        else
            return null;
    }

    private JsonArrayFormat RequirementsFormat;
    private JsonArrayFormat TargetFormat;
    private Requirement? RequirementKillWithAbility;
    private Requirement? RequirementTargetInArea;
}
