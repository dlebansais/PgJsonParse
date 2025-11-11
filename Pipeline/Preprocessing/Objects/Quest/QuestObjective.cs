namespace Preprocessor;

using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class QuestObjective
{
    private const string AreaHeader = "Area:";

    public QuestObjective(RawQuestObjective rawQuestObjective)
    {
        if (rawQuestObjective.AbilityKeyword is not null && rawQuestObjective.AbilityKeyword.Length == 0)
        {
            AbilityKeyword = "Empty";
            EmptyAbilityKeyword = true;
        }
        else
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
        NumberToDeliver = ParseNumToDeliver(rawQuestObjective.NumToDeliver);
        Number = rawQuestObjective.Number;
        Requirements = Preprocessor.ToSingleOrMultiple(rawQuestObjective.Requirements, (RawRequirement rawRequirement) => new Requirement(rawRequirement), out RequirementsFormat);
        ResultItemKeyword = rawQuestObjective.ResultItemKeyword;
        Skill = rawQuestObjective.Skill;
        StringParam = rawQuestObjective.StringParam;
        Type = rawQuestObjective.Type;

        // For kill objectives, move AbilityKeyword to a requirement.
        if (Type is string TypeString && TypeString == "Kill" && AbilityKeyword is not null)
        {
            RequirementKillWithAbility = new Requirement(new RawRequirement() { T = "UseAbility", AbilityKeyword = AbilityKeyword });
            Requirements = AddRequirement(Requirements, RequirementKillWithAbility, out RequirementIndexKillWithAbility);
            AbilityKeyword = null;
        }

        // For targets in a specific area, move the area to a requirement.
        string[]? Targets = Preprocessor.ToSingleOrMultiple(rawQuestObjective.Target, (string s) => s, out TargetFormat);
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

                    RequirementTargetInArea = new Requirement(new RawRequirement() { T = "AreaEventOff", AreaEvent = AreaEvent });
                    Requirements = AddRequirement(Requirements, RequirementTargetInArea, out RequirementIndexTargetInArea);
                }
                else
                    throw new PreprocessorException(this);
            }
            else
                throw new PreprocessorException(this);

            if (Target == "*")
                Target = "Any";
        }
    }

    private static Requirement[]? AddRequirement(Requirement[]? requirements, Requirement newRequirement, out int index)
    {
        List<Requirement> NewRequirements = new();

        if (requirements is not null)
            NewRequirements.AddRange(requirements);

        index = NewRequirements.Count;
        NewRequirements.Add(newRequirement);

        return NewRequirements.ToArray();
    }

    private int? ParseNumToDeliver(string? content)
    {
        if (content is null)
            return null;

        return int.Parse(content);
    }

    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public string? AbilityKeyword { get; set; }
    
    public string? AllowedFishingZone { get; set; }
    
    public string? AnatomyType { get; set; }
    
    public string? BehaviorId { get; set; }
    
    public string? Description { get; set; }
    
    public string? FishConfig { get; set; }
    
    public int? GroupId { get; set; }

    [Column(MapType = typeof(string))]
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
    
    public int? Number { get; set; }
    
    public int? NumberToDeliver { get; set; }

    [Navigate(nameof(Requirement.Key))]
    public Requirement[]? Requirements { get; set; }
    
    public string? ResultItemKeyword { get; set; }
    
    public string? Skill { get; set; }
    
    public string? StringParam { get; set; }
    
    public string? Target { get; set; }
    
    public string? Type { get; set; }
    
    public RawQuestObjective ToRawQuestObjective()
    {
        RawQuestObjective Result = new();

        if (EmptyAbilityKeyword)
            Result.AbilityKeyword = string.Empty;
        else
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
        Result.NumToDeliver = ToRawNumToDeliver(NumberToDeliver);
        Result.Number = Number;
        Result.Requirements = Preprocessor.FromSingleOrMultiple(Requirements, (Requirement requirement) => requirement.ToRawRequirement(), RequirementsFormat);
        Result.ResultItemKeyword = ResultItemKeyword;
        Result.Skill = Skill;
        Result.StringParam = StringParam;
        Result.Type = Type;

        string[]? Targets;
        if (Target is string RawTarget)
        {
            if (RawTarget == "Any")
                RawTarget = "*";

            if (RequirementTargetInArea is not null)
            {
                RawRequirement RawRequirementTargetInArea = RequirementTargetInArea.ToRawRequirement();
                string TargetArea = $"{AreaHeader}{RawRequirementTargetInArea.AreaEvent}";
                Targets = new string[] { RawTarget, TargetArea };

                Debug.Assert(Result.Requirements is not null);
                Result.Requirements = RemoveRequirement(Result.Requirements!, RequirementIndexTargetInArea);
            }
            else
                Targets = new string[] { RawTarget };
        }
        else
            Targets = null;
        Result.Target = Preprocessor.FromSingleOrMultiple(Targets, (string s) => s, TargetFormat);

        if (RequirementKillWithAbility is not null)
        {
            RawRequirement RawRequirementKillWithAbility = RequirementKillWithAbility.ToRawRequirement();
            Result.AbilityKeyword = RawRequirementKillWithAbility.AbilityKeyword;

            Debug.Assert(Result.Requirements is not null);
            Result.Requirements = RemoveRequirement(Result.Requirements!, RequirementIndexKillWithAbility);
        }

        return Result;
    }

    private static RawRequirement[]? RemoveRequirement(object requirements, int index)
    {
        List<RawRequirement> NewRequirements = new();

        RawRequirement[] ExistingRequirements = (RawRequirement[])requirements;
        NewRequirements.AddRange(ExistingRequirements);
        NewRequirements.RemoveAt(index);

        if (NewRequirements.Count > 0)
            return NewRequirements.ToArray();
        else
            return null;
    }

    private string? ToRawNumToDeliver(int? mumberToDeliver)
    {
        if (mumberToDeliver is null)
            return null;

        return mumberToDeliver.ToString();
    }

    private JsonArrayFormat RequirementsFormat;
    private JsonArrayFormat TargetFormat;
    private Requirement? RequirementKillWithAbility;
    private int RequirementIndexKillWithAbility;
    private Requirement? RequirementTargetInArea;
    private int RequirementIndexTargetInArea;
    private bool EmptyAbilityKeyword;
}
