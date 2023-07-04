namespace Preprocessor;

internal class RawItem
{
    public bool? AllowPrefix { get; set; }
    public bool? AllowSuffix { get; set; }
    public bool? AttuneOnPickup { get; set; }
    public object? Behaviors { get; set; }
    public string? BestowAbility { get; set; }
    public int? BestowLoreBook { get; set; }
    public string? BestowQuest { get; set; }
    public string[]? BestowRecipes { get; set; }
    public int? BestowTitle { get; set; }
    public int? CraftPoints { get; set; }
    public int? CraftingTargetLevel { get; set; }
    public string? Description { get; set; }
    public bool? DestroyWhenUsedUp { get; set; }
    public string? DroppedAppearance { get; set; }
    public string? DyeColor { get; set; }
    public string? DynamicCraftingSummary { get; set; }
    public string[]? EffectDescs { get; set; }
    public string? EquipAppearance { get; set; }
    public string? EquipSlot { get; set; }
    public string? FoodDesc { get; set; }
    public int IconId { get; set; }
    public string? InternalName { get; set; }
    public bool? IsCrafted { get; set; }
    public bool? IsSkillReqsDefaults { get; set; }
    public bool? IsTemporary { get; set; }
    public string[]? Keywords { get; set; }
    public string? Lint_VendorNpc { get; set; }
    public string? MacGuffinQuestName { get; set; }
    public int? MaxCarryable { get; set; }
    public int? MaxOnVendor { get; set; }
    public int? MaxStackSize { get; set; }
    public string? MountedAppearance { get; set; }
    public string? Name { get; set; }
    public int? NumUses { get; set; }
    public string? RequiredAppearance { get; set; }
    public SkillRequirementDictionary? SkillReqs { get; set; }
    public string? StockDye { get; set; }
    public string? TSysProfile { get; set; }
    public decimal? Value { get; set; }
}
