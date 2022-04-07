namespace PgObjects
{
    using System.Collections.Generic;

    public class PgItem : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public PgRecipeCollection BestowRecipeList { get; set; } = new PgRecipeCollection();
        public string? BestowAbility_Key { get; set; }
        public string? BestowQuest_Key { get; set; }
        public bool AllowPrefix { get { return RawAllowPrefix.HasValue && RawAllowPrefix.Value; } }
        public bool? RawAllowPrefix { get; set; }
        public bool AllowSuffix { get { return RawAllowSuffix.HasValue && RawAllowSuffix.Value; } }
        public bool? RawAllowSuffix { get; set; }
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        public int? RawCraftPoints { get; set; }
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        public int? RawCraftingTargetLevel { get; set; }
        public string Description { get; set; } = string.Empty;
        public ItemDroppedAppearance DroppedAppearance { get; set; }
        public AppearanceSkin ItemAppearanceSkin { get; set; }
        public AppearanceSkin ItemAppearanceCork { get; set; }
        public AppearanceSkin ItemAppearanceFood { get; set; }
        public AppearanceSkin ItemAppearancePlate { get; set; }
        public uint ItemAppearanceColor { get { return RawItemAppearanceColor.HasValue ? RawItemAppearanceColor.Value : 0; } }
        public uint? RawItemAppearanceColor { get; set; }
        public PgItemEffectCollection EffectDescriptionList { get; set; } = new PgItemEffectCollection();
        public uint DyeColor { get { return RawDyeColor.HasValue ? RawDyeColor.Value : 0; } }
        public uint? RawDyeColor { get; set; }
        public string EquipAppearance { get; set; } = string.Empty;
        public ItemSlot EquipSlot { get; set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public string InternalName { get; set; } = string.Empty;
        public bool IsTemporary { get { return RawIsTemporary.HasValue && RawIsTemporary.Value; } }
        public bool? RawIsTemporary { get; set; }
        public bool IsCrafted { get { return RawIsCrafted.HasValue && RawIsCrafted.Value; } }
        public bool? RawIsCrafted { get; set; }
        public List<RecipeItemKey> RecipeItemKeyList { get; set; } = new List<RecipeItemKey>();
        public string? MacGuffinQuest_Key { get; set; }
        public int MaxCarryable { get { return RawMaxCarryable.HasValue ? RawMaxCarryable.Value : 0; } }
        public int? RawMaxCarryable { get; set; }
        public int MaxOnVendor { get { return RawMaxOnVendor.HasValue ? RawMaxOnVendor.Value : 0; } }
        public int? RawMaxOnVendor { get; set; }
        public int MaxStackSize { get { return RawMaxStackSize.HasValue ? RawMaxStackSize.Value : 0; } }
        public int? RawMaxStackSize { get; set; }
        public string Name { get; set; } = string.Empty;
        public Appearance RequiredAppearance { get; set; }
        public Dictionary<string, int> SkillRequirementTable { get; set; } = new Dictionary<string, int>();
        public PgItemDyeCollection StockDyeList { get; set; } = new PgItemDyeCollection();
        public bool HasGlow { get { return RawHasGlow.HasValue && RawHasGlow.Value; } }
        public bool? RawHasGlow { get; set; }
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
        public int NumUses { get { return RawNumUses.HasValue ? RawNumUses.Value : 0; } }
        public int? RawNumUses { get; set; }
        public bool DestroyWhenUsedUp { get { return RawDestroyWhenUsedUp.HasValue && RawDestroyWhenUsedUp.Value; } }
        public bool? RawDestroyWhenUsedUp { get; set; }
        public Profile TSysProfile { get; set; }
        public PgItemBehaviorCollection BehaviorList { get; set; } = new PgItemBehaviorCollection();
        public string DynamicCraftingSummary { get; set; } = string.Empty;
        public bool IsSkillReqsDefaults { get { return RawIsSkillReqsDefaults.HasValue && RawIsSkillReqsDefaults.Value; } }
        public bool? RawIsSkillReqsDefaults { get; set; }
        public string? BestowTitle_Key { get; set; }
        public string? BestowLoreBook_Key { get; set; }
        public WorkOrderSign LintVendorNpc { get; set; }
        public Dictionary<ItemKeyword, List<float>> KeywordTable { get; set; } = new Dictionary<ItemKeyword, List<float>>();
        public string MountedAppearance { get; set; } = string.Empty;

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name; } }
        public override string ToString() { return Name; }
    }
}
