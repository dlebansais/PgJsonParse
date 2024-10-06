namespace Preprocessor;

public class StorageVault
{
    private const string AreaHeader = "Area";

    public StorageVault(RawStorageVault rawStorageVault)
    {
        StorageArea = ParseArea(rawStorageVault.Area);
        EventLevels = rawStorageVault.EventLevels;
        Grouping = ParseArea(rawStorageVault.Grouping);
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

    private static AreaDetail ParseArea(string? rawArea)
    {
        AreaDetail Result;

        if (rawArea is not null)
        {
            if (rawArea == "*")
            {
                Result = new() { AreaType = (int)AreaType.Any };
            }
            else if (rawArea == "Apartment")
            {
                Result = new() { AreaType = (int)AreaType.Apartment };
            }
            else if (rawArea.StartsWith(AreaHeader))
            {
                string AreaName = rawArea.Substring(AreaHeader.Length);
                AreaName = Area.FromRawAreaName(AreaName, out string? OriginalAreaName)!;

                if (OriginalAreaName is not null)
                    throw new PreprocessorException();

                Result = new() { AreaType = (int)AreaType.Normal, AreaName = AreaName };
            }
            else
                throw new PreprocessorException();
        }
        else
            Result = new();

        return Result;
    }

    public StorageVaultEventLevel? EventLevels { get; set; }
    public AreaDetail Grouping { get; set; }
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
    public AreaDetail StorageArea { get; set; }

    public RawStorageVault ToRawStorageVault()
    {
        RawStorageVault Result = new();

        Result.Area = ToRawArea(StorageArea);
        Result.EventLevels = EventLevels;
        Result.Grouping = ToRawArea(Grouping);
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

    private static string? ToRawArea(AreaDetail areaDetail)
    {
        return (AreaType)areaDetail.AreaType switch
        {
            AreaType.Apartment => "Apartment",
            AreaType.Any => "*",
            AreaType.Normal => areaDetail.AreaName is not null ? $"{AreaHeader}{Area.ToRawAreaName(areaDetail.AreaName, null)}" : null,
            _ => null,
        };
    }

    private JsonArrayFormat RequirementsFormat;
}
