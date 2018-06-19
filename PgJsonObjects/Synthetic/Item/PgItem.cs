using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItem : MainPgObject<PgItem>, IPgItem
    {
        public PgItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItem CreateNew(byte[] data, ref int offset)
        {
            return new PgItem(data, ref offset);
        }

        public IPgAbility BestowAbility { get { return GetObject(0, ref _BestowAbility, PgAbility.CreateNew); } } private IPgAbility _BestowAbility;
        public IPgQuest BestowQuest { get { return GetObject(4, ref _BestowQuest, PgQuest.CreateNew); } } private IPgQuest _BestowQuest;
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        public int? RawCraftPoints { get { return GetInt(8); } }
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        public int? RawCraftingTargetLevel { get { return GetInt(12); } }
        public string Description { get { return GetString(16); } }
        public ItemDroppedAppearance DroppedAppearance { get { return GetEnum<ItemDroppedAppearance>(20); } }
        public ItemSlot EquipSlot { get { return GetEnum<ItemSlot>(22); } }
        public AppearanceSkin ItemAppearanceSkin { get { return GetEnum<AppearanceSkin>(24); } }
        public AppearanceSkin ItemAppearanceCork { get { return GetEnum<AppearanceSkin>(26); } }
        public AppearanceSkin ItemAppearanceFood { get { return GetEnum<AppearanceSkin>(28); } }
        public AppearanceSkin ItemAppearancePlate { get { return GetEnum<AppearanceSkin>(30); } }
        public uint ItemAppearanceColor { get { return RawItemAppearanceColor.HasValue ? RawItemAppearanceColor.Value : 0; } }
        public uint? RawItemAppearanceColor { get { return GetUInt(32); } }
        public ItemEffectCollection EffectDescriptionList { get { return GetObjectList(36, ref _EffectDescriptionList, ItemEffectCollection.CreateItem, () => new ItemEffectCollection()); } } private ItemEffectCollection _EffectDescriptionList;
        public uint DyeColor { get { return RawDyeColor.HasValue ? RawDyeColor.Value : 0; } }
        public uint? RawDyeColor { get { return GetUInt(40); } }
        public string EquipAppearance { get { return GetString(44); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(48); } }
        public string InternalName { get { return GetString(52); } }
        public bool AllowPrefix { get { return RawAllowPrefix.HasValue && RawAllowPrefix.Value; } }
        public bool? RawAllowPrefix { get { return GetBool(56, 0); } }
        public bool AllowSuffix { get { return RawAllowSuffix.HasValue && RawAllowSuffix.Value; } }
        public bool? RawAllowSuffix { get { return GetBool(56, 2); } }
        public bool IsTemporary { get { return RawIsTemporary.HasValue && RawIsTemporary.Value; } }
        public bool? RawIsTemporary { get { return GetBool(56, 4); } }
        public bool IsCrafted { get { return RawIsCrafted.HasValue && RawIsCrafted.Value; } }
        public bool? RawIsCrafted { get { return GetBool(56, 6); } }
        public bool DestroyWhenUsedUp { get { return RawDestroyWhenUsedUp.HasValue && RawDestroyWhenUsedUp.Value; } }
        public bool? RawDestroyWhenUsedUp { get { return GetBool(56, 8); } }
        public bool IsSkillReqsDefaults { get { return RawIsSkillReqsDefaults.HasValue && RawIsSkillReqsDefaults.Value; } }
        public bool? RawIsSkillReqsDefaults { get { return GetBool(56, 10); } }
        public Appearance RequiredAppearance { get { return GetEnum<Appearance>(58); } }
        public List<RecipeItemKey> ItemKeyList { get { return GetEnumList(60, ref _ItemKeyList); } } private List<RecipeItemKey> _ItemKeyList;
        public List<ItemKeyword> EmptyKeywordList { get { return GetEnumList(64, ref _EmptyKeywordList); } } private List<ItemKeyword> _EmptyKeywordList;
        public List<ItemKeyword> RepeatedKeywordList { get { return GetEnumList(68, ref _RepeatedKeywordList); } } private List<ItemKeyword> _RepeatedKeywordList;
        public IPgQuest MacGuffinQuestName { get { return GetObject(72, ref _MacGuffinQuestName, PgQuest.CreateNew); } } private IPgQuest _MacGuffinQuestName;
        public int MaxCarryable { get { return RawMaxCarryable.HasValue ? RawMaxCarryable.Value : 0; } }
        public int? RawMaxCarryable { get { return GetInt(76); } }
        public int MaxOnVendor { get { return RawMaxOnVendor.HasValue ? RawMaxOnVendor.Value : 0; } }
        public int? RawMaxOnVendor { get { return GetInt(80); } }
        public int MaxStackSize { get { return RawMaxStackSize.HasValue ? RawMaxStackSize.Value : 0; } }
        public int? RawMaxStackSize { get { return GetInt(84); } }
        public string Name { get { return GetString(88); } }
        public ItemSkillLinkCollection SkillRequirementList { get { return GetObjectList(92, ref _SkillRequirementList, ItemSkillLinkCollection.CreateItem, () => new ItemSkillLinkCollection()); } } private ItemSkillLinkCollection _SkillRequirementList;
        public List<uint> StockDye { get { return GetUIntList(96, ref _StockDye); } } private List<uint> _StockDye;
        public List<string> StockDyeByName { get { return GetStringList(100, ref _StockDyeByName); } } private List<string> _StockDyeByName;
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get { return GetDouble(104); } }
        public int NumUses { get { return RawNumUses.HasValue ? RawNumUses.Value : 0; } }
        public int? RawNumUses { get { return GetInt(108); } }
        public ItemBehaviorCollection BehaviorList { get { return GetObjectList(112, ref _BehaviorList, ItemBehaviorCollection.CreateItem, () => new ItemBehaviorCollection()); } } private ItemBehaviorCollection _BehaviorList;
        public string DynamicCraftingSummary { get { return GetString(116); } }
        public int BestowTitle { get { return RawBestowTitle.HasValue ? RawBestowTitle.Value : 0; } }
        public int? RawBestowTitle { get { return GetInt(120); } }
        public int BestowLoreBook { get { return RawBestowLoreBook.HasValue ? RawBestowLoreBook.Value : 0; } }
        public int? RawBestowLoreBook { get { return GetInt(124); } }
        public IPgLoreBook ConnectedLoreBook { get { return GetObject(128, ref _ConnectedLoreBook, PgLoreBook.CreateNew); } } private IPgLoreBook _ConnectedLoreBook;
    }
}
