using System.Collections.Generic;

namespace PgJsonObjects
{
    interface IPgItem
    {
        Ability BestowAbility { get; }
        Quest BestowQuest { get; }
        int CraftPoints { get; }
        int? RawCraftPoints { get; }
        int CraftingTargetLevel { get; }
        int? RawCraftingTargetLevel { get; }
        string Description { get; }
        ItemDroppedAppearance DroppedAppearance { get; }
        ItemSlot EquipSlot { get; }
        AppearanceSkin ItemAppearanceSkin { get; }
        AppearanceSkin ItemAppearanceCork { get; }
        AppearanceSkin ItemAppearanceFood { get; }
        AppearanceSkin ItemAppearancePlate { get; }
        uint ItemAppearanceColor { get; }
        uint? RawItemAppearanceColor { get; }
        List<ItemEffect> EffectDescriptionList { get; }
        uint DyeColor { get; }
        uint? RawDyeColor { get; }
        string EquipAppearance { get; }
        int IconId { get; }
        int? RawIconId { get; }
        string InternalName { get; }
        bool AllowPrefix { get; }
        bool? RawAllowPrefix { get; }
        bool AllowSuffix { get; }
        bool? RawAllowSuffix { get; }
        bool IsTemporary { get; }
        bool? RawIsTemporary { get; }
        bool IsCrafted { get; }
        bool? RawIsCrafted { get; }
        bool DestroyWhenUsedUp { get; }
        bool? RawDestroyWhenUsedUp { get; }
        bool IsSkillReqsDefaults { get; }
        bool? RawIsSkillReqsDefaults { get; }
        Appearance RequiredAppearance { get; }
        List<RecipeItemKey> ItemKeyList { get; }
        List<ItemKeyword> EmptyKeywordList { get; }
        List<ItemKeyword> RepeatedKeywordList { get; }
        Quest MacGuffinQuestName { get; }
        int MaxCarryable { get; }
        int? RawMaxCarryable { get; }
        int MaxOnVendor { get; }
        int? RawMaxOnVendor { get; }
        int MaxStackSize { get; }
        int? RawMaxStackSize { get; }
        string Name { get; }
        List<ItemSkillLink> SkillRequirementList { get; }
        List<uint> StockDye { get; }
        List<string> StockDyeByName { get; }
        double Value { get; }
        double? RawValue { get; }
        int NumUses { get; }
        int? RawNumUses { get; }
        List<ItemBehavior> BehaviorList { get; }
        string DynamicCraftingSummary { get; }
        int BestowTitle { get; }
        int? RawBestowTitle { get; }
        int BestowLoreBook { get; }
        int? RawBestowLoreBook { get; }
        LoreBook ConnectedLoreBook { get; }
    }
}
