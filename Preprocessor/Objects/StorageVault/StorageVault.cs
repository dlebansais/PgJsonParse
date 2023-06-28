namespace Preprocessor;

internal class StorageVault
{
    public StorageVault(RawStorageVault rawStorageVault)
    {
        Area = rawStorageVault.Area;
        EventLevels = rawStorageVault.EventLevels;
        Grouping = rawStorageVault.Grouping;
        HasAssociatedNpc = rawStorageVault.HasAssociatedNpc;
        ID = rawStorageVault.ID;
        Levels = rawStorageVault.Levels;
        NpcFriendlyName = rawStorageVault.NpcFriendlyName;
        NumSlots = rawStorageVault.NumSlots;
        RequiredItemKeywords = rawStorageVault.RequiredItemKeywords;
        RequirementDescription = rawStorageVault.RequirementDescription;
        Requirements = Preprocessor.ToSingleOrMultiple<Requirement>(rawStorageVault.Requirements, out RequirementsFormat);
        SlotAttribute = rawStorageVault.SlotAttribute;
    }

    public string? Area { get; set; }
    public StorageVaultEventLevel? EventLevels { get; set; }
    public string? Grouping { get; set; }
    public bool? HasAssociatedNpc { get; set; }
    public int? ID { get; set; }
    public StorageVaultLevel? Levels { get; set; }
    public string? NpcFriendlyName { get; set; }
    public int? NumSlots { get; set; }
    public string[]? RequiredItemKeywords { get; set; }
    public string? RequirementDescription { get; set; }
    public Requirement[]? Requirements { get; set; }
    public string? SlotAttribute { get; set; }

    public RawStorageVault ToRawStorageVault()
    {
        RawStorageVault Result = new();

        Result.Area = Area;
        Result.EventLevels = EventLevels;
        Result.Grouping = Grouping;
        Result.HasAssociatedNpc = HasAssociatedNpc;
        Result.ID = ID;
        Result.Levels = Levels;
        Result.NpcFriendlyName = NpcFriendlyName;
        Result.NumSlots = NumSlots;
        Result.RequiredItemKeywords = RequiredItemKeywords;
        Result.RequirementDescription = RequirementDescription;
        Result.Requirements = Preprocessor.FromSingleOrMultiple(Requirements, RequirementsFormat);
        Result.SlotAttribute = SlotAttribute;

        return Result;
    }

    private JsonArrayFormat RequirementsFormat;
}
