﻿using PgJsonReader;
using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Recipe : GenericJsonObject<Recipe>, IPgRecipe
    {
        #region Direct Properties
        public string Description { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; private set; }
        public RecipeItemCollection IngredientList { get; } = new RecipeItemCollection();
        public string InternalName { get; private set; }
        public string Name { get; private set; }
        public RecipeItemCollection ResultItemList { get; } = new RecipeItemCollection();
        public Skill Skill { get; private set; }
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get; private set; }
        public List<RecipeResultEffect> ResultEffectList { get; } = new List<RecipeResultEffect>();
        public Skill SortSkill { get; private set; }
        public List<RecipeKeyword> KeywordList { get; } = new List<RecipeKeyword>();
        public int UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public int? RawUsageDelay { get; private set; }
        public string UsageDelayMessage { get; private set; }
        public RecipeAction ActionLabel { get; private set; }
        public RecipeUsageAnimation UsageAnimation { get; private set; }
        public AbilityRequirementCollection OtherRequirementList { get; } = new AbilityRequirementCollection();
        public RecipeCostCollection CostList { get; } = new RecipeCostCollection();
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get; private set; }
        public string UsageAnimationEnd { get; private set; }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        public int? RawResetTimeInSeconds { get; private set; }
        public uint? DyeColor { get; private set; }
        public Skill RewardSkill { get; private set; }
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get; private set; }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get; private set; }
        public Recipe SharesResetTimerWith { get; private set; }
        public string ItemMenuLabel { get; private set; }
        public string RawItemMenuCategory { get; private set; }
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get; private set; }
        public Recipe PrereqRecipe { get; private set; }
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get; private set; }
        public ItemKeyword RecipeItemKeyword { get; private set; }

        private bool EmptyIngredientList;
        private bool EmptyResultItemList;
        private PowerSkill RawSkill;
        private bool IsSkillParsed;
        private PowerSkill RawSortSkill;
        private bool IsSortSkillParsed;
        private PowerSkill RawRewardSkill;
        private bool IsRewardSkillParsed;
        private string RawSharesResetTimerWith;
        private bool IsSharesResetTimerWithParsed;
        private string RawPrereqRecipe;
        private bool IsPrereqRecipeParsed;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }
        public List<GenericSource> SourceList { get; private set; } = new List<GenericSource>();

        public void SetSource(GenericSource Source, ParseErrorInfo ErrorInfo)
        {
            if (Source == null)
                return;

            if (SourceList.Contains(Source))
                ErrorInfo.AddInvalidObjectFormat("Recipe Source");
            else
                SourceList.Add(Source);
        }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Description", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseDescription,
                GetString = () => Description } },
            { "IconId", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseIconId,
                GetInteger = () => RawIconId } },
            { "Ingredients", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = ParseIngredients,
                SetArrayIsEmpty = () => EmptyIngredientList = true,
                GetObjectArray = () => IngredientList,
                GetArrayIsEmpty = () => EmptyIngredientList} },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => InternalName = value,
                GetString = () => InternalName } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Name = value,
                GetString = () => Name } },
            { "ResultItems", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<RecipeItem>.ParseList("ResultItems", value, ResultItemList, errorInfo),
                SetArrayIsEmpty = () => EmptyResultItemList = true ,
                GetObjectArray = () => ResultItemList,
                GetArrayIsEmpty = () => EmptyResultItemList } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkill,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkill, null, PowerSkill.Internal_None) } },
            { "SkillLevelReq", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawSkillLevelReq = value,
                GetInteger = () => RawSkillLevelReq } },
            { "ResultEffects", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseResultEffects,
                GetStringArray = GetResultEffects } },
            { "SortSkill", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawSortSkill = StringToEnumConversion<PowerSkill>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSortSkill, null, PowerSkill.Internal_None) } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<RecipeKeyword>.ParseList(value, KeywordList, errorInfo),
                GetStringArray = () => StringToEnumConversion<RecipeKeyword>.ToStringList(KeywordList) } },
            { "ActionLabel", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => ActionLabel = StringToEnumConversion<RecipeAction>.Parse(value, TextMaps.RecipeActionStringMap, errorInfo),
                GetString = () => StringToEnumConversion<RecipeAction>.ToString(ActionLabel, TextMaps.RecipeActionStringMap, RecipeAction.Internal_None) } },
            { "UsageDelay", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawUsageDelay = value,
                GetInteger = () => RawUsageDelay } },
            { "UsageDelayMessage", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => UsageDelayMessage = value,
                GetString = () => UsageDelayMessage } },
            { "UsageAnimation", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => UsageAnimation = StringToEnumConversion<RecipeUsageAnimation>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<RecipeUsageAnimation>.ToString(UsageAnimation, null, RecipeUsageAnimation.Internal_None) } },
            { "OtherRequirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<AbilityRequirement>.ParseList("OtherRequirements", value, OtherRequirementList, errorInfo),
                GetObjectArray = () => OtherRequirementList,
                SimplifyArray = true } },
            { "Costs", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<RecipeCost>.ParseList("Costs", value, CostList, errorInfo),
                GetObjectArray = () => CostList } },
            { "NumResultItems", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNumResultItems = value,
                GetInteger = () => RawNumResultItems } },
            { "UsageAnimationEnd", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => UsageAnimationEnd = value,
                GetString = () => UsageAnimationEnd } },
            { "ResetTimeInSeconds", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawResetTimeInSeconds = value,
                GetInteger = () => RawResetTimeInSeconds } },
            { "DyeColor", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseDyeColor,
                GetString = () => DyeColor.HasValue ? InvariantCulture.ColorToString(DyeColor.Value) : null } },
            { "RewardSkill", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseRewardSkill,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawRewardSkill, null, PowerSkill.Internal_None) } },
            { "RewardSkillXp", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRewardSkillXp = value,
                GetInteger = () => RawRewardSkillXp } },
            { "RewardSkillXpFirstTime", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRewardSkillXpFirstTime = value,
                GetInteger = () => RawRewardSkillXpFirstTime } },
            { "SharesResetTimerWith", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawSharesResetTimerWith = value,
                GetString = () => RawSharesResetTimerWith } },
            { "ItemMenuLabel", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => ItemMenuLabel = value,
                GetString = () => ItemMenuLabel } },
            { "ItemMenuKeywordReq", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RecipeItemKeyword = StringToEnumConversion<ItemKeyword>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(RecipeItemKeyword, null, ItemKeyword.Internal_None) } },
            { "IsItemMenuKeywordReqSufficient", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsItemMenuKeywordReqSufficient = value,
                GetBool = () => RawIsItemMenuKeywordReqSufficient } },
            { "ItemMenuCategory", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseItemMenuCategory,
                GetString = GetItemMenuCategory } },
            { "ItemMenuCategoryLevel", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawItemMenuCategoryLevel = value,
                GetInteger = () => RawItemMenuCategoryLevel } },
            { "PrereqRecipe", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawPrereqRecipe = value,
                GetString = () => RawPrereqRecipe } },
        }; } }

        private void ParseDescription(string value, ParseErrorInfo ErrorInfo)
        {
            Description = value;
            ErrorInfo.LookForIconId(Description);
        }

        private void ParseIconId(int value, ParseErrorInfo ErrorInfo)
        {
            if (value > 0)
            {
                RawIconId = value;
                ErrorInfo.AddIconId(value);

                PgJsonObjects.Skill.UpdateAnySkillIcon(RawSkill, RawIconId);
                PgJsonObjects.Skill.UpdateAnySkillIcon(RawRewardSkill, RawIconId);
            }
            else
                RawIconId = null;
        }

        private void ParseIngredients(JsonObject value, ParseErrorInfo ErrorInfo)
        {
            RecipeItem ParsedIngredient;
            JsonObjectParser<RecipeItem>.InitAsSubitem("Ingredients", value, out ParsedIngredient, ErrorInfo);

            if (ParsedIngredient != null)
                if (ParsedIngredient.AttuneToCrafter)
                    ErrorInfo.AddInvalidObjectFormat("Recipe Ingredients");
                else
                    IngredientList.Add(ParsedIngredient);
        }

        private void ParseSkill(string value, ParseErrorInfo ErrorInfo)
        {
            RawSkill = StringToEnumConversion<PowerSkill>.Parse(value, ErrorInfo);
            PgJsonObjects.Skill.UpdateAnySkillIcon(RawSkill, RawIconId);
        }

        private bool ParseResultEffects(string value, ParseErrorInfo ErrorInfo)
        {
            RecipeResultEffect NewResultEffect;
            if (ParseResultEffect(value, ErrorInfo, out NewResultEffect))
            {
                ResultEffectList.Add(NewResultEffect);
                return true;
            }
            else
                return false;
        }

        private List<string> GetResultEffects()
        {
            List<string> Result = new List<string>();

            foreach (RecipeResultEffect Item in ResultEffectList)
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

                    default:
                        Result.Add(StringToEnumConversion<RecipeEffect>.ToString(Item.Effect, TextMaps.RecipeEffectStringMap));
                        break;
                }
            }

            return Result;
        }

        private void ParseDyeColor(string value, ParseErrorInfo ErrorInfo)
        {
            uint NewColor;
            if (InvariantCulture.TryParseColor(value, out NewColor))
                DyeColor = NewColor;
            else
                ErrorInfo.AddInvalidString("Item DyeColor", value);
        }

        private void ParseRewardSkill(string value, ParseErrorInfo ErrorInfo)
        {
            RawRewardSkill = StringToEnumConversion<PowerSkill>.Parse(value, ErrorInfo);

            PgJsonObjects.Skill.UpdateAnySkillIcon(RawRewardSkill, RawIconId);
        }

        private void ParseItemMenuCategory(string value, ParseErrorInfo ErrorInfo)
        {
            if (value == "TSysExtract")
                RawItemMenuCategory = "Extract";

            else if (value == "TSysDistill")
                RawItemMenuCategory = "Distill";
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

        private void ParsePrereqRecipe(string value, ParseErrorInfo errorInfo)
        {
            //TODO
        }

        private bool ParseResultEffect(string value, ParseErrorInfo ErrorInfo, out RecipeResultEffect NewResultEffect)
        {
            NewResultEffect = new RecipeResultEffect();

            if (StringToEnumConversion<RecipeEffect>.TryParse(value, TextMaps.RecipeEffectStringMap, out RecipeEffect ConvertedRecipeEffect, null))
            {
                NewResultEffect.Effect = ConvertedRecipeEffect;
                return true;
            }

            //if (ParseDecomposeEffect(RawEffect, ErrorInfo, ref NewResultEffect))
            //    return true;

            if (ParseExtractEffect(value, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseRepairEffect(value, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseCraftEffect(value, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseEnhanceEffect(value, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseAddItemPower(value, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseBrewItem(value, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseAdjustRecipeReuseTime(value, ErrorInfo, ref NewResultEffect))
                return true;

            ErrorInfo.AddMissingEnum("RecipeEffect", value);
            return false;
        }

        /*
        private bool ParseDecomposeEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string DecomposePattern = "DecomposeItemByTSysLevels(";
            if (RawEffect.StartsWith(DecomposePattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.DecomposeItemByTSysLevels;
                string Decomposed = RawEffect.Substring(DecomposePattern.Length, RawEffect.Length - DecomposePattern.Length - 1);
                string[] DecomposedSplit = Decomposed.Split(',');

                if (DecomposedSplit.Length == 2)
                {
                    DecomposeMaterial ConvertedMaterial;
                    DecomposeSkill ConvertedSkill;
                    if (StringToEnumConversion<DecomposeMaterial>.TryParse(DecomposedSplit[0], out ConvertedMaterial, ErrorInfo) &&
                        StringToEnumConversion<DecomposeSkill>.TryParse(DecomposedSplit[1], out ConvertedSkill, ErrorInfo))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.Material = ConvertedMaterial;
                        NewResultEffect.Skill = ConvertedSkill;
                        return true;
                    }
                }
            }

            return false;
        }
        */

        private bool ParseExtractEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string ExtractPattern = "ExtractTSysPower(";
            if (RawEffect.StartsWith(ExtractPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.ExtractTSysPower;
                string Extracted = RawEffect.Substring(ExtractPattern.Length, RawEffect.Length - ExtractPattern.Length - 1);
                string[] ExtractedSplit = Extracted.Split(',');

                if (ExtractedSplit.Length == 4)
                {
                    Augment ConvertedAugment;
                    DecomposeSkill ConvertedSkill;
                    int MinLevel, MaxLevel;
                    //DecomposeMaterial ConvertedMaterial;
                    if (StringToEnumConversion<Augment>.TryParse(ExtractedSplit[0], out ConvertedAugment, ErrorInfo) &&
                        StringToEnumConversion<DecomposeSkill>.TryParse(ExtractedSplit[1], out ConvertedSkill, ErrorInfo) &&
                        int.TryParse(ExtractedSplit[2], out MinLevel) &&
                        int.TryParse(ExtractedSplit[3], out MaxLevel))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.ExtractedAugment = ConvertedAugment;
                        NewResultEffect.Skill = ConvertedSkill;
                        NewResultEffect.MinLevel = MinLevel;
                        NewResultEffect.MaxLevel = MaxLevel;
                        return true;
                    }
                }
            }

            return false;
        }

        private string GetExtractResultEffects(RecipeResultEffect Item)
        {
            string Result = "ExtractTSysPower(";

            Result += StringToEnumConversion<Augment>.ToString(Item.ExtractedAugment) + ",";
            Result += StringToEnumConversion<DecomposeSkill>.ToString(Item.Skill) + ",";
            Result += Item.MinLevel.ToString() + ",";
            Result += Item.MaxLevel.ToString() + ")";

            return Result;
        }

        private bool ParseRepairEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string RepairPattern = "RepairItemDurability(";
            if (RawEffect.StartsWith(RepairPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.RepairItemDurability;
                string Repaired = RawEffect.Substring(RepairPattern.Length, RawEffect.Length - RepairPattern.Length - 1);
                string[] RepairedSplit = Repaired.Split(',');

                if (RepairedSplit.Length == 5)
                {
                    float RepairMinEfficiency;
                    FloatFormat RepairMinEfficiencyFormat;
                    float RepairMaxEfficiency;
                    FloatFormat RepairMaxEfficiencyFormat;
                    int RepairCooldown;
                    int MinLevel, MaxLevel;
                    if (Tools.TryParseFloat(RepairedSplit[0], out RepairMinEfficiency, out RepairMinEfficiencyFormat) &&
                        Tools.TryParseFloat(RepairedSplit[1], out RepairMaxEfficiency, out RepairMaxEfficiencyFormat) &&
                        int.TryParse(RepairedSplit[2], out RepairCooldown) &&
                        int.TryParse(RepairedSplit[3], out MinLevel) &&
                        int.TryParse(RepairedSplit[4], out MaxLevel))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.RepairMinEfficiency = RepairMinEfficiency;
                        NewResultEffect.RepairMinEfficiencyFormat = RepairMinEfficiencyFormat;
                        NewResultEffect.RepairMaxEfficiency = RepairMaxEfficiency;
                        NewResultEffect.RepairMaxEfficiencyFormat = RepairMaxEfficiencyFormat;
                        NewResultEffect.RepairCooldown = RepairCooldown;
                        NewResultEffect.MinLevel = MinLevel;
                        NewResultEffect.MaxLevel = MaxLevel;
                        return true;
                    }
                }
            }

            return false;
        }

        private string GetRepairItemResultEffects(RecipeResultEffect Item)
        {
            string Result = "RepairItemDurability(";

            Result += Tools.FloatToString(Item.RepairMinEfficiency, Item.RepairMinEfficiencyFormat) + ",";
            Result += Tools.FloatToString(Item.RepairMaxEfficiency, Item.RepairMaxEfficiencyFormat) + ",";
            Result += Item.RepairCooldown.ToString() + ",";
            Result += Item.MinLevel.ToString() + ",";
            Result += Item.MaxLevel.ToString() + ")";

            return Result;
        }

        private bool ParseCraftEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string CraftPattern = "TSysCraftedEquipment(";
            if (RawEffect.StartsWith(CraftPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.TSysCraftedEquipment;
                string Crafted = RawEffect.Substring(CraftPattern.Length, RawEffect.Length - CraftPattern.Length - 1);
                string[] CraftedSplit = Crafted.Split(',');

                if (CraftedSplit.Length == 0)
                {
                    ErrorInfo.AddMissingEnum("RecipeEffect", RawEffect);
                    return false;
                }

                else
                {
                    CraftedBoost Boost;
                    int BoostLevel = 0;
                    bool IsCamouflaged = false;
                    int? AdditionalEnchantments = null;
                    Appearance BoostedAnimal = Appearance.Internal_None;

                    string CraftedItem = CraftedSplit[0];

                    if (CraftedItem.Length > 0 && CraftedItem[CraftedItem.Length - 1] == 'C')
                    {
                        IsCamouflaged = true;
                        CraftedItem = CraftedItem.Substring(0, CraftedItem.Length - 1);
                    }

                    if (CraftedItem.Length > 0)
                    {
                        int DigitIndex = CraftedItem.Length;
                        while (DigitIndex > 0 && char.IsDigit(CraftedItem[DigitIndex - 1]))
                            DigitIndex--;

                        if (DigitIndex < CraftedItem.Length)
                        {
                            string LevelString = CraftedItem.Substring(DigitIndex, CraftedItem.Length - DigitIndex);
                            int ParsedLevel;
                            if (int.TryParse(LevelString, out ParsedLevel))
                            {
                                CraftedItem = CraftedItem.Substring(0, DigitIndex);
                                BoostLevel = ParsedLevel;
                            }
                        }
                    }

                    CraftedBoost ConvertedBoost;
                    if (StringToEnumConversion<CraftedBoost>.TryParse(CraftedItem, out ConvertedBoost, ErrorInfo))
                        Boost = ConvertedBoost;
                    else
                        return false;

                    if (CraftedSplit.Length > 1)
                    {
                        int ParsedAdditionalEnchantments;
                        if (int.TryParse(CraftedSplit[1], out ParsedAdditionalEnchantments))
                            AdditionalEnchantments = ParsedAdditionalEnchantments;
                        else
                        {
                            ErrorInfo.AddMissingEnum("RecipeEffect", RawEffect);
                            return false;
                        }
                    }

                    if (CraftedSplit.Length > 2)
                    {
                        Appearance ParsedBoostedAnimal;
                        if (StringToEnumConversion<Appearance>.TryParse(CraftedSplit[2], out ParsedBoostedAnimal, ErrorInfo))
                            BoostedAnimal = ParsedBoostedAnimal;
                        else
                            return false;
                    }

                    NewResultEffect.Effect = ConvertedRecipeEffect;
                    NewResultEffect.Boost = Boost;
                    NewResultEffect.BoostLevel = BoostLevel;
                    NewResultEffect.IsCamouflaged = IsCamouflaged;
                    NewResultEffect.AdditionalEnchantments = AdditionalEnchantments;
                    NewResultEffect.BoostedAnimal = BoostedAnimal;
                    return true;
                }
            }

            return false;
        }

        private string GetCraftedResultEffects(RecipeResultEffect Item)
        {
            string Result = "TSysCraftedEquipment(";

            Result += StringToEnumConversion<CraftedBoost>.ToString(Item.Boost);
            Result += Item.BoostLevel.ToString();
            if (Item.IsCamouflaged)
                Result += "C";
            if (Item.AdditionalEnchantments.HasValue)
                Result += "," + Item.AdditionalEnchantments.Value.ToString();
            if (Item.BoostedAnimal != Appearance.Internal_None)
                Result += "," + StringToEnumConversion<Appearance>.ToString(Item.BoostedAnimal);

            Result += ")";

            return Result;
        }

        private bool ParseEnhanceEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string EnhancePattern = "CraftingEnhanceItem";
            if (RawEffect.StartsWith(EnhancePattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.CraftingEnhanceItem;
                string Enhance = RawEffect.Substring(EnhancePattern.Length, RawEffect.Length - EnhancePattern.Length - 1);
                string[] EnhanceSplit = Enhance.Split('(');

                if (EnhanceSplit.Length == 2)
                {
                    EnhancementEffect ConvertedEnhancementEffect;
                    if (StringToEnumConversion<EnhancementEffect>.TryParse(EnhanceSplit[0], out ConvertedEnhancementEffect, ErrorInfo))
                    {
                        string[] EnhanceDataSplit = EnhanceSplit[1].Split(',');
                        if (EnhanceDataSplit.Length == 2)
                        {
                            float AddedQuantity;
                            int ConsumedEnhancementPoints;

                            if (InvariantCulture.TryParseSingle(EnhanceDataSplit[0], out AddedQuantity) &&
                                int.TryParse(EnhanceDataSplit[1], out ConsumedEnhancementPoints))
                            {
                                NewResultEffect.Effect = ConvertedRecipeEffect;
                                NewResultEffect.Enhancement = ConvertedEnhancementEffect;
                                NewResultEffect.AddedQuantity = AddedQuantity;
                                NewResultEffect.ConsumedEnhancementPoints = ConsumedEnhancementPoints;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private string GetCraftingEnhanceResultEffects(RecipeResultEffect Item)
        {
            string Result = "CraftingEnhanceItem";

            Result += StringToEnumConversion<EnhancementEffect>.ToString(Item.Enhancement) + "(";
            Result += InvariantCulture.SingleToString(Item.AddedQuantity) + ",";
            Result += Item.ConsumedEnhancementPoints.ToString() + ")";

            return Result;
        }

        private bool ParseAddItemPower(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string AddItemPowerPattern = "AddItemTSysPower(";
            if (RawEffect.StartsWith(AddItemPowerPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.AddItemTSysPower;
                string PowerAdded = RawEffect.Substring(AddItemPowerPattern.Length, RawEffect.Length - AddItemPowerPattern.Length - 1);
                string[] PowerAddedSplit = PowerAdded.Split(',');

                if (PowerAddedSplit.Length == 2)
                {
                    ShamanicSlotPower ConvertedSlot;
                    int PowerLevel;
                    if (StringToEnumConversion<ShamanicSlotPower>.TryParse(PowerAddedSplit[0], out ConvertedSlot, ErrorInfo) &&
                        int.TryParse(PowerAddedSplit[1], out PowerLevel))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.SlotPower = ConvertedSlot;
                        NewResultEffect.SlotPowerLevel = PowerLevel;
                        return true;
                    }
                    else
                        ErrorInfo.AddMissingEnum("ShamanicSlotPower", PowerAddedSplit[0]);
                }
            }

            return false;
        }

        private string GetPowerResultEffects(RecipeResultEffect Item)
        {
            string Result = "AddItemTSysPower(";

            Result += StringToEnumConversion<ShamanicSlotPower>.ToString(Item.SlotPower) + ",";
            Result += Item.SlotPowerLevel.ToString() + ")";

            return Result;
        }

        private bool ParseBrewItem(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string BrewItemPattern = "BrewItem(";
            if (RawEffect.StartsWith(BrewItemPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.BrewItem;
                string Brewed = RawEffect.Substring(BrewItemPattern.Length, RawEffect.Length - BrewItemPattern.Length - 1);
                string[] BrewedSplit = Brewed.Split(',');

                if (BrewedSplit.Length == 3)
                {
                    int BrewPartCount;
                    int BrewLevel;
                    if (int.TryParse(BrewedSplit[0].Trim(), out BrewPartCount) && int.TryParse(BrewedSplit[1].Trim(), out BrewLevel))
                    {
                        string[] PartsAndResults = BrewedSplit[2].Trim().Split('=');
                        if (PartsAndResults.Length == 2)
                        {
                            string[] Parts = PartsAndResults[0].Trim().Split('+');
                            string[] Results = PartsAndResults[1].Trim().Split('+');

                            if (Parts.Length > 0 && Parts[0].Trim().Length > 0 && Results.Length > 0 && Results[0].Trim().Length > 0)
                            {
                                List<RecipeItemKey> BrewPartList = new List<RecipeItemKey>();
                                foreach (string RawPart in Parts)
                                {
                                    RecipeItemKey ParsedPart;
                                    if (StringToEnumConversion<RecipeItemKey>.TryParse(RawPart, out ParsedPart, ErrorInfo))
                                        BrewPartList.Add(ParsedPart);
                                }

                                List<RecipeResultKey> BrewResultList = new List<RecipeResultKey>();
                                foreach (string RawResult in Results)
                                {
                                    RecipeResultKey ParsedResult;
                                    if (StringToEnumConversion<RecipeResultKey>.TryParse(RawResult, out ParsedResult, ErrorInfo))
                                        BrewResultList.Add(ParsedResult);
                                }

                                if (BrewPartList.Count > 0 && BrewResultList.Count > 0)
                                {
                                    NewResultEffect.Effect = ConvertedRecipeEffect;
                                    NewResultEffect.BrewPartCount = BrewPartCount;
                                    NewResultEffect.BrewLevel = BrewLevel;
                                    NewResultEffect.BrewPartList = BrewPartList;
                                    NewResultEffect.BrewResultList = BrewResultList;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private string GetBrewItemResultEffects(RecipeResultEffect Item)
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

        private bool ParseAdjustRecipeReuseTime(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string AdjustRecipeReuseTimePattern = "AdjustRecipeReuseTime(";
            if (RawEffect.StartsWith(AdjustRecipeReuseTimePattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.AdjustRecipeReuseTime;
                string Adjusted = RawEffect.Substring(AdjustRecipeReuseTimePattern.Length, RawEffect.Length - AdjustRecipeReuseTimePattern.Length - 1);
                string[] AdjustedSplit = Adjusted.Split(',');

                if (AdjustedSplit.Length == 2)
                {
                    int AdjustedReuseTime;
                    MoonPhases MoonPhase;
                    if (int.TryParse(AdjustedSplit[0].Trim(), out AdjustedReuseTime) && StringToEnumConversion<MoonPhases>.TryParse(AdjustedSplit[1].Trim(), out MoonPhase, ErrorInfo))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.AdjustedReuseTime = AdjustedReuseTime;
                        NewResultEffect.MoonPhase = MoonPhase;
                        return true;
                    }
                }
            }

            return false;
        }

        private string GetAdjustRecipeResultEffects(RecipeResultEffect Item)
        {
            string Result = "AdjustRecipeReuseTime(";

            Result += Item.AdjustedReuseTime.ToString() + ",";
            Result += StringToEnumConversion<MoonPhases>.ToString(Item.MoonPhase) + ")";

            return Result;
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                try
                {
                    string Result = "";

                    AddWithFieldSeparator(ref Result, Name);
                    AddWithFieldSeparator(ref Result, Description);
                    foreach (RecipeItem Ingredient in IngredientList)
                        AddWithFieldSeparator(ref Result, Ingredient.TextContent);
                    foreach (RecipeItem ResultItem in ResultItemList)
                        AddWithFieldSeparator(ref Result, ResultItem.TextContent);
                    if (Skill != null)
                        AddWithFieldSeparator(ref Result, Skill.Name);
                    foreach (RecipeResultEffect Item in ResultEffectList)
                        AddWithFieldSeparator(ref Result, Item.CombinedEffect);
                    if (SortSkill != null)
                        AddWithFieldSeparator(ref Result, SortSkill.Name);
                    foreach (RecipeKeyword Keyword in KeywordList)
                        AddWithFieldSeparator(ref Result, TextMaps.RecipeKeywordTextMap[Keyword]);
                    if (ActionLabel != RecipeAction.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.RecipeActionTextMap[ActionLabel]);
                    AddWithFieldSeparator(ref Result, UsageDelayMessage);
                    if (UsageAnimation != RecipeUsageAnimation.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.RecipeUsageAnimationTextMap[UsageAnimation]);
                    foreach (AbilityRequirement Requirement in OtherRequirementList)
                        AddWithFieldSeparator(ref Result, Requirement.TextContent);
                    foreach (RecipeCost Item in CostList)
                        AddWithFieldSeparator(ref Result, TextMaps.RecipeCurrencyTextMap[Item.Currency]);
                    if (RewardSkill != null)
                        AddWithFieldSeparator(ref Result, RewardSkill.Name);
                    if (SharesResetTimerWith != null)
                        AddWithFieldSeparator(ref Result, SharesResetTimerWith.Name);
                    AddWithFieldSeparator(ref Result, ItemMenuLabel);
                    if (RecipeItemKeyword != ItemKeyword.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[RecipeItemKeyword]);
                    AddWithFieldSeparator(ref Result, RawItemMenuCategory);
                    if (PrereqRecipe != null)
                        AddWithFieldSeparator(ref Result, PrereqRecipe.Name);

                    return Result;
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            foreach (RecipeItem Item in IngredientList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            foreach (RecipeItem Item in ResultItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            if (RawSkill != PowerSkill.Internal_None && RawSkill != PowerSkill.AnySkill && RawSkill != PowerSkill.Unknown)
                Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsSkillParsed, ref IsConnected, this);

            if (RawSortSkill != PowerSkill.Internal_None && RawSortSkill != PowerSkill.AnySkill && RawSortSkill != PowerSkill.Unknown)
                SortSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSortSkill, SortSkill, ref IsSortSkillParsed, ref IsConnected, this);

            if (RawRewardSkill != PowerSkill.Internal_None && RawRewardSkill != PowerSkill.AnySkill && RawRewardSkill != PowerSkill.Unknown)
                RewardSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawRewardSkill, RewardSkill, ref IsRewardSkillParsed, ref IsConnected, this);

            foreach (AbilityRequirement Item in OtherRequirementList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            foreach (RecipeCost Item in CostList)
                Item.Connect(ErrorInfo, this, AllTables);

            if (RawSharesResetTimerWith != null)
                SharesResetTimerWith = PgJsonObjects.Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawSharesResetTimerWith, SharesResetTimerWith, ref IsSharesResetTimerWithParsed, ref IsConnected, this);

            if (RawPrereqRecipe != null)
                PrereqRecipe = PgJsonObjects.Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawPrereqRecipe, PrereqRecipe, ref IsPrereqRecipeParsed, ref IsConnected, this);

            return IsConnected;
        }

        public static bool ConnectTableByInternalName(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> RecipeTable, List<string> ConnectedList, Dictionary<string, Recipe> ConnectedTable)
        {
            bool Connected = false;

            foreach (string s in ConnectedList)
            {
                bool Found = false;
                foreach (KeyValuePair<string, IGenericJsonObject> Entry in RecipeTable)
                {
                    Recipe RecipeValue = Entry.Value as Recipe;
                    if (RecipeValue.InternalName == s)
                    {
                        Found = true;
                        Connected = true;
                        if (ConnectedTable.ContainsKey(s))
                            ErrorInfo.AddDuplicateString("Recipe", s);
                        else
                            ConnectedTable.Add(Entry.Key, RecipeValue);
                        break;
                    }
                }

                if (!Found)
                    ErrorInfo.AddMissingKey(s);
            }

            return Connected;
        }

        public static Recipe ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> RecipeTable, string RawRecipeName, Recipe ParsedRecipe, ref bool IsRawRecipeParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawRecipeParsed)
                return ParsedRecipe;

            IsRawRecipeParsed = true;

            if (RawRecipeName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in RecipeTable)
            {
                Recipe RecipeValue = Entry.Value as Recipe;
                if (RecipeValue.InternalName == RawRecipeName)
                {
                    IsConnected = true;
                    RecipeValue.AddLinkBack(LinkBack);
                    return RecipeValue;
                }
            }

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in RecipeTable)
            {
                Recipe RecipeValue = Entry.Value as Recipe;
                if (RecipeValue.Name == RawRecipeName)
                {
                    IsConnected = true;
                    RecipeValue.AddLinkBack(LinkBack);
                    return RecipeValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawRecipeName);

            return null;
        }

        public static RecipeCollection ConnectByKeyword(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> RecipeTable, RecipeKeyword Keyword, RecipeCollection RecipeList, ref bool IsRawRecipeParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawRecipeParsed)
                return RecipeList;

            IsRawRecipeParsed = true;

            if (Keyword == RecipeKeyword.Internal_None)
                return RecipeList;

            RecipeList = new RecipeCollection();
            IsConnected = true;

            foreach (KeyValuePair<string, IGenericJsonObject> RecipeEntry in RecipeTable)
            {
                Recipe RecipeValue = RecipeEntry.Value as Recipe;
                if (RecipeValue.KeywordList.Contains(Keyword))
                {
                    RecipeValue.AddLinkBack(LinkBack);
                    RecipeList.Add(RecipeValue);
                }
            }

            if (RecipeList.Count == 0 && ErrorInfo != null)
                ErrorInfo.AddMissingKey(Keyword.ToString());

            return RecipeList;
        }

        public static Recipe ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> RecipeTable, int RecipeId, Recipe Recipe, ref bool IsRawRecipeParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawRecipeParsed)
                return Recipe;

            IsRawRecipeParsed = true;
            string RawRecipeId = "recipe_" + RecipeId;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in RecipeTable)
            {
                Recipe RecipeValue = Entry.Value as Recipe;
                if (RecipeValue.Key == RawRecipeId)
                {
                    IsConnected = true;
                    RecipeValue.AddLinkBack(LinkBack);
                    return RecipeValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawRecipeId);

            return null;
        }
        #endregion

        #region Recursive Components Sum
        public void MeasurePerfectCottonRatio(ref bool Continue)
        {
            double RecipePerfectCottonRatio = 0;

            if (InternalName == "CottonThread" || InternalName == "FineCottonYarn")
                RecipePerfectCottonRatio = 0;

            foreach (RecipeItem Item in IngredientList)
                if (Item != null && !double.IsNaN(Item.PerfectCottonRatio) && Item.PerfectCottonRatio > 0)
                {
                    double AddedPerfectCottonRatio = Item.PerfectCottonRatio * Item.StackSize * Item.ChanceToConsume;
                    RecipePerfectCottonRatio += AddedPerfectCottonRatio;
                }

            foreach (RecipeItem Item in ResultItemList)
                if (Item != null)
                    Item.SetPerfectCottonRatio(RecipePerfectCottonRatio);

            if ((!PerfectCottonRatio.HasValue && RecipePerfectCottonRatio > 0) || (PerfectCottonRatio.HasValue && PerfectCottonRatio.Value != RecipePerfectCottonRatio))
            {
                PerfectCottonRatio = RecipePerfectCottonRatio;
                Continue = true;
            }
        }

        public double? PerfectCottonRatio { get; private set; }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Recipe"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddString(Description, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddInt(RawIconId, data, ref offset, BaseOffset, 4);
            AddObjectList(IngredientList, data, ref offset, BaseOffset, 8, StoredObjectListTable);
            AddString(InternalName, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddString(Name, data, ref offset, BaseOffset, 16, StoredStringtable);
            AddObjectList(ResultItemList, data, ref offset, BaseOffset, 20, StoredObjectListTable);
            AddObject(Skill, data, ref offset, BaseOffset, 24, StoredObjectTable);
            AddInt(RawSkillLevelReq, data, ref offset, BaseOffset, 28);

            offset += 4;
            //AddObjectList(ResultEffectList, data, ref offset, BaseOffset, 32, StoredObjectListTable);

            AddObject(SortSkill, data, ref offset, BaseOffset, 36, StoredObjectTable);
            AddEnumList(KeywordList, data, ref offset, BaseOffset, 40, StoredEnumListTable);
            AddInt(RawUsageDelay, data, ref offset, BaseOffset, 44);
            AddString(UsageDelayMessage, data, ref offset, BaseOffset, 48, StoredStringtable);
            AddEnum(ActionLabel, data, ref offset, BaseOffset, 52);
            AddEnum(UsageAnimation, data, ref offset, BaseOffset, 54);
            AddObjectList(OtherRequirementList, data, ref offset, BaseOffset, 56, StoredObjectListTable);
            AddObjectList(CostList, data, ref offset, BaseOffset, 60, StoredObjectListTable);
            AddInt(RawNumResultItems, data, ref offset, BaseOffset, 64);
            AddString(UsageAnimationEnd, data, ref offset, BaseOffset, 68, StoredStringtable);
            AddInt(RawResetTimeInSeconds, data, ref offset, BaseOffset, 72);
            AddUInt(DyeColor, data, ref offset, BaseOffset, 76);
            AddObject(RewardSkill, data, ref offset, BaseOffset, 80, StoredObjectTable);
            AddInt(RawRewardSkillXp, data, ref offset, BaseOffset, 84);
            AddInt(RawRewardSkillXpFirstTime, data, ref offset, BaseOffset, 88);
            AddObject(SharesResetTimerWith, data, ref offset, BaseOffset, 92, StoredObjectTable);
            AddString(ItemMenuLabel, data, ref offset, BaseOffset, 96, StoredStringtable);
            AddString(RawItemMenuCategory, data, ref offset, BaseOffset, 100, StoredStringtable);
            AddInt(RawItemMenuCategoryLevel, data, ref offset, BaseOffset, 104);
            AddObject(PrereqRecipe, data, ref offset, BaseOffset, 108, StoredObjectTable);
            AddBool(RawIsItemMenuKeywordReqSufficient, data, ref offset, ref BitOffset, BaseOffset, 112, 0);
            CloseBool(ref offset, ref BitOffset);
            AddEnum(RecipeItemKeyword, data, ref offset, BaseOffset, 114);

            FinishSerializing(data, ref offset, BaseOffset, 116, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
