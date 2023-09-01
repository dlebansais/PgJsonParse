namespace Preprocessor;

public class RawStorageVault
{
    public string? Area { get; set; }
    public StorageVaultEventLevel? EventLevels { get; set; }
    public string? Grouping { get; set; }
    public bool? HasAssociatedNpc { get; set; }
    public int? ID { get; set; }
    public StorageVaultLevel? Levels { get; set; }
    public string? NpcFriendlyName { get; set; }
    public int? NumSlots { get; set; }
    public string? NumSlotsScriptAtomic { get; set; }
    public int? NumSlotsScriptAtomicMaxValue { get; set; }
    public int? NumSlotsScriptAtomicMinValue { get; set; }
    public string[]? RequiredItemKeywords { get; set; }
    public string? RequirementDescription { get; set; }
    public object? Requirements { get; set; }
    public string? SlotAttribute { get; set; }
}
