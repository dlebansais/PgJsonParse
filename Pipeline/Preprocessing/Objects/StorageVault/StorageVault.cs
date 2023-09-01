namespace Preprocessor;

public class StorageVault
{
    private const string AreaHeader = "Area";

    public StorageVault(RawStorageVault rawStorageVault)
    {
        (IsAnyArea, AreaName, OriginalAreaName) = ParseArea(rawStorageVault.Area);
        EventLevels = rawStorageVault.EventLevels;
        (_, Grouping, _) = ParseArea(rawStorageVault.Grouping);
        HasAssociatedNpc = rawStorageVault.HasAssociatedNpc;
        ID = rawStorageVault.ID;
        Levels = rawStorageVault.Levels;
        NpcFriendlyName = rawStorageVault.NpcFriendlyName;
        NumberOfSlots = rawStorageVault.NumSlots;
        NumberOfSlotsScriptAtomic = rawStorageVault.NumSlotsScriptAtomic;
        NumberOfSlotsScriptAtomicMaxValue = rawStorageVault.NumSlotsScriptAtomicMaxValue;
        NumberOfSlotsScriptAtomicMinValue = rawStorageVault.NumSlotsScriptAtomicMinValue;
        RequiredItemKeywords = rawStorageVault.RequiredItemKeywords;
        RequirementDescription = rawStorageVault.RequirementDescription;
        Requirements = Preprocessor.ToSingleOrMultiple(rawStorageVault.Requirements, (RawRequirement rawRequirement) => new Requirement(rawRequirement), out RequirementsFormat);
        SlotAttribute = rawStorageVault.SlotAttribute;
    }

    private static (bool, string?, string?) ParseArea(string? rawArea)
    {
        (bool, string ?, string ?) Result = (false, null, null);

        if (rawArea is not null)
        {
            if (rawArea == "*")
            {
                Result = (true, null, null);
            }
            else if (rawArea.StartsWith(AreaHeader))
            {
                string AreaName = rawArea.Substring(AreaHeader.Length);
                AreaName = Area.FromRawAreaName(AreaName, out string? OriginalAreaName)!;

                Result = (false, AreaName, OriginalAreaName);
            }
            else
                throw new PreprocessorException();
        }

        return Result;
    }

    public string? AreaName { get; set; }
    public StorageVaultEventLevel? EventLevels { get; set; }
    public string? Grouping { get; set; }
    public bool? HasAssociatedNpc { get; set; }
    public int? ID { get; set; }
    public StorageVaultLevel? Levels { get; set; }
    public string? NpcFriendlyName { get; set; }
    public int? NumberOfSlots { get; set; }
    public string? NumberOfSlotsScriptAtomic { get; set; }
    public int? NumberOfSlotsScriptAtomicMaxValue { get; set; }
    public int? NumberOfSlotsScriptAtomicMinValue { get; set; }
    public string[]? RequiredItemKeywords { get; set; }
    public string? RequirementDescription { get; set; }
    public Requirement[]? Requirements { get; set; }
    public string? SlotAttribute { get; set; }

    public RawStorageVault ToRawStorageVault()
    {
        RawStorageVault Result = new();

        Result.Area = ToRawArea(IsAnyArea, AreaName, OriginalAreaName);
        Result.EventLevels = EventLevels;
        Result.Grouping = ToRawArea(isAnyArea: false, Grouping, originalAreaName: null);
        Result.HasAssociatedNpc = HasAssociatedNpc;
        Result.ID = ID;
        Result.Levels = Levels;
        Result.NpcFriendlyName = NpcFriendlyName;
        Result.NumSlots = NumberOfSlots;
        Result.NumSlotsScriptAtomic = NumberOfSlotsScriptAtomic;
        Result.NumSlotsScriptAtomicMaxValue = NumberOfSlotsScriptAtomicMaxValue;
        Result.NumSlotsScriptAtomicMinValue = NumberOfSlotsScriptAtomicMinValue;
        Result.RequiredItemKeywords = RequiredItemKeywords;
        Result.RequirementDescription = RequirementDescription;
        Result.Requirements = Preprocessor.FromSingleOrMultiple(Requirements, (Requirement requirement) => requirement.ToRawRequirement(), RequirementsFormat);
        Result.SlotAttribute = SlotAttribute;

        return Result;
    }

    private static string? ToRawArea(bool isAnyArea, string? areaName, string? originalAreaName)
    {
        if (isAnyArea)
            return "*";
        else if (areaName is not null)
            return $"{AreaHeader}{Area.ToRawAreaName(areaName, originalAreaName)}";
        else
            return null;
    }

    private JsonArrayFormat RequirementsFormat;
    private bool IsAnyArea;
    private string? OriginalAreaName;
}
