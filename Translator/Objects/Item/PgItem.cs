namespace PgObjects
{
    using System.Collections.Generic;

    public class PgItem : PgObject
    {
        public PgRecipeCollection BestowRecipeList { get; set; } = new PgRecipeCollection();
        public int BoolValues { get; set; }
        public string? BestowAbility_Key { get; set; }
        public string? BestowQuest_Key { get; set; }
        public const int AllowPrefixNotNull = 1 << 0;
        public const int AllowPrefixIsTrue = 1 << 1;
        public bool AllowPrefix { get { return (BoolValues & (AllowPrefixNotNull + AllowPrefixIsTrue)) == (AllowPrefixNotNull + AllowPrefixIsTrue); } }
        public bool? RawAllowPrefix { get { return ((BoolValues & AllowPrefixNotNull) != 0) ? (BoolValues & AllowPrefixIsTrue) != 0 : null; } }
        public void SetAllowPrefix(bool value) { BoolValues |= (BoolValues & ~(AllowPrefixNotNull + AllowPrefixIsTrue)) | ((value ? AllowPrefixIsTrue : 0) + AllowPrefixNotNull); }
        public const int AllowSuffixNotNull = 1 << 2;
        public const int AllowSuffixIsTrue = 1 << 3;
        public bool AllowSuffix { get { return (BoolValues & (AllowSuffixNotNull + AllowSuffixIsTrue)) == (AllowSuffixNotNull + AllowSuffixIsTrue); } }
        public bool? RawAllowSuffix { get { return ((BoolValues & AllowSuffixNotNull) != 0) ? (BoolValues & AllowSuffixIsTrue) != 0 : null; } }
        public void SetAllowSuffix(bool value) { BoolValues |= (BoolValues & ~(AllowSuffixNotNull + AllowSuffixIsTrue)) | ((value ? AllowSuffixIsTrue : 0) + AllowSuffixNotNull); }
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        public int? RawCraftPoints { get; set; }
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        public int? RawCraftingTargetLevel { get; set; }
        public string Description { get; set; } = string.Empty;
        public PgDroppedAppearance DroppedAppearance { get; set; } = null!;
        public PgItemEffectCollection EffectDescriptionList { get; set; } = new PgItemEffectCollection();
        public uint DyeColor { get { return RawDyeColor.HasValue ? RawDyeColor.Value : 0; } }
        public uint? RawDyeColor { get; set; }
        public string EquipAppearance { get; set; } = string.Empty;
        public ItemSlot EquipSlot { get; set; }
        public string FoodDescription { get; set; } = string.Empty;
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; set; }
        public string InternalName { get; set; } = string.Empty;
        public const int IsTemporaryNotNull = 1 << 4;
        public const int IsTemporaryIsTrue = 1 << 5;
        public bool IsTemporary { get { return (BoolValues & (IsTemporaryNotNull + IsTemporaryIsTrue)) == (IsTemporaryNotNull + IsTemporaryIsTrue); } }
        public bool? RawIsTemporary { get { return ((BoolValues & IsTemporaryNotNull) != 0) ? (BoolValues & IsTemporaryIsTrue) != 0 : null; } }
        public void SetIsTemporary(bool value) { BoolValues |= (BoolValues & ~(IsTemporaryNotNull + IsTemporaryIsTrue)) | ((value ? IsTemporaryIsTrue : 0) + IsTemporaryNotNull); }
        public const int IsCraftedNotNull = 1 << 6;
        public const int IsCraftedIsTrue = 1 << 7;
        public bool IsCrafted { get { return (BoolValues & (IsCraftedNotNull + IsCraftedIsTrue)) == (IsCraftedNotNull + IsCraftedIsTrue); } }
        public bool? RawIsCrafted { get { return ((BoolValues & IsCraftedNotNull) != 0) ? (BoolValues & IsCraftedIsTrue) != 0 : null; } }
        public void SetIsCrafted(bool value) { BoolValues |= (BoolValues & ~(IsCraftedNotNull + IsCraftedIsTrue)) | ((value ? IsCraftedIsTrue : 0) + IsCraftedNotNull); }
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
        public PgStockDye StockDye { get; set; } = null!;
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
        public int NumberOfUses { get { return RawNumberOfUses.HasValue ? RawNumberOfUses.Value : 0; } }
        public int? RawNumberOfUses { get; set; }
        public const int DestroyWhenUsedUpNotNull = 1 << 10;
        public const int DestroyWhenUsedUpIsTrue = 1 << 11;
        public bool DestroyWhenUsedUp { get { return (BoolValues & (DestroyWhenUsedUpNotNull + DestroyWhenUsedUpIsTrue)) == (DestroyWhenUsedUpNotNull + DestroyWhenUsedUpIsTrue); } }
        public bool? RawDestroyWhenUsedUp { get { return ((BoolValues & DestroyWhenUsedUpNotNull) != 0) ? (BoolValues & DestroyWhenUsedUpIsTrue) != 0 : null; } }
        public void SetDestroyWhenUsedUp(bool value) { BoolValues |= (BoolValues & ~(DestroyWhenUsedUpNotNull + DestroyWhenUsedUpIsTrue)) | ((value ? DestroyWhenUsedUpIsTrue : 0) + DestroyWhenUsedUpNotNull); }
        public string? Profile_Key { get; set; }
        public PgItemBehaviorCollection BehaviorList { get; set; } = new PgItemBehaviorCollection();
        public string DynamicCraftingSummary { get; set; } = string.Empty;
        public const int IsSkillReqsDefaultsNotNull = 1 << 12;
        public const int IsSkillReqsDefaultsIsTrue = 1 << 13;
        public bool IsSkillReqsDefaults { get { return (BoolValues & (IsSkillReqsDefaultsNotNull + IsSkillReqsDefaultsIsTrue)) == (IsSkillReqsDefaultsNotNull + IsSkillReqsDefaultsIsTrue); } }
        public bool? RawIsSkillReqsDefaults { get { return ((BoolValues & IsSkillReqsDefaultsNotNull) != 0) ? (BoolValues & IsSkillReqsDefaultsIsTrue) != 0 : null; } }
        public void SetIsSkillReqsDefaults(bool value) { BoolValues |= (BoolValues & ~(IsSkillReqsDefaultsNotNull + IsSkillReqsDefaultsIsTrue)) | ((value ? IsSkillReqsDefaultsIsTrue : 0) + IsSkillReqsDefaultsNotNull); }
        public string? BestowTitle_Key { get; set; }
        public string? BestowLoreBook_Key { get; set; }
        public WorkOrderSign LintVendorNpc { get; set; }
        public List<PgItemKeywordValues> KeywordValuesList { get; set; } = new();
        public string MountedAppearance { get; set; } = string.Empty;
        public const int AttuneOnPickupNotNull = 1 << 14;
        public const int AttuneOnPickupIsTrue = 1 << 15;
        public bool AttuneOnPickup { get { return (BoolValues & (AttuneOnPickupNotNull + AttuneOnPickupIsTrue)) == (AttuneOnPickupNotNull + AttuneOnPickupIsTrue); } }
        public bool? RawAttuneOnPickup { get { return ((BoolValues & AttuneOnPickupNotNull) != 0) ? (BoolValues & AttuneOnPickupIsTrue) != 0 : null; } }
        public void SetAttuneOnPickup(bool value) { BoolValues |= (BoolValues & ~(AttuneOnPickupNotNull + AttuneOnPickupIsTrue)) | ((value ? AttuneOnPickupIsTrue : 0) + AttuneOnPickupNotNull); }
        public const int IgnoreAlreadyKnownBestowalsNotNull = 1 << 16;
        public const int IgnoreAlreadyKnownBestowalsIsTrue = 1 << 17;
        public bool IgnoreAlreadyKnownBestowals { get { return (BoolValues & (IgnoreAlreadyKnownBestowalsNotNull + IgnoreAlreadyKnownBestowalsIsTrue)) == (IgnoreAlreadyKnownBestowalsNotNull + IgnoreAlreadyKnownBestowalsIsTrue); } }
        public bool? RawIgnoreAlreadyKnownBestowals { get { return ((BoolValues & IgnoreAlreadyKnownBestowalsNotNull) != 0) ? (BoolValues & IgnoreAlreadyKnownBestowalsIsTrue) != 0 : null; } }
        public void SetIgnoreAlreadyKnownBestowals(bool value) { BoolValues |= (BoolValues & ~(IgnoreAlreadyKnownBestowalsNotNull + IgnoreAlreadyKnownBestowalsIsTrue)) | ((value ? IgnoreAlreadyKnownBestowalsIsTrue : 0) + IgnoreAlreadyKnownBestowalsNotNull); }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name; } }
        public override string ToString() { return Name; }
    }
}
