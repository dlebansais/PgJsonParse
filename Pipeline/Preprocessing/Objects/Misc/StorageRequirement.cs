namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class StorageRequirement : IHasKey<int>, IHasParentKey<string>
{
    private const string AreaHeader = "Area";

    public StorageRequirement(RawRequirement rawRequirement)
    {
        AbilityKeyword = rawRequirement.AbilityKeyword;
        AccountFlag = rawRequirement.AccountFlag;
        AllowedRace = rawRequirement.AllowedRace;
        AllowedStates = rawRequirement.AllowedStates;
        Appearance = rawRequirement.Appearance;
        AreaEvent = rawRequirement.AreaEvent;
        AtomicVar = rawRequirement.AtomicVar;
        Attribute = rawRequirement.Attribute;
        ClearSky = rawRequirement.ClearSky;
        DaysAllowed = rawRequirement.DaysAllowed;
        DisallowedRace = rawRequirement.DisallowedRace;
        DisallowedStates = rawRequirement.DisallowedStates;
        Distance = rawRequirement.Distance;
        EntityTypeTag = rawRequirement.EntityTypeTag;
        ErrorMessage = rawRequirement.ErrorMsg;
        HangOut = rawRequirement.HangOut;
        InteractionFlag = rawRequirement.InteractionFlag;
        Item = rawRequirement.Item;
        Keyword = rawRequirement.Keyword;
        List = Preprocessor.ToSingleOrMultiple(rawRequirement.List, (RawRequirement rawRequirement) => new NestedRequirement(rawRequirement), out ListFormat);
        MaxCount = rawRequirement.MaxCount;
        MaxHour = rawRequirement.MaxHour;
        MaxTimesUsed = rawRequirement.MaxTimesUsed;
        MinCount = rawRequirement.MinCount;
        MinFavor = rawRequirement.MinFavor;
        MinHour = rawRequirement.MinHour;
        MinLevel = rawRequirement.MinLevel;
        MinimumMountsNeeded = rawRequirement.MinimumMountsNeeded;
        MoonPhase = rawRequirement.MoonPhase;
        Name = rawRequirement.Name;
        Npc = rawRequirement.Npc;
        PetTypeTag = rawRequirement.PetTypeTag;
        Quest = rawRequirement.Quest;
        Recipe = rawRequirement.Recipe;
        Rule = ParseRule(rawRequirement.Rule);
        ScriptAtomicInt = rawRequirement.ScriptAtomicInt;
        Shape = rawRequirement.Shape;
        Skill = rawRequirement.Skill;
        Slot = rawRequirement.Slot;
        T = rawRequirement.T;
        Value = rawRequirement.Value;

        (Level, FavorLevel) = Preprocessor.ParseAsNumberOrString(rawRequirement.Level);

        if (AreaEvent is not null)
            UpdateAreaEventRequirement(this, AreaEvent);
    }

    private static string? ParseRule(string? content)
    {
        if (content is null)
            return null;
        else if (content == "ChristmasQuests")
            return "During Christmas Quests";
        else
            return content;
    }

    private static void UpdateAreaEventRequirement(StorageRequirement requirement, string areaEvent)
    {
        if (areaEvent == "Daytime")
            UpdateAreaEventRequirementDayTime(requirement);
        else if (areaEvent == "PovusNightlyQuest")
            UpdateAreaEventRequirementPovusNightly(requirement);
        else
            UpdateAreaEventRequirementAreaOrQuest(requirement, areaEvent);
    }

    private static void UpdateAreaEventRequirementDayTime(StorageRequirement requirement)
    {
        requirement.AreaEvent = null;
        requirement.Daytime = true;
    }

    private static void UpdateAreaEventRequirementPovusNightly(StorageRequirement requirement)
    {
        requirement.AreaEvent = null;
        requirement.EventQuest = "PovusNightly";
    }

    private static void UpdateAreaEventRequirementAreaOrQuest(StorageRequirement requirement, string areaEvent)
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
            throw new PreprocessorException();
    }

    private static void UpdateAreaEventRequirementArea(StorageRequirement requirement, string areaEvent)
    {
        if (!areaEvent.StartsWith(AreaHeader))
            throw new PreprocessorException();

        requirement.AreaName = areaEvent.Substring(AreaHeader.Length);
        requirement.AreaEvent = null;
    }

    private static void UpdateAreaEventRequirementQuest(StorageRequirement requirement, string eventName, string? eventSkill, string eventQuest, string areaName)
    {
        requirement.AreaEvent = eventName;
        requirement.EventSkill = eventSkill;
        requirement.EventQuest = eventQuest;

        string? AreaName = Area.FromRawAreaName(areaName, out string? OriginalAreaName);
        requirement.AreaName = AreaName;
        requirement.OriginalAreaName = OriginalAreaName;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public string ParentKey { get; set; } = string.Empty;

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? AbilityKeyword { get; set; }

    public string? AccountFlag { get; set; }

    public string? AllowedRace { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AllowedStates { get; set; }

    public string? Appearance { get; set; }

    public string? AreaEvent { get; set; }

    public string? AreaName { get; set; }

    public string? AtomicVar { get; set; }

    public string? Attribute { get; set; }

    public bool? ClearSky { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? DaysAllowed { get; set; }

    public bool? Daytime { get; set; }
    
    public string? DisallowedRace { get; set; }

    [Column(MapType = typeof(string))]
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
    
    public NestedRequirement[]? List { get; set; }
    
    public int? MaxCount { get; set; }
    
    public int? MaxHour { get; set; }
    
    public int? MaxTimesUsed { get; set; }
    
    public int? MinCount { get; set; }
    
    public int? MinFavor { get; set; }

    public int? MinHour { get; set; }

    public int? MinLevel { get; set; }

    public int? MinimumMountsNeeded { get; set; }

    public string? MoonPhase { get; set; }
    
    public string? Name { get; set; }
    
    public string? Npc { get; set; }
    
    public string? PetTypeTag { get; set; }
    
    public string? Quest { get; set; }
    
    public string? Recipe { get; set; }
    
    public string? Rule { get; set; }
    
    public string? ScriptAtomicInt { get; set; }
    
    public string? Shape { get; set; }
    
    public string? Skill { get; set; }
    
    public string? Slot { get; set; }
    
    public string? T { get; set; }
    
    public string? Value { get; set; }

    public RawRequirement ToRawRequirement()
    {
        RawRequirement Result = new();

        Result.AbilityKeyword = AbilityKeyword;
        Result.AccountFlag = AccountFlag;
        Result.AllowedRace = AllowedRace;
        Result.AllowedStates = AllowedStates;
        Result.Appearance = Appearance;
        Result.AreaEvent = AreaEvent;
        Result.AtomicVar = AtomicVar;
        Result.Attribute = Attribute;
        Result.ClearSky = ClearSky;
        Result.DaysAllowed = DaysAllowed;
        Result.DisallowedRace = DisallowedRace;
        Result.DisallowedStates = DisallowedStates;
        Result.Distance = Distance;
        Result.EntityTypeTag = EntityTypeTag;
        Result.ErrorMsg = ErrorMessage;
        Result.HangOut = HangOut;
        Result.InteractionFlag = InteractionFlag;
        Result.Item = Item;
        Result.Keyword = Keyword;
        Result.List = Preprocessor.FromSingleOrMultiple(List, (NestedRequirement requirement) => requirement.ToRawRequirement(), ListFormat);
        Result.MaxCount = MaxCount;
        Result.MaxHour = MaxHour;
        Result.MaxTimesUsed = MaxTimesUsed;
        Result.MinCount = MinCount;
        Result.MinFavor = MinFavor;
        Result.MinHour = MinHour;
        Result.MinLevel = MinLevel;
        Result.MinimumMountsNeeded = MinimumMountsNeeded;
        Result.MoonPhase = MoonPhase;
        Result.Name = Name;
        Result.Npc = Npc;
        Result.PetTypeTag = PetTypeTag;
        Result.Quest = Quest;
        Result.Recipe = Recipe;
        Result.Rule = ToRawRule(Rule);
        Result.ScriptAtomicInt = ScriptAtomicInt;
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

        RestoreAreaEventRequirement(Result, Daytime, EventQuest, AreaName, OriginalAreaName, EventSkill);

        return Result;
    }

    private static string? ToRawRule(string? rule)
    {
        if (rule is null)
            return null;
        else if (rule == "During Christmas Quests")
            return "ChristmasQuests";
        else
            return rule;
    }

    private static void RestoreAreaEventRequirement(RawRequirement rawRequirement, bool? dayTime, string? eventQuest, string? areaName, string? originalAreaName, string? eventSkill)
    {
        if (dayTime is not null)
            rawRequirement.AreaEvent = "Daytime";
        else if (eventQuest == "PovusNightly")
            rawRequirement.AreaEvent = "PovusNightlyQuest";
        else if (areaName is not null)
        {
            if (eventQuest is not null)
            {
                string Skill = eventSkill is null ? string.Empty : $"_{eventSkill}";
                string? AreaSuffix = Area.ToRawAreaName(areaName, originalAreaName);

                rawRequirement.AreaEvent = $"{rawRequirement.AreaEvent}{Skill}_{eventQuest}_{AreaSuffix}";
            }
            else
                rawRequirement.AreaEvent = $"{AreaHeader}{areaName}";
        }
    }

    private JsonArrayFormat ListFormat;
    private string? OriginalAreaName;
}
