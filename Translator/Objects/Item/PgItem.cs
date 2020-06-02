namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgItem
    {
        public PgAbility BestowAbility { get; set; }
        public PgQuest BestowQuest { get; set; }
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        public int? RawCraftPoints { get; set; }
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        public int? RawCraftingTargetLevel { get; set; }
        public string Description { get; set; } = string.Empty;
        public ItemDroppedAppearance DroppedAppearance { get; set; }
        public ItemSlot EquipSlot { get; set; }
        public AppearanceSkin ItemAppearanceSkin { get; set; }
        public AppearanceSkin ItemAppearanceCork { get; set; }
        public AppearanceSkin ItemAppearanceFood { get; set; }
        public AppearanceSkin ItemAppearancePlate { get; set; }
        public uint ItemAppearanceColor { get { return RawItemAppearanceColor.HasValue ? RawItemAppearanceColor.Value : 0; } }
        public uint? RawItemAppearanceColor { get; set; }
        public PgItemEffectCollection EffectDescriptionList { get; } = new PgItemEffectCollection();
        public uint DyeColor { get { return RawDyeColor.HasValue ? RawDyeColor.Value : 0; } }
        public uint? RawDyeColor { get; set; }
        public string EquipAppearance { get; set; } = string.Empty;
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public string InternalName { get; set; } = string.Empty;
        public bool AllowPrefix { get { return RawAllowPrefix.HasValue && RawAllowPrefix.Value; } }
        public bool? RawAllowPrefix { get; set; }
        public bool AllowSuffix { get { return RawAllowSuffix.HasValue && RawAllowSuffix.Value; } }
        public bool? RawAllowSuffix { get; set; }
        public bool IsTemporary { get { return RawIsTemporary.HasValue && RawIsTemporary.Value; } }
        public bool? RawIsTemporary { get; set; }
        public bool IsCrafted { get { return RawIsCrafted.HasValue && RawIsCrafted.Value; } }
        public bool? RawIsCrafted { get; set; }
        public bool DestroyWhenUsedUp { get { return RawDestroyWhenUsedUp.HasValue && RawDestroyWhenUsedUp.Value; } }
        public bool? RawDestroyWhenUsedUp { get; set; }
        public bool IsSkillReqsDefaults { get { return RawIsSkillReqsDefaults.HasValue && RawIsSkillReqsDefaults.Value; } }
        public bool? RawIsSkillReqsDefaults { get; set; }
        public bool IsEffectDescriptionEmpty { get { return RawIsEffectDescriptionEmpty.HasValue && RawIsEffectDescriptionEmpty.Value; } }
        public bool? RawIsEffectDescriptionEmpty { get; set; }
        public List<RecipeItemKey> ItemKeyList { get; } = new List<RecipeItemKey>();
        public List<ItemKeyword> EmptyKeywordList { get; } = new List<ItemKeyword>();
        public List<ItemKeyword> RepeatedKeywordList { get; } = new List<ItemKeyword>();
        public PgQuest MacGuffinQuestName { get; set; }
        public int MaxCarryable { get { return RawMaxCarryable.HasValue ? RawMaxCarryable.Value : 0; } }
        public int? RawMaxCarryable { get; set; }
        public int MaxOnVendor { get { return RawMaxOnVendor.HasValue ? RawMaxOnVendor.Value : 0; } }
        public int? RawMaxOnVendor { get; set; }
        public int MaxStackSize { get { return RawMaxStackSize.HasValue ? RawMaxStackSize.Value : 0; } }
        public int? RawMaxStackSize { get; set; }
        public string Name { get; set; } = string.Empty;
        public PgItemSkillLinkCollection SkillRequirementList { get; } = new PgItemSkillLinkCollection();
        public List<uint> StockDye { get; set; } = new List<uint>();
        public List<string> StockDyeByName { get; set; } = new List<string>();
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
        public int NumUses { get { return RawNumUses.HasValue ? RawNumUses.Value : 0; } }
        public int? RawNumUses { get; set; }
        public PgItemBehaviorCollection BehaviorList { get; } = new PgItemBehaviorCollection();
        public string DynamicCraftingSummary { get; set; } = string.Empty;
        public PgPlayerTitle BestowTitle { get; set; }
        public PgLoreBook ConnectedLoreBook { get; set; }
        public Appearance RequiredAppearance { get; set; }
        public List<string> KeywordValueList { get; } = new List<string>();
        public PgRecipeCollection BestowRecipeList { get; set; } = null;
        public List<string> AppearanceDetailList { get; set; } = new List<string>();
        public List<string> RawKeywordList { get; set; } = new List<string>();
        public int UnknownSkillReqIndex { get { return RawUnknownSkillReqIndex.HasValue ? RawUnknownSkillReqIndex.Value : 0; } }
        public int? RawUnknownSkillReqIndex { get; set; }
        public WorkOrderSign LintVendorNpc { get; set; }
    }
}
