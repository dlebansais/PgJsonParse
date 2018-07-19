using Presentation;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItem : MainPgObject<PgItem>, IPgItem
    {
        public PgItem(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 160;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItem CreateNew(byte[] data, ref int offset)
        {
            PgItem Result = new PgItem(data, ref offset);
            return Result;
        }

        public override void Init()
        {
            foreach (string s in RawKeywordList)
                Item.ParseRawKeyword(s, null, KeywordValueList, KeywordTable, out string ParsedKeyString);

            AddLinkBack(BestowAbility);
            AddLinkBack(BestowQuest);
            AddLinkBackCollection(EffectDescriptionList, (IPgItemEffect value) => value.GetLinkBack());
            AddLinkBack(MacGuffinQuestName);
            AddLinkBackCollection(SkillRequirementList, (IPgItemSkillLink value) => new List<IBackLinkable>() { value.Link });
            AddLinkBackCollection(BehaviorList, GetBehaviorLinkBacks);
            AddLinkBack(BestowTitle);
            AddLinkBack(ConnectedLoreBook);
            AddLinkBackCollection(BestowRecipeList);
        }

        public IList<IBackLinkable> GetBehaviorLinkBacks(IPgItemBehavior value)
        {
            if (value.ServerInfo != null)
                return GetBehaviorLinkBacks(value.ServerInfo);
            else
                return null;
        }

        public IList<IBackLinkable> GetBehaviorLinkBacks(IPgServerInfo value)
        {
            IPgServerInfoEffectCollection ServerInfoEffectList = value.ServerInfoEffectList;
            IPgItemCollection GiveItemList = value.GiveItemList;
            IPgAbilityRequirementCollection OtherRequirementList = value.OtherRequirementList;

            List<IBackLinkable> Result = new List<IBackLinkable>();

            if (ServerInfoEffectList != null)
                foreach (IPgServerInfoEffect Item in ServerInfoEffectList)
                {
                    IList<IBackLinkable> ItemResult = Item.GetLinkBack();
                    if (ItemResult != null)
                        Result.AddRange(ItemResult);
                }

            if (GiveItemList != null)
                Result.AddRange(GiveItemList);

            if (OtherRequirementList != null)
                foreach (IPgAbilityRequirement Item in OtherRequirementList)
                {
                    IList<IBackLinkable> ItemResult = Item.GetLinkBack();
                    if (ItemResult != null)
                        Result.AddRange(ItemResult);
                }

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
        public IPgItemEffectCollection EffectDescriptionList { get { return GetObjectList(40, ref _EffectDescriptionList, PgItemEffectCollection.CreateItem, () => new PgItemEffectCollection()); } } private IPgItemEffectCollection _EffectDescriptionList;
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
        public bool BestowRecipesListIsEmpty { get { return RawBestowRecipesListIsEmpty.HasValue && RawBestowRecipesListIsEmpty.Value; } }
        public bool? RawBestowRecipesListIsEmpty { get { return GetBool(60, 12); } }
        public bool IsEffectDescriptionEmpty { get { return RawIsEffectDescriptionEmpty.HasValue && RawIsEffectDescriptionEmpty.Value; } }
        public bool? RawIsEffectDescriptionEmpty { get { return GetBool(60, 14); } }
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
        public IPgItemSkillLinkCollection SkillRequirementList { get { return GetObjectList(96, ref _SkillRequirementList, PgItemSkillLinkCollection.CreateItem, () => new PgItemSkillLinkCollection()); } } private IPgItemSkillLinkCollection _SkillRequirementList;
        public List<uint> StockDye { get { return GetUIntList(100, ref _StockDye); } } private List<uint> _StockDye;
        public List<string> StockDyeByName { get { return GetStringList(104, ref _StockDyeByName); } } private List<string> _StockDyeByName;
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get { return GetDouble(108); } }
        public int NumUses { get { return RawNumUses.HasValue ? RawNumUses.Value : 0; } }
        public int? RawNumUses { get { return GetInt(112); } }
        public IPgItemBehaviorCollection BehaviorList { get { return GetObjectList(116, ref _BehaviorList, PgItemBehaviorCollection.CreateItem, () => new PgItemBehaviorCollection()); } } private IPgItemBehaviorCollection _BehaviorList;
        public string DynamicCraftingSummary { get { return GetString(120); } }
        public IPgPlayerTitle BestowTitle { get { return GetObject(124, ref _BestowTitle, PgPlayerTitle.CreateNew); } } private IPgPlayerTitle _BestowTitle;
        public int BestowLoreBook { get { return RawBestowLoreBook.HasValue ? RawBestowLoreBook.Value : 0; } }
        public int? RawBestowLoreBook { get { return GetInt(128); } }
        public IPgLoreBook ConnectedLoreBook { get { return GetObject(132, ref _ConnectedLoreBook, PgLoreBook.CreateNew); } } private IPgLoreBook _ConnectedLoreBook;
        public List<string> KeywordValueList { get { return GetStringList(136, ref _KeywordValueList); } } private List<string> _KeywordValueList;
        protected override List<string> FieldTableOrder { get { return GetStringList(140, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgRecipeCollection BestowRecipeList { get { return GetObjectList(144, ref _BestowRecipeList, PgRecipeCollection.CreateItem, () => new PgRecipeCollection()); } } private IPgRecipeCollection _BestowRecipeList;
        public List<string> AppearanceDetailList { get { return GetStringList(148, ref _AppearanceDetailList); } } private List<string> _AppearanceDetailList;
        public List<string> RawKeywordList { get { return GetStringList(152, ref _RawKeywordList); } } private List<string> _RawKeywordList;
        public int UnknownSkillReqIndex { get { return RawUnknownSkillReqIndex.HasValue ? RawUnknownSkillReqIndex.Value : 0; } }
        public int? RawUnknownSkillReqIndex { get { return GetInt(156); } }

        public Dictionary<ItemKeyword, List<float>> KeywordTable { get; } = new Dictionary<ItemKeyword, List<float>>();

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "BestowRecipes", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = GetBestowRecipesList,
                GetArrayIsEmpty =() => BestowRecipesListIsEmpty } },
            { "BestowAbility", new FieldParser() {
                Type = FieldType.String,
                GetString = () => BestowAbility != null ? BestowAbility.InternalName : null } },
            { "BestowQuest", new FieldParser() {
                Type = FieldType.String,
                GetString = () => BestowQuest != null ? BestowQuest.InternalName : null } },
            { "AllowPrefix", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawAllowPrefix } },
            { "AllowSuffix", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawAllowSuffix } },
            { "CraftPoints", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawCraftPoints } },
            { "CraftingTargetLevel", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawCraftingTargetLevel } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "DroppedAppearance", new FieldParser() {
                Type = FieldType.String,
                GetString = GetDroppedAppearance } },
            { "EffectDescs", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = GetEffectDescs,
                GetArrayIsEmpty = () => IsEffectDescriptionEmpty } },
            { "DyeColor", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawDyeColor.HasValue ? InvariantCulture.ColorToString(RawDyeColor.Value) : null } },
            { "EquipAppearance", new FieldParser() {
                Type = FieldType.String,
                GetString = () => EquipAppearance } },
            { "EquipSlot", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemSlot>.ToString(EquipSlot, null, ItemSlot.Internal_None) } },
            { "IconId", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawIconId } },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InternalName } },
            { "IsTemporary", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsTemporary } },
            { "IsCrafted", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsCrafted } },
            { "Keywords", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => RawKeywordList } },
            { "MacGuffinQuestName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => MacGuffinQuestName != null ? MacGuffinQuestName.InternalName : null } },
            { "MaxCarryable", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMaxCarryable } },
            { "MaxOnVendor", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMaxOnVendor } },
            { "MaxStackSize", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMaxStackSize } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Name } },
            { "RequiredAppearance", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Appearance>.ToString(RequiredAppearance, null, Appearance.Internal_None) } },
            { "SkillReqs", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetSkillReqs } },
            { "StockDye", new FieldParser() {
                Type = FieldType.String,
                GetString = GetStockDye } },
            { "Value", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawValue } },
            { "NumUses", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumUses } },
            { "DestroyWhenUsedUp", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawDestroyWhenUsedUp } },
            { "Behaviors", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => BehaviorList } },
            { "DynamicCraftingSummary", new FieldParser() {
                Type = FieldType.String,
                GetString = () => DynamicCraftingSummary } },
            { "IsSkillReqsDefaults", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsSkillReqsDefaults } },
            { "BestowTitle", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => BestowTitle != null ? BestowTitle.Id : null  } },
            { "BestowLoreBook", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawBestowLoreBook } },
        }; } }

        private List<string> GetBestowRecipesList()
        {
            List<string> Result = new List<string>();

            foreach (IPgRecipe Item in BestowRecipeList)
                Result.Add(Item.InternalName);

            return Result;
        }

        private string GetDroppedAppearance()
        {
            if (DroppedAppearance == ItemDroppedAppearance.Internal_None)
                return null;

            string Line = StringToEnumConversion<ItemDroppedAppearance>.ToString(DroppedAppearance);

            if (AppearanceDetailList.Count > 0)
            {
                string DetailString = "";

                foreach (string DetailKey in AppearanceDetailList)
                {
                    if (DetailString.Length > 0)
                        DetailString += ";";

                    if (DetailKey == "Skin")
                        DetailString += "Skin=^" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceSkin, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Skin")
                        DetailString += "^Skin=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceSkin, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Cork")
                        DetailString += "^Cork=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceCork, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Food")
                        DetailString += "^Food=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearanceFood, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "^Plate")
                        DetailString += "^Plate=" + StringToEnumConversion<AppearanceSkin>.ToString(ItemAppearancePlate, TextMaps.AppearanceSkinStringMap);

                    else if (DetailKey == "Skin_Color")
                        DetailString += "Skin_Color=" + InvariantCulture.ColorToString(RawItemAppearanceColor.Value);
                }

                Line += "(" + DetailString + ")";
            }

            return Line;
        }

        private List<string> GetEffectDescs()
        {
            List<string> Result = new List<string>();

            foreach (IPgItemEffect Effect in EffectDescriptionList)
                Result.Add(Effect.AsEffectString());

            return Result;
        }

        private IObjectContentGenerator GetSkillReqs()
        {
            SkillRequirement Skillreq = new SkillRequirement();

            int Index = 0;
            foreach (IPgItemSkillLink Item in SkillRequirementList)
            {
                if (RawUnknownSkillReqIndex.HasValue && RawUnknownSkillReqIndex.Value == Index)
                    Skillreq.SetFieldValue("Unknown", new ItemSkillLink("Unknown", 0));

                Skillreq.SetFieldValue(Item.SkillName, Item);
                Index++;
            }

            if (RawUnknownSkillReqIndex.HasValue && RawUnknownSkillReqIndex.Value == Index)
                Skillreq.SetFieldValue("Unknown", new ItemSkillLink("Unknown", 0));

            return Skillreq;
        }

        private string GetStockDye()
        {
            if (StockDye == null)
                return null;

            string Result = "";

            for (int i = 0; i < StockDye.Count; i++)
            {
                string ColorPrefix = "Color" + (i + 1).ToString();
                Result += ";" + ColorPrefix + "=" + StockDyeByName[i];
            }

            return Result;
        }

        public override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }
    }
}
