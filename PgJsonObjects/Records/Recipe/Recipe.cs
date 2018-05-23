using PgJsonReader;
using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace PgJsonObjects
{
    public class Recipe : GenericJsonObject<Recipe>
    {
        #region Direct Properties
        public string Description { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        private int? RawIconId;
        public List<RecipeItem> IngredientList { get; } = new List<RecipeItem>();
        private bool EmptyIngredientList;
        public string InternalName { get; private set; }
        public string Name { get; private set; }
        public List<RecipeItem> ResultItemList { get; } = new List<RecipeItem>();
        private bool EmptyResultItemList;
        public PowerSkill Skill { get; private set; }
        public Skill ConnectedSkill { get; private set; }
        private bool IsSkillParsed;
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get; private set; }
        public List<RecipeResultEffect> ResultEffectList { get; } = new List<RecipeResultEffect>();
        public PowerSkill SortSkill { get; private set; }
        public Skill ConnectedSortSkill { get; private set; }
        private bool IsSortSkillParsed;
        public List<RecipeKeyword> KeywordList { get; } = new List<RecipeKeyword>();
        public RecipeAction ActionLabel { get; private set; }
        public int UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public int? RawUsageDelay { get; private set; }
        public string UsageDelayMessage { get; private set; }
        public RecipeUsageAnimation UsageAnimation { get; private set; }
        public List<AbilityRequirement> OtherRequirementList { get; } = new List<AbilityRequirement>();
        public List<RecipeCost> CostList { get; } = new List<RecipeCost>();
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get; private set; }
        public string UsageAnimationEnd { get; private set; }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        public int? RawResetTimeInSeconds { get; private set; }
        public uint? DyeColor { get; private set; }
        public PowerSkill RewardSkill { get; private set; }
        public Skill ConnectedRewardSkill { get; private set; }
        private bool IsRewardSkillParsed;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get; private set; }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get; private set; }
        public Recipe SharesResetTimerWith { get; private set; }
        private string RawSharesResetTimerWith;
        private bool IsSharesResetTimerWithParsed;
        public string ItemMenuLabel { get; private set; }
        public ItemKeyword RecipeItemKeyword { get; private set; }
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get; private set; }
        public string RawItemMenuCategory { get; private set; }
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get; private set; }
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
            { "Description", new FieldParser() { Type = FieldType.String, ParserString = ParseDescription } },
            { "IconId", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseIconId } },
            { "Ingredients", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseIngredients, ParserSetArrayIsEmpty = () => EmptyIngredientList = true } },
            { "InternalName", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { InternalName = value; }} },
            { "Name", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { Name = value; }} },
            { "ResultItems", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseResultItems } },
            { "Skill", new FieldParser() { Type = FieldType.String, ParserString = ParseSkill } },
            { "SkillLevelReq", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawSkillLevelReq = value; }} },
            { "ResultEffects", new FieldParser() { Type = FieldType.StringArray, ParserStringArray = ParseResultEffects } },
            { "SortSkill", new FieldParser() { Type = FieldType.String, ParserString = ParseSortSkill } },
            { "Keywords", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = ParseKeywords } },
            { "ActionLabel", new FieldParser() { Type = FieldType.String, ParserString = ParseActionLabel } },
            { "UsageDelay", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawUsageDelay = value; }} },
            { "UsageDelayMessage", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { UsageDelayMessage = value; }} },
            { "UsageAnimation", new FieldParser() { Type = FieldType.String, ParserString = ParseUsageAnimation } },
            { "OtherRequirements", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseOtherRequirements } },
            { "Costs", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseCosts } },
            { "NumResultItems", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawNumResultItems = value; }} },
            { "UsageAnimationEnd", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { UsageAnimationEnd = value; }} },
            { "ResetTimeInSeconds", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawResetTimeInSeconds = value; }} },
            { "DyeColor", new FieldParser() { Type = FieldType.String, ParserString = ParseDyeColor } },
            { "RewardSkill", new FieldParser() { Type = FieldType.String, ParserString = ParseRewardSkill } },
            { "RewardSkillXp", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRewardSkillXp = value; }} },
            { "RewardSkillXpFirstTime", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRewardSkillXpFirstTime = value; }} },
            { "SharesResetTimerWith", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawSharesResetTimerWith = value; }} },
            { "ItemMenuLabel", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { ItemMenuLabel = value; }} },
            { "ItemMenuKeywordReq", new FieldParser() { Type = FieldType.String, ParserString = ParseItemMenuKeywordReq } },
            { "IsItemMenuKeywordReqSufficient", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawIsItemMenuKeywordReqSufficient = value; }} },
            { "ItemMenuCategory", new FieldParser() { Type = FieldType.String, ParserString = ParseItemMenuCategory } },
            { "ItemMenuCategoryLevel", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawItemMenuCategoryLevel = value; }} },
        }; } }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
            ErrorInfo.LookForIconId(Description);
        }

        private void ParseIconId(int value, ParseErrorInfo ErrorInfo)
        {
            if (value > 0)
            {
                RawIconId = value;
                ErrorInfo.AddIconId(value);

                PgJsonObjects.Skill.UpdateAnySkillIcon(Skill, RawIconId);
                PgJsonObjects.Skill.UpdateAnySkillIcon(RewardSkill, RawIconId);
            }
            else
                RawIconId = null;
        }

        private void ParseIngredients(JsonObject RawIngredients, ParseErrorInfo ErrorInfo)
        {
            RecipeItem ParsedIngredient;
            JsonObjectParser<RecipeItem>.InitAsSubitem("Ingredients", RawIngredients, out ParsedIngredient, ErrorInfo);

            if (ParsedIngredient != null)
                if (ParsedIngredient.AttuneToCrafter)
                    ErrorInfo.AddInvalidObjectFormat("Recipe Ingredients");
                else
                    IngredientList.Add(ParsedIngredient);
        }

        private void ParseResultItems(JsonObject RawResultItems, ParseErrorInfo ErrorInfo)
        {
            RecipeItem ParsedResultItem;
            JsonObjectParser<RecipeItem>.InitAsSubitem("ResultItems", RawResultItems, out ParsedResultItem, ErrorInfo);
            
            if (ParsedResultItem != null)
            {
                ResultItemList.Add(ParsedResultItem);
                EmptyResultItemList = false;
            }
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSkill, out ParsedSkill, ErrorInfo);
            Skill = ParsedSkill;

            PgJsonObjects.Skill.UpdateAnySkillIcon(Skill, this.RawIconId);
        }

        private bool ParseResultEffects(string RawResultEffect, ParseErrorInfo ErrorInfo)
        {
            RecipeResultEffect NewResultEffect;
            if (ParseResultEffect(RawResultEffect, ErrorInfo, out NewResultEffect))
            {
                ResultEffectList.Add(NewResultEffect);
                return true;
            }
            else
                return false;
        }

        private void ParseSortSkill(string RawSortSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSortSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSortSkill, out ParsedSortSkill, ErrorInfo);
            SortSkill = ParsedSortSkill;
        }

        private void ParseKeywords(string RawKeywords, ParseErrorInfo ErrorInfo)
        {
            RecipeKeyword ParsedKeyword;
            if (StringToEnumConversion<RecipeKeyword>.TryParse(RawKeywords, out ParsedKeyword, ErrorInfo))
                KeywordList.Add(ParsedKeyword);
        }

        private void ParseActionLabel(string RawActionLabel, ParseErrorInfo ErrorInfo)
        {
            RecipeAction ParsedActionLabel;
            StringToEnumConversion<RecipeAction>.TryParse(RawActionLabel, TextMaps.RecipeActionStringMap, out ParsedActionLabel, ErrorInfo);
            ActionLabel = ParsedActionLabel;
        }

        private void ParseUsageAnimation(string RawUsageAnimation, ParseErrorInfo ErrorInfo)
        {
            RecipeUsageAnimation ConvertedRecipeUsageAnimation;
            StringToEnumConversion<RecipeUsageAnimation>.TryParse(RawUsageAnimation, out ConvertedRecipeUsageAnimation, ErrorInfo);
            UsageAnimation = ConvertedRecipeUsageAnimation;
        }

        public void ParseOtherRequirements(JsonObject RawOtherRequirements, ParseErrorInfo ErrorInfo)
        {
            AbilityRequirement ParsedOtherRequirement;
            JsonObjectParser<AbilityRequirement>.InitAsSubitem("OtherRequirements", RawOtherRequirements, out ParsedOtherRequirement, ErrorInfo);

            AbilityRequirement ConvertedAbilityRequirement = ParsedOtherRequirement.ToSpecificAbilityRequirement(ErrorInfo);
            if (ConvertedAbilityRequirement != null)
                OtherRequirementList.Add(ConvertedAbilityRequirement);
        }

        private void ParseCosts(JsonObject RawCosts, ParseErrorInfo ErrorInfo)
        {
            RecipeCost ParsedCost;
            JsonObjectParser<RecipeCost>.InitAsSubitem("Costs", RawCosts, out ParsedCost, ErrorInfo);
            if (ParsedCost != null)
                CostList.Add(ParsedCost);
        }

        private void ParseDyeColor(string RawDyeColor, ParseErrorInfo ErrorInfo)
        {
            uint NewColor;
            if (InvariantCulture.TryParseColor(RawDyeColor, out NewColor))
                DyeColor = NewColor;
            else
                ErrorInfo.AddInvalidString("Item DyeColor", RawDyeColor);
        }

        private void ParseRewardSkill(string RawRewardSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedRewardSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawRewardSkill, out ParsedRewardSkill, ErrorInfo);
            RewardSkill = ParsedRewardSkill;

            PgJsonObjects.Skill.UpdateAnySkillIcon(RewardSkill, this.RawIconId);
        }

        private void ParseItemMenuKeywordReq(string RawItemMenuKeywordReq, ParseErrorInfo ErrorInfo)
        {
            ItemKeyword ParsedItemKeyword;
            StringToEnumConversion<ItemKeyword>.TryParse(RawItemMenuKeywordReq, out ParsedItemKeyword, ErrorInfo);
            RecipeItemKeyword = ParsedItemKeyword;
        }

        private void ParseItemMenuCategory(string RawItemMenuCategory, ParseErrorInfo ErrorInfo)
        {
            if (RawItemMenuCategory == "TSysExtract")
                this.RawItemMenuCategory = "Extract";

            else if (RawItemMenuCategory == "TSysDistill")
                this.RawItemMenuCategory = "Distill";
        }

        private bool ParseResultEffect(string RawEffect, ParseErrorInfo ErrorInfo, out RecipeResultEffect NewResultEffect)
        {
            NewResultEffect = new RecipeResultEffect();

            RecipeEffect ConvertedRecipeEffect;
            if (StringToEnumConversion<RecipeEffect>.TryParse(RawEffect, TextMaps.RecipeEffectStringMap, out ConvertedRecipeEffect, null))
            {
                NewResultEffect.Effect = ConvertedRecipeEffect;
                return true;
            }

            //if (ParseDecomposeEffect(RawEffect, ErrorInfo, ref NewResultEffect))
            //    return true;

            if (ParseExtractEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseRepairEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseCraftEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseEnhanceEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseAddItemPower(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseBrewItem(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseAdjustRecipeReuseTime(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            ErrorInfo.AddMissingEnum("RecipeEffect", RawEffect);
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
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("ActionLabel", StringToEnumConversion<RecipeAction>.ToString(ActionLabel, TextMaps.RecipeActionStringMap, RecipeAction.Internal_None));

            Generator.AddString("Description", Description);

            if (DyeColor.HasValue)
            {
                string AsString = InvariantCulture.ColorToString(DyeColor.Value);
                Generator.AddString("DyeColor", AsString);
            }

            Generator.AddInteger("IconId", RawIconId);

            if (IngredientList.Count > 0)
            {
                Generator.OpenArray("Ingredients");

                foreach (RecipeItem Ingredient in IngredientList)
                    Ingredient.GenerateObjectContent(Generator);

                Generator.CloseArray();
            }
            else if (EmptyIngredientList)
                Generator.AddEmptyArray("Ingredients");

            Generator.AddString("InternalName", InternalName);

            if (KeywordList.Count > 0)
            {
                Generator.OpenArray("Keywords");

                foreach (RecipeKeyword Keyword in KeywordList)
                    Generator.AddString(null, Keyword.ToString());

                Generator.CloseArray();
            }

            Generator.AddString("Name", Name);

            if (OtherRequirementList.Count > 1)
            {
                Generator.OpenArray("OtherRequirements");

                foreach (AbilityRequirement Item in OtherRequirementList)
                    Item.GenerateObjectContent(Generator);

                Generator.CloseArray();
            }
            else if (OtherRequirementList.Count > 0)
            {
                AbilityRequirement Item = OtherRequirementList[0];
                Item.GenerateObjectContent(Generator);
            }

            if (ResultEffectList.Count > 0)
            {
                Generator.OpenArray("ResultEffects");

                foreach (RecipeResultEffect ResultEffect in ResultEffectList)
                    GenerateResultEffectContent(Generator, ResultEffect);

                Generator.CloseArray();
            }

            Generator.AddInteger("NumResultItems", RawNumResultItems);

            if (ResultItemList.Count > 0)
            {
                Generator.OpenArray("ResultItems");

                foreach (RecipeItem Item in ResultItemList)
                    Item.GenerateObjectContent(Generator);

                Generator.CloseArray();
            }
            else if (EmptyResultItemList)
                Generator.AddEmptyArray("ResultItems");

            if (Skill != PowerSkill.Internal_None)
                Generator.AddString("Skill", Skill.ToString());

            Generator.AddInteger("SkillLevelReq", RawSkillLevelReq);

            if (SortSkill != PowerSkill.Internal_None)
                Generator.AddString("SortSkill", SortSkill.ToString());

            if (UsageAnimation != RecipeUsageAnimation.Internal_None)
                Generator.AddString("UsageAnimation", UsageAnimation.ToString());

            Generator.AddInteger("UsageDelay", RawUsageDelay);
            Generator.AddString("UsageDelayMessage", UsageDelayMessage);

            Generator.CloseObject();
        }

        public void GenerateResultEffectContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            string Content;

            switch (ResultEffect.Effect)
            {
                default:
                    Content = StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap);
                    break;

                /*case RecipeEffect.DecomposeItemByTSysLevels:
                    Content = GenerateDecomposeContent(Generator, ResultEffect);
                    break;*/

                case RecipeEffect.ExtractTSysPower:
                    Content = GenerateExtractContent(Generator, ResultEffect);
                    break;

                case RecipeEffect.RepairItemDurability:
                    Content = GenerateRepairContent(Generator, ResultEffect);
                    break;

                case RecipeEffect.TSysCraftedEquipment:
                    Content = GenerateCraftContent(Generator, ResultEffect);
                    break;

                case RecipeEffect.CraftingEnhanceItem:
                    Content = GenerateEnhanceContent(Generator, ResultEffect);
                    break;
            }

            Generator.AddString(null, Content);
        }

        /*public string GenerateDecomposeContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + "(" + ResultEffect.Material + "," + ResultEffect.Skill + ")";
        }*/

        public string GenerateExtractContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + "(" + ResultEffect.ExtractedAugment + "," + ResultEffect.Skill + "," + ResultEffect.MinLevel + "," + ResultEffect .MaxLevel + ")";
        }

        public string GenerateRepairContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + "(" + Tools.FloatToString(ResultEffect.RepairMinEfficiency, ResultEffect.RepairMinEfficiencyFormat) + "," + Tools.FloatToString(ResultEffect.RepairMaxEfficiency, ResultEffect.RepairMaxEfficiencyFormat) + "," + ResultEffect.RepairCooldown + "," + ResultEffect.MinLevel + "," + ResultEffect.MaxLevel + ")";
        }

        public string GenerateCraftContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            string CraftedItem = ResultEffect.Boost.ToString();

            if (ResultEffect.BoostLevel != 0)
                CraftedItem += ResultEffect.BoostLevel.ToString();
            if (ResultEffect.IsCamouflaged)
                CraftedItem += "C";

            if (ResultEffect.AdditionalEnchantments != null)
                CraftedItem += "," + ResultEffect.AdditionalEnchantments.Value.ToString();

            if (ResultEffect.BoostedAnimal != Appearance.Internal_None)
                CraftedItem += "," + ResultEffect.BoostedAnimal;

            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + "(" + CraftedItem + ")";
        }

        public string GenerateEnhanceContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + ResultEffect.Enhancement.ToString() + "(" + InvariantCulture.SingleToString(ResultEffect.AddedQuantity) + "," + ResultEffect.ConsumedEnhancementPoints + ")";
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
                    if (ConnectedSkill != null)
                        AddWithFieldSeparator(ref Result, ConnectedSkill.Name);
                    foreach (RecipeResultEffect Item in ResultEffectList)
                        AddWithFieldSeparator(ref Result, Item.CombinedEffect);
                    if (ConnectedSortSkill != null)
                        AddWithFieldSeparator(ref Result, ConnectedSortSkill.Name);
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
                    if (ConnectedRewardSkill != null)
                        AddWithFieldSeparator(ref Result, ConnectedRewardSkill.Name);
                    if (SharesResetTimerWith != null)
                        AddWithFieldSeparator(ref Result, SharesResetTimerWith.Name);
                    AddWithFieldSeparator(ref Result, ItemMenuLabel);
                    if (RecipeItemKeyword != ItemKeyword.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[RecipeItemKeyword]);
                    AddWithFieldSeparator(ref Result, RawItemMenuCategory);

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

            if (Skill != PowerSkill.Internal_None && Skill != PowerSkill.AnySkill && Skill != PowerSkill.Unknown)
                ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);

            if (SortSkill != PowerSkill.Internal_None && SortSkill != PowerSkill.AnySkill && SortSkill != PowerSkill.Unknown)
                ConnectedSortSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, SortSkill, ConnectedSortSkill, ref IsSortSkillParsed, ref IsConnected, this);

            if (RewardSkill != PowerSkill.Internal_None && RewardSkill != PowerSkill.AnySkill && RewardSkill != PowerSkill.Unknown)
                ConnectedRewardSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RewardSkill, ConnectedRewardSkill, ref IsRewardSkillParsed, ref IsConnected, this);

            foreach (AbilityRequirement Item in OtherRequirementList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            foreach (RecipeCost Item in CostList)
                Item.Connect(ErrorInfo, this, AllTables);

            if (RawSharesResetTimerWith != null)
                SharesResetTimerWith = PgJsonObjects.Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawSharesResetTimerWith, SharesResetTimerWith, ref IsSharesResetTimerWithParsed, ref IsConnected, this);

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

        public static List<Recipe> ConnectByKeyword(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> RecipeTable, RecipeKeyword Keyword, List<Recipe> RecipeList, ref bool IsRawRecipeParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawRecipeParsed)
                return RecipeList;

            IsRawRecipeParsed = true;

            if (Keyword == RecipeKeyword.Internal_None)
                return RecipeList;

            RecipeList = new List<Recipe>();
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
    }
}
