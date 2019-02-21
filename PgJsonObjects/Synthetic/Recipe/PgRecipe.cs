using Presentation;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipe : MainPgObject<PgRecipe>, IPgRecipe
    {
        public PgRecipe(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 140;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgRecipe CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipe CreateNew(byte[] data, ref int offset)
        {
            PgRecipe Result = new PgRecipe(data, ref offset);
            return Result;
        }

        public override void Init()
        {
            AddLinkBackCollection(IngredientList, GetIngredientLinkBacks);
            AddLinkBackCollection(ResultItemList, GetIngredientLinkBacks);
            AddLinkBack(Skill);
            //AddLinkBackCollection(ResultEffectList);
            AddLinkBack(SortSkill);
            AddLinkBackCollection(OtherRequirementList, (IPgAbilityRequirement value) => value.GetLinkBack());
            //AddLinkBackCollection(CostList);
            AddLinkBack(RewardSkill);
            AddLinkBack(SharesResetTimerWith);
            AddLinkBack(PrereqRecipe);
            AddLinkBackCollection(ProtoResultItemList, GetIngredientLinkBacks);
        }

        public IList<IBackLinkable> GetIngredientLinkBacks(IPgRecipeItem value)
        {
            List<IBackLinkable> Result = new List<IBackLinkable>();
            Result.Add(value.Item);
            Result.AddRange(value.MatchingKeyItemList);

            return Result;
        }

        public override string Key { get { return GetString(0); } }
        public string Description { get { return GetString(4); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(8); } }
        public IPgRecipeItemCollection IngredientList { get { return GetObjectList(12, ref _IngredientList, PgRecipeItemCollection.CreateItem, () => new PgRecipeItemCollection()); } } private IPgRecipeItemCollection _IngredientList;
        public string InternalName { get { return GetString(16); } }
        public string Name { get { return GetString(20); } }
        public IPgRecipeItemCollection ResultItemList { get { return GetObjectList(24, ref _ResultItemList, PgRecipeItemCollection.CreateItem, () => new PgRecipeItemCollection()); } } private IPgRecipeItemCollection _ResultItemList;
        public IPgSkill Skill { get { return GetObject(28, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get { return GetInt(32); } }
        public IPgRecipeResultEffectCollection ResultEffectList { get { return GetObjectList(36, ref _ResultEffectList, PgRecipeResultEffectCollection.CreateItem, () => new PgRecipeResultEffectCollection()); } } private IPgRecipeResultEffectCollection _ResultEffectList;
        public IPgSkill SortSkill { get { return GetObject(40, ref _SortSkill, PgSkill.CreateNew); } } private IPgSkill _SortSkill;
        public List<RecipeKeyword> KeywordList { get { return GetEnumList(44, ref _KeywordList); } } private List<RecipeKeyword> _KeywordList;
        public int UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public int? RawUsageDelay { get { return GetInt(48); } }
        public string UsageDelayMessage { get { return GetString(52); } }
        public RecipeAction ActionLabel { get { return GetEnum<RecipeAction>(56); } }
        public RecipeUsageAnimation UsageAnimation { get { return GetEnum<RecipeUsageAnimation>(58); } }
        public IPgAbilityRequirementCollection OtherRequirementList { get { return GetObjectList(60, ref _OtherRequirementList, PgAbilityRequirementCollection.CreateItem, () => new PgAbilityRequirementCollection()); } } private IPgAbilityRequirementCollection _OtherRequirementList;
        public IPgRecipeCostCollection CostList { get { return GetObjectList(64, ref _CostList, PgRecipeCostCollection.CreateItem, () => new PgRecipeCostCollection()); } } private PgRecipeCostCollection _CostList;
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get { return GetInt(68); } }
        public string UsageAnimationEnd { get { return GetString(72); } }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        public int? RawResetTimeInSeconds { get { return GetInt(76); } }
        public uint? DyeColor { get { return GetUInt(80); } }
        public IPgSkill RewardSkill { get { return GetObject(84, ref _RewardSkill, PgSkill.CreateNew); } } private IPgSkill _RewardSkill;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get { return GetInt(88); } }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get { return GetInt(92); } }
        public IPgRecipe SharesResetTimerWith { get { return GetObject(96, ref _SharesResetTimerWith, CreateNew); } } private IPgRecipe _SharesResetTimerWith;
        public string ItemMenuLabel { get { return GetString(100); } }
        public string RawItemMenuCategory { get { return GetString(104); } }
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get { return GetInt(108); } }
        public IPgRecipe PrereqRecipe { get { return GetObject(112, ref _PrereqRecipe, CreateNew); } } private IPgRecipe _PrereqRecipe;
        protected override List<string> FieldTableOrder { get { return GetStringList(116, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get { return GetBool(120, 0); } }
        public bool IngredientListIsEmpty { get { return RawIngredientListIsEmpty.HasValue && RawIngredientListIsEmpty.Value; } }
        public bool? RawIngredientListIsEmpty { get { return GetBool(120, 2); } }
        public bool ResultItemListIsEmpty { get { return RawResultItemListIsEmpty.HasValue && RawResultItemListIsEmpty.Value; } }
        public bool? RawResultItemListIsEmpty { get { return GetBool(120, 4); } }
        public bool ProtoResultItemListIsEmpty { get { return RawProtoResultItemListIsEmpty.HasValue && RawProtoResultItemListIsEmpty.Value; } }
        public bool? RawProtoResultItemListIsEmpty { get { return GetBool(120, 6); } }
        public ItemKeyword RecipeItemKeyword { get { return GetEnum<ItemKeyword>(122); } }
        public IPgGenericSourceCollection SourceList { get { return GetObjectList(124, ref _SourceList, (byte[] data, ref int offset) => PgGenericSourceCollection.CreateItem(this, data, ref offset), () => new PgGenericSourceCollection()); } } private PgGenericSourceCollection _SourceList;
        public double? PerfectCottonRatio { get { return GetDouble(128); } }
        public List<ItemKeyword> ValidationIngredientKeywordList { get { return GetEnumList(132, ref _ValidationIngredientKeywordList); } } private List<ItemKeyword> _ValidationIngredientKeywordList;
        public IPgRecipeItemCollection ProtoResultItemList { get { return GetObjectList(136, ref _ProtoResultItemList, PgRecipeItemCollection.CreateItem, () => new PgRecipeItemCollection()); } } private IPgRecipeItemCollection _ProtoResultItemList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "IconId", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawIconId } },
            { "Ingredients", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => IngredientList,
                GetArrayIsEmpty = () => IngredientListIsEmpty} },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InternalName } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Name } },
            { "ResultItems", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => ResultItemList,
                GetArrayIsEmpty = () => ResultItemListIsEmpty } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Skill != null ? StringToEnumConversion<PowerSkill>.ToString(Skill.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "SkillLevelReq", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawSkillLevelReq } },
            { "ResultEffects", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = GetResultEffects } },
            { "SortSkill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SortSkill != null ? StringToEnumConversion<PowerSkill>.ToString(SortSkill.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<RecipeKeyword>.ToStringList(KeywordList) } },
            { "ActionLabel", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<RecipeAction>.ToString(ActionLabel, TextMaps.RecipeActionStringMap, RecipeAction.Internal_None) } },
            { "UsageDelay", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawUsageDelay } },
            { "UsageDelayMessage", new FieldParser() {
                Type = FieldType.String,
                GetString = () => UsageDelayMessage } },
            { "UsageAnimation", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<RecipeUsageAnimation>.ToString(UsageAnimation, null, RecipeUsageAnimation.Internal_None) } },
            { "OtherRequirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OtherRequirementList,
                SimplifyArray = true } },
            { "Costs", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => CostList } },
            { "NumResultItems", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumResultItems } },
            { "UsageAnimationEnd", new FieldParser() {
                Type = FieldType.String,
                GetString = () => UsageAnimationEnd } },
            { "ResetTimeInSeconds", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawResetTimeInSeconds } },
            { "DyeColor", new FieldParser() {
                Type = FieldType.String,
                GetString = () => DyeColor.HasValue ? InvariantCulture.ColorToString(DyeColor.Value) : null } },
            { "RewardSkill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RewardSkill != null ? StringToEnumConversion<PowerSkill>.ToString(RewardSkill.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "RewardSkillXp", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRewardSkillXp } },
            { "RewardSkillXpFirstTime", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRewardSkillXpFirstTime } },
            { "SharesResetTimerWith", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SharesResetTimerWith != null ? SharesResetTimerWith.InternalName : null } },
            { "ItemMenuLabel", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ItemMenuLabel } },
            { "ItemMenuKeywordReq", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(RecipeItemKeyword, TextMaps.ItemKeywordStringMap, ItemKeyword.Internal_None) } },
            { "IsItemMenuKeywordReqSufficient", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsItemMenuKeywordReqSufficient } },
            { "ItemMenuCategory", new FieldParser() {
                Type = FieldType.String,
                GetString = GetItemMenuCategory } },
            { "ItemMenuCategoryLevel", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawItemMenuCategoryLevel } },
            { "PrereqRecipe", new FieldParser() {
                Type = FieldType.String,
                GetString = () => PrereqRecipe != null ? PrereqRecipe.InternalName : null } },
            { "ValidationIngredientKeywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<ItemKeyword>.ToStringList(ValidationIngredientKeywordList) } },
            { "ProtoResultItems", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => ProtoResultItemList,
                GetArrayIsEmpty = () => ProtoResultItemListIsEmpty } },
        }; } }

        private List<string> GetResultEffects()
        {
            List<string> Result = new List<string>();

            foreach (IPgRecipeResultEffect Item in ResultEffectList)
            {
                switch (Item.Effect)
                {
                    case RecipeEffect.ExtractTSysPower:
                        Result.Add(GetExtractResultEffects(Item));
                        break;

                    case RecipeEffect.RepairItemDurability:
                        Result.Add(GetRepairItemResultEffects(Item));
                        break;

                    case RecipeEffect.TSysCraftedEquipment:
                        Result.Add(GetCraftedResultEffects(Item));
                        break;

                    case RecipeEffect.CraftingEnhanceItem:
                        Result.Add(GetCraftingEnhanceResultEffects(Item));
                        break;

                    case RecipeEffect.AddItemTSysPower:
                        Result.Add(GetPowerResultEffects(Item));
                        break;

                    case RecipeEffect.BrewItem:
                        Result.Add(GetBrewItemResultEffects(Item));
                        break;

                    case RecipeEffect.AdjustRecipeReuseTime:
                        Result.Add(GetAdjustRecipeResultEffects(Item));
                        break;

                    case RecipeEffect.GiveItemPower:
                        Result.Add(GetGiveItemPowerEffects(Item));
                        break;

                    default:
                        Result.Add(StringToEnumConversion<RecipeEffect>.ToString(Item.Effect, TextMaps.RecipeEffectStringMap));
                        break;
                }
            }

            return Result;
        }

        private string GetExtractResultEffects(IPgRecipeResultEffect Item)
        {
            string Result = "ExtractTSysPower(";

            Result += StringToEnumConversion<Augment>.ToString(Item.ExtractedAugment) + ",";
            Result += StringToEnumConversion<DecomposeSkill>.ToString(Item.Skill) + ",";
            Result += Item.MinLevel.ToString() + ",";
            Result += Item.MaxLevel.ToString() + ")";

            return Result;
        }

        private string GetRepairItemResultEffects(IPgRecipeResultEffect Item)
        {
            string Result = "RepairItemDurability(";

            Result += Tools.FloatToString(Item.RepairMinEfficiency, Item.RepairMinEfficiencyFormat) + ",";
            Result += Tools.FloatToString(Item.RepairMaxEfficiency, Item.RepairMaxEfficiencyFormat) + ",";
            Result += Item.RepairCooldown.ToString() + ",";
            Result += Item.MinLevel.ToString() + ",";
            Result += Item.MaxLevel.ToString() + ")";

            return Result;
        }

        private string GetCraftedResultEffects(IPgRecipeResultEffect Item)
        {
            string Result = "TSysCraftedEquipment(";

            Result += StringToEnumConversion<CraftedBoost>.ToString(Item.Boost);
            Result += Item.BoostLevel.ToString();
            if (Item.IsCamouflaged)
                Result += "C";
            if (Item.RawAdditionalEnchantments.HasValue)
                Result += "," + Item.RawAdditionalEnchantments.Value.ToString();
            if (Item.BoostedAnimal != Appearance.Internal_None)
                Result += "," + StringToEnumConversion<Appearance>.ToString(Item.BoostedAnimal);

            Result += ")";

            return Result;
        }

        private string GetCraftingEnhanceResultEffects(IPgRecipeResultEffect Item)
        {
            string Result = "CraftingEnhanceItem";

            Result += StringToEnumConversion<EnhancementEffect>.ToString(Item.Enhancement) + "(";
            Result += InvariantCulture.SingleToString(Item.AddedQuantity) + ",";
            Result += Item.ConsumedEnhancementPoints.ToString() + ")";

            return Result;
        }

        private string GetPowerResultEffects(IPgRecipeResultEffect Item)
        {
            string Result = "AddItemTSysPower(";

            Result += StringToEnumConversion<ShamanicSlotPower>.ToString(Item.SlotPower) + ",";
            Result += Item.SlotPowerLevel.ToString() + ")";

            return Result;
        }

        private string GetBrewItemResultEffects(IPgRecipeResultEffect Item)
        {
            string Result = "BrewItem(";

            Result += Item.BrewPartCount.ToString() + ",";
            Result += Item.BrewLevel.ToString() + ",";

            string Parts = "";
            foreach (RecipeItemKey k in Item.BrewPartList)
            {
                if (Parts.Length > 0)
                    Parts += "+";

                Parts += StringToEnumConversion<RecipeItemKey>.ToString(k);
            }

            string PartResults = "";
            foreach (RecipeResultKey k in Item.BrewResultList)
            {
                if (PartResults.Length > 0)
                    PartResults += "+";

                PartResults += StringToEnumConversion<RecipeResultKey>.ToString(k);
            }

            Result += Parts + "=" + PartResults + ")";

            return Result;
        }

        private string GetAdjustRecipeResultEffects(IPgRecipeResultEffect Item)
        {
            string Result = "AdjustRecipeReuseTime(";

            Result += Item.AdjustedReuseTime.ToString() + ",";
            Result += StringToEnumConversion<MoonPhases>.ToString(Item.MoonPhase) + ")";

            return Result;
        }

        private string GetGiveItemPowerEffects(IPgRecipeResultEffect Item)
        {
            string Result = "GiveTSysItem(";

            Result += Item.Item != null ? Item.Item.InternalName : "unknown";
            Result += ")";

            return Result;
        }

        private string GetItemMenuCategory()
        {
            if (RawItemMenuCategory == "Extract")
                return "TSysExtract";

            else if (RawItemMenuCategory == "Distill")
                return "TSysDistill";

            else
                return null;
        }

        public override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }
    }
}
