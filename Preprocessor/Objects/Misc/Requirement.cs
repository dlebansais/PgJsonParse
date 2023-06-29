namespace Preprocessor;

using System;
using System.Diagnostics;

internal class Requirement
{
    private const string AreaHeader = "Area";

    public Requirement(RawRequirement rawRequirement)
    {
        AbilityKeyword = rawRequirement.AbilityKeyword;
        AllowedRace = rawRequirement.AllowedRace;
        AllowedStates = rawRequirement.AllowedStates;
        Appearance = rawRequirement.Appearance;
        AreaEvent = rawRequirement.AreaEvent;
        AtomicVar = rawRequirement.AtomicVar;
        ClearSky = rawRequirement.ClearSky;
        DisallowedRace = rawRequirement.DisallowedRace;
        DisallowedStates = rawRequirement.DisallowedStates;
        Distance = rawRequirement.Distance;
        EntityTypeTag = rawRequirement.EntityTypeTag;
        ErrorMessage = rawRequirement.ErrorMsg;
        HangOut = rawRequirement.HangOut;
        InteractionFlag = rawRequirement.InteractionFlag;
        Item = rawRequirement.Item;
        Keyword = rawRequirement.Keyword;
        List = Preprocessor.ToSingleOrMultiple(rawRequirement.List, (RawRequirement rawRequirement) => new Requirement(rawRequirement), out ListFormat);
        MaxCount = rawRequirement.MaxCount;
        MaxHour = rawRequirement.MaxHour;
        MaxTimesUsed = rawRequirement.MaxTimesUsed;
        MinCount = rawRequirement.MinCount;
        MinFavor = rawRequirement.MinFavor;
        MinHour = rawRequirement.MinHour;
        MoonPhase = rawRequirement.MoonPhase;
        Name = rawRequirement.Name;
        Npc = rawRequirement.Npc;
        PetTypeTag = rawRequirement.PetTypeTag;
        Quest = rawRequirement.Quest;
        Recipe = rawRequirement.Recipe;
        Rule = rawRequirement.Rule;
        Shape = rawRequirement.Shape;
        Skill = rawRequirement.Skill;
        Slot = rawRequirement.Slot;
        T = rawRequirement.T;
        Value = rawRequirement.Value;

        (Level, FavorLevel) = Preprocessor.ParseAsNumberOrString(rawRequirement.Level);

        if (AreaEvent is not null)
            UpdateAreaEventRequirement(this, AreaEvent);
    }

    private static void UpdateAreaEventRequirement(Requirement requirement, string areaEvent)
    {
        if (areaEvent == "Daytime")
            UpdateAreaEventRequirementDayTime(requirement);
        else if (areaEvent == "PovusNightlyQuest")
            UpdateAreaEventRequirementPovusNightly(requirement);
        else
            UpdateAreaEventRequirementAreaOrQuest(requirement, areaEvent);
    }

    private static void UpdateAreaEventRequirementDayTime(Requirement requirement)
    {
        requirement.AreaEvent = null;
        requirement.Daytime = true;
    }

    private static void UpdateAreaEventRequirementPovusNightly(Requirement requirement)
    {
        requirement.AreaEvent = null;
        requirement.EventQuest = "PovusNightly";
    }

    private static void UpdateAreaEventRequirementAreaOrQuest(Requirement requirement, string areaEvent)
    {
        string[] AreaSplitted = areaEvent.Split('_');
        if (AreaSplitted.Length == 1)
            UpdateAreaEventRequirementArea(requirement, areaEvent);
        else if (AreaSplitted.Length >= 3 && AreaSplitted.Length <= 4)
        {
            int i = 0;

            string EventName = AreaSplitted[i++];
            string? EventSkill = (AreaSplitted.Length == 4) ? AreaSplitted[i++] : null;
            string EventQuest = AreaSplitted[i++];
            string AreaName = AreaSplitted[i++];

            UpdateAreaEventRequirementQuest(requirement, EventName, EventSkill, EventQuest, AreaName);
        }
        else
            throw new InvalidCastException();
    }

    private static void UpdateAreaEventRequirementArea(Requirement requirement, string areaEvent)
    {
        if (!areaEvent.StartsWith(AreaHeader))
            throw new InvalidCastException();

        requirement.AreaName = areaEvent.Substring(AreaHeader.Length);
        requirement.AreaEvent = null;
    }

    private static void UpdateAreaEventRequirementQuest(Requirement requirement, string eventName, string? eventSkill, string eventQuest, string areaName)
    {
        requirement.AreaEvent = eventName;
        requirement.EventSkill = eventSkill;
        requirement.EventQuest = eventQuest;

        if (areaName == "Ilmari")
            requirement.AreaName = "Desert1";
        else if (areaName == "Kur")
            requirement.AreaName = "KurMountains";
        else
            requirement.AreaName = areaName;
    }

    public string? AbilityKeyword { get; set; }
    public string? AllowedRace { get; set; }
    public string[]? AllowedStates { get; set; }
    public string? Appearance { get; set; }
    public string? AreaEvent { get; set; }
    public string? AreaName { get; set; }
    public string? AtomicVar { get; set; }
    public bool? ClearSky { get; set; }
    public bool? Daytime { get; set; }
    public string? DisallowedRace { get; set; }
    public string[]? DisallowedStates { get; set; }
    public int? Distance { get; set; }
    public string? EntityTypeTag { get; set; }
    public string? ErrorMessage { get; set; }
    public string? EventQuest { get; set; }
    public string? EventSkill { get; set; }
    public string? FavorLevel { get; set; }
    public string? HangOut { get; set; }
    public string? InteractionFlag { get; set; }
    public string? Item { get; set; }
    public string? Keyword { get; set; }
    public int? Level { get; set; }
    public Requirement[]? List { get; set; }
    public int? MaxCount { get; set; }
    public int? MaxHour { get; set; }
    public int? MaxTimesUsed { get; set; }
    public int? MinCount { get; set; }
    public int? MinFavor { get; set; }
    public int? MinHour { get; set; }
    public string? MoonPhase { get; set; }
    public string? Name { get; set; }
    public string? Npc { get; set; }
    public string? PetTypeTag { get; set; }
    public string? Quest { get; set; }
    public string? Recipe { get; set; }
    public string? Rule { get; set; }
    public string? Shape { get; set; }
    public string? Skill { get; set; }
    public string? Slot { get; set; }
    public string? T { get; set; }
    public string? Value { get; set; }

    public RawRequirement ToRawRequirement()
    {
        RawRequirement Result = new();

        Result.AbilityKeyword = AbilityKeyword;
        Result.AllowedRace = AllowedRace;
        Result.AllowedStates = AllowedStates;
        Result.Appearance = Appearance;
        Result.AreaEvent = AreaEvent;
        Result.AtomicVar = AtomicVar;
        Result.ClearSky = ClearSky;
        Result.DisallowedRace = DisallowedRace;
        Result.DisallowedStates = DisallowedStates;
        Result.Distance = Distance;
        Result.EntityTypeTag = EntityTypeTag;
        Result.ErrorMsg = ErrorMessage;
        Result.HangOut = HangOut;
        Result.InteractionFlag = InteractionFlag;
        Result.Item = Item;
        Result.Keyword = Keyword;
        Result.List = Preprocessor.FromSingleOrMultiple(List, (Requirement requirement) => requirement.ToRawRequirement(), ListFormat);
        Result.MaxCount = MaxCount;
        Result.MaxHour = MaxHour;
        Result.MaxTimesUsed = MaxTimesUsed;
        Result.MinCount = MinCount;
        Result.MinFavor = MinFavor;
        Result.MinHour = MinHour;
        Result.MoonPhase = MoonPhase;
        Result.Name = Name;
        Result.Npc = Npc;
        Result.PetTypeTag = PetTypeTag;
        Result.Quest = Quest;
        Result.Recipe = Recipe;
        Result.Rule = Rule;
        Result.Shape = Shape;
        Result.Skill = Skill;
        Result.Slot = Slot;
        Result.T = T;
        Result.Value = Value;

        if (FavorLevel is not null)
            Result.Level = FavorLevel;
        else if (Level is not null)
            Result.Level = Level;
        else
            Result.Level = null;

        RestoreAreaEventRequirement(Result, Daytime, EventQuest, AreaName, EventSkill);

        return Result;
    }

    private static void RestoreAreaEventRequirement(RawRequirement requirement, bool? dayTime, string? eventQuest, string? areaName, string? eventSkill)
    {
        if (dayTime is not null)
            requirement.AreaEvent = "Daytime";
        else if (eventQuest == "PovusNightly")
            requirement.AreaEvent = "PovusNightlyQuest";
        else if (areaName is not null)
        {
            if (eventQuest is not null)
            {
                string Skill = eventSkill is null ? string.Empty : $"_{eventSkill}";

                string AreaSuffix;
                if (areaName == "Desert1")
                    AreaSuffix = "Ilmari";
                else if (areaName == "KurMountains")
                    AreaSuffix = "Kur";
                else
                    AreaSuffix = areaName;

                requirement.AreaEvent = $"{requirement.AreaEvent}{Skill}_{eventQuest}_{AreaSuffix}";
            }
            else
                requirement.AreaEvent = $"{AreaHeader}{areaName}";
        }
    }

    private JsonArrayFormat ListFormat;
}
