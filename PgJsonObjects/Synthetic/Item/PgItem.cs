using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItem : MainPgObject<PgItem>, IPgItem
    {
        public PgItem(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 140;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItem CreateNew(byte[] data, ref int offset)
        {
            PgItem Result = new PgItem(data, ref offset);
            ItemBehaviorCollection BehaviorList = Result.BehaviorList;
            return Result;
        }

        public override string Key { get { return GetString(0); } }
        public IPgAbility BestowAbility { get { return GetObject(4, ref _BestowAbility, PgAbility.CreateNew); } } private IPgAbility _BestowAbility;
        public IPgQuest BestowQuest { get { return GetObject(8, ref _BestowQuest, PgQuest.CreateNew); } } private IPgQuest _BestowQuest;
        public int CraftPoints { get { return RawCraftPoints.HasValue ? RawCraftPoints.Value : 0; } }
        public int? RawCraftPoints { get { return GetInt(12); } }
        public int CraftingTargetLevel { get { return RawCraftingTargetLevel.HasValue ? RawCraftingTargetLevel.Value : 0; } }
        public int? RawCraftingTargetLevel { get { return GetInt(16); } }
        public string Description { get { return GetString(20); } }
        public ItemDroppedAppearance DroppedAppearance { get { return GetEnum<ItemDroppedAppearance>(24); } }
        public ItemSlot EquipSlot { get { return GetEnum<ItemSlot>(26); } }
        public AppearanceSkin ItemAppearanceSkin { get { return GetEnum<AppearanceSkin>(28); } }
        public AppearanceSkin ItemAppearanceCork { get { return GetEnum<AppearanceSkin>(30); } }
        public AppearanceSkin ItemAppearanceFood { get { return GetEnum<AppearanceSkin>(32); } }
        public AppearanceSkin ItemAppearancePlate { get { return GetEnum<AppearanceSkin>(34); } }
        public uint ItemAppearanceColor { get { return RawItemAppearanceColor.HasValue ? RawItemAppearanceColor.Value : 0; } }
        public uint? RawItemAppearanceColor { get { return GetUInt(36); } }
        public ItemEffectCollection EffectDescriptionList { get { return GetObjectList(40, ref _EffectDescriptionList, ItemEffectCollection.CreateItem, () => new ItemEffectCollection()); } } private ItemEffectCollection _EffectDescriptionList;
        public uint DyeColor { get { return RawDyeColor.HasValue ? RawDyeColor.Value : 0; } }
        public uint? RawDyeColor { get { return GetUInt(44); } }
        public string EquipAppearance { get { return GetString(48); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(52); } }
        public string InternalName { get { return GetString(56); } }
        public bool AllowPrefix { get { return RawAllowPrefix.HasValue && RawAllowPrefix.Value; } }
        public bool? RawAllowPrefix { get { return GetBool(60, 0); } }
        public bool AllowSuffix { get { return RawAllowSuffix.HasValue && RawAllowSuffix.Value; } }
        public bool? RawAllowSuffix { get { return GetBool(60, 2); } }
        public bool IsTemporary { get { return RawIsTemporary.HasValue && RawIsTemporary.Value; } }
        public bool? RawIsTemporary { get { return GetBool(60, 4); } }
        public bool IsCrafted { get { return RawIsCrafted.HasValue && RawIsCrafted.Value; } }
        public bool? RawIsCrafted { get { return GetBool(60, 6); } }
        public bool DestroyWhenUsedUp { get { return RawDestroyWhenUsedUp.HasValue && RawDestroyWhenUsedUp.Value; } }
        public bool? RawDestroyWhenUsedUp { get { return GetBool(60, 8); } }
        public bool IsSkillReqsDefaults { get { return RawIsSkillReqsDefaults.HasValue && RawIsSkillReqsDefaults.Value; } }
        public bool? RawIsSkillReqsDefaults { get { return GetBool(60, 10); } }
        public Appearance RequiredAppearance { get { return GetEnum<Appearance>(62); } }
        public List<RecipeItemKey> ItemKeyList { get { return GetEnumList(64, ref _ItemKeyList); } } private List<RecipeItemKey> _ItemKeyList;
        public List<ItemKeyword> EmptyKeywordList { get { return GetEnumList(68, ref _EmptyKeywordList); } } private List<ItemKeyword> _EmptyKeywordList;
        public List<ItemKeyword> RepeatedKeywordList { get { return GetEnumList(72, ref _RepeatedKeywordList); } } private List<ItemKeyword> _RepeatedKeywordList;
        public IPgQuest MacGuffinQuestName { get { return GetObject(76, ref _MacGuffinQuestName, PgQuest.CreateNew); } } private IPgQuest _MacGuffinQuestName;
        public int MaxCarryable { get { return RawMaxCarryable.HasValue ? RawMaxCarryable.Value : 0; } }
        public int? RawMaxCarryable { get { return GetInt(80); } }
        public int MaxOnVendor { get { return RawMaxOnVendor.HasValue ? RawMaxOnVendor.Value : 0; } }
        public int? RawMaxOnVendor { get { return GetInt(84); } }
        public int MaxStackSize { get { return RawMaxStackSize.HasValue ? RawMaxStackSize.Value : 0; } }
        public int? RawMaxStackSize { get { return GetInt(88); } }
        public string Name { get { return GetString(92); } }
        public ItemSkillLinkCollection SkillRequirementList { get { return GetObjectList(96, ref _SkillRequirementList, ItemSkillLinkCollection.CreateItem, () => new ItemSkillLinkCollection()); } } private ItemSkillLinkCollection _SkillRequirementList;
        public List<uint> StockDye { get { return GetUIntList(100, ref _StockDye); } } private List<uint> _StockDye;
        public List<string> StockDyeByName { get { return GetStringList(104, ref _StockDyeByName); } } private List<string> _StockDyeByName;
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get { return GetDouble(108); } }
        public int NumUses { get { return RawNumUses.HasValue ? RawNumUses.Value : 0; } }
        public int? RawNumUses { get { return GetInt(112); } }
        public ItemBehaviorCollection BehaviorList { get { return GetObjectList(116, ref _BehaviorList, ItemBehaviorCollection.CreateItem, () => new ItemBehaviorCollection()); } } private ItemBehaviorCollection _BehaviorList;
        public string DynamicCraftingSummary { get { return GetString(120); } }
        public int BestowTitle { get { return RawBestowTitle.HasValue ? RawBestowTitle.Value : 0; } }
        public int? RawBestowTitle { get { return GetInt(124); } }
        public int BestowLoreBook { get { return RawBestowLoreBook.HasValue ? RawBestowLoreBook.Value : 0; } }
        public int? RawBestowLoreBook { get { return GetInt(128); } }
        public IPgLoreBook ConnectedLoreBook { get { return GetObject(132, ref _ConnectedLoreBook, PgLoreBook.CreateNew); } } private IPgLoreBook _ConnectedLoreBook;
        public List<string> KeywordValueList { get { return GetStringList(136, ref _KeywordValueList); } } private List<string> _KeywordValueList;
    }
}
