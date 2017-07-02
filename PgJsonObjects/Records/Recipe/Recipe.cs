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
        public RecipeCost Cost { get; private set; }
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
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }
        public double? PerfectCottonRatio { get; private set; }

        public string CombinedSkill
        {
            get
            {
                if (ConnectedSkill == null)
                    return TextMaps.PowerSkillTextMap[Skill];
                else
                    return ConnectedSkill.Name;
            }
        }

        public string CombinedKeywords
        {
            get
            {
                string Result = "";

                foreach (RecipeKeyword Keyword in KeywordList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += TextMaps.RecipeKeywordTextMap[Keyword];
                }

                return Result;
            }
        }

        public string CombinedSortSkill
        {
            get
            {
                if (ConnectedSortSkill == null)
                    return TextMaps.PowerSkillTextMap[SortSkill];
                else
                    return ConnectedSortSkill.Name;
            }
        }

        public string CombinedRewardSkill
        {
            get
            {
                if (ConnectedRewardSkill == null)
                    return TextMaps.PowerSkillTextMap[RewardSkill];
                else
                    return ConnectedRewardSkill.Name;
            }
        }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Description", ParseFieldDescription },
            { "IconId", ParseFieldIconId },
            { "Ingredients", ParseFieldIngredients },
            { "InternalName", ParseFieldInternalName },
            { "Name", ParseFieldName },
            { "ResultItems", ParseFieldResultItems },
            { "Skill", ParseFieldSkill },
            { "SkillLevelReq", ParseFieldSkillLevelReq },
            { "ResultEffects", ParseFieldResultEffects },
            { "SortSkill", ParseFieldSortSkill },
            { "Keywords", ParseFieldKeywords },
            { "ActionLabel", ParseFieldActionLabel },
            { "UsageDelay", ParseFieldUsageDelay },
            { "UsageDelayMessage", ParseFieldUsageDelayMessage },
            { "UsageAnimation", ParseFieldUsageAnimation },
            { "OtherRequirements", ParseFieldOtherRequirements },
            { "Costs", ParseFieldCosts },
            { "NumResultItems", ParseFieldNumResultItems },
            { "UsageAnimationEnd", ParseFieldUsageAnimationEnd },
            { "ResetTimeInSeconds", ParseFieldResetTimeInSeconds },
            { "DyeColor", ParseFieldDyeColor },
            { "RewardSkill", ParseFieldRewardSkill },
            { "RewardSkillXp", ParseFieldRewardSkillXp },
            { "RewardSkillXpFirstTime", ParseFieldRewardSkillXpFirstTime },
        };

        private static void ParseFieldDescription(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDescription;
            if ((RawDescription = Value as string) != null)
                This.ParseDescription(RawDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Description");
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
            ErrorInfo.LookForIconId(Description);
        }

        private static void ParseFieldIconId(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseIconId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe IconId");
        }

        private void ParseIconId(int RawIconId, ParseErrorInfo ErrorInfo)
        {
            if (RawIconId > 0)
            {
                this.RawIconId = RawIconId;
                ErrorInfo.AddIconId(RawIconId);

                UpdateAnySkillIcon();
            }
            else
                this.RawIconId = null;
        }

        private static void ParseFieldIngredients(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawIngredients;
            if ((RawIngredients = Value as ArrayList) != null)
                This.ParseIngredients(RawIngredients, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Ingredients");
        }

        private void ParseIngredients(ArrayList RawIngredients, ParseErrorInfo ErrorInfo)
        {
            List<RecipeItem> ParsedIngredientList;
            JsonObjectParser<RecipeItem>.InitAsSublist(RawIngredients, out ParsedIngredientList, ErrorInfo);
            IngredientList.AddRange(ParsedIngredientList);
            EmptyIngredientList = (IngredientList.Count == 0);
        }

        private static void ParseFieldInternalName(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawInternalName;
            if ((RawInternalName = Value as string) != null)
                This.ParseInternalName(RawInternalName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe InternalName");
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;
        }

        private static void ParseFieldName(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldResultItems(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawResultItems;
            if ((RawResultItems = Value as ArrayList) != null)
                This.ParseResultItems(RawResultItems, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ResultItems");
        }

        private void ParseResultItems(ArrayList RawResultItems, ParseErrorInfo ErrorInfo)
        {
            List<RecipeItem> ParsedResultItemList;
            JsonObjectParser<RecipeItem>.InitAsSublist(RawResultItems, out ParsedResultItemList, ErrorInfo);
            ResultItemList.AddRange(ParsedResultItemList);
            EmptyResultItemList = (ResultItemList.Count == 0);
        }

        private static void ParseFieldSkill(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkill;
            if ((RawSkill = Value as string) != null)
                This.ParseSkill(RawSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Skill");
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSkill, out ParsedSkill, ErrorInfo);
            Skill = ParsedSkill;

            UpdateAnySkillIcon();
        }

        private static void ParseFieldSkillLevelReq(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseSkillLevelReq((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe SkillLevelReq");
        }

        private void ParseSkillLevelReq(int RawSkillLevelReq, ParseErrorInfo ErrorInfo)
        {
            this.RawSkillLevelReq = RawSkillLevelReq;
        }

        private static void ParseFieldResultEffects(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawResultEffects;
            if ((RawResultEffects = Value as ArrayList) != null)
                This.ParseResultEffects(RawResultEffects, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ResultEffects");
        }

        private void ParseResultEffects(ArrayList RawResultEffects, ParseErrorInfo ErrorInfo)
        {
            foreach (object Item in RawResultEffects)
            {
                string RawEffect;
                if ((RawEffect = Item as string) !=  null)
                {
                    RecipeResultEffect NewResultEffect;
                    if (ParseResultEffect(RawEffect, ErrorInfo, out NewResultEffect))
                        ResultEffectList.Add(NewResultEffect);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Recipe ResultEffects");
            }
        }

        private static void ParseFieldSortSkill(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSortSkill;
            if ((RawSortSkill = Value as string) != null)
                This.ParseSortSkill(RawSortSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe SortSkill");
        }

        private void ParseSortSkill(string RawSortSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSortSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSortSkill, out ParsedSortSkill, ErrorInfo);
            SortSkill = ParsedSortSkill;
        }

        private static void ParseFieldKeywords(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawKeywords;
            if ((RawKeywords = Value as ArrayList) != null)
                This.ParseKeywords(RawKeywords, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Keywords");
        }

        private void ParseKeywords(ArrayList RawKeywords, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<RecipeKeyword>.ParseList(RawKeywords, KeywordList, ErrorInfo);
        }

        private static void ParseFieldActionLabel(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawActionLabel;
            if ((RawActionLabel = Value as string) != null)
                This.ParseActionLabel(RawActionLabel, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ActionLabel");
        }

        private void ParseActionLabel(string RawActionLabel, ParseErrorInfo ErrorInfo)
        {
            RecipeAction ParsedActionLabel;
            StringToEnumConversion<RecipeAction>.TryParse(RawActionLabel, TextMaps.RecipeActionStringMap, out ParsedActionLabel, ErrorInfo);
            ActionLabel = ParsedActionLabel;
        }

        private static void ParseFieldUsageDelay(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseUsageDelay((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageDelay");
        }

        private void ParseUsageDelay(int RawUsageDelay, ParseErrorInfo ErrorInfo)
        {
            this.RawUsageDelay = RawUsageDelay;
        }

        private static void ParseFieldUsageDelayMessage(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUsageDelayMessage;
            if ((RawUsageDelayMessage = Value as string) != null)
                This.ParseUsageDelayMessage(RawUsageDelayMessage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageDelayMessage");
        }

        private void ParseUsageDelayMessage(string RawUsageDelayMessage, ParseErrorInfo ErrorInfo)
        {
            UsageDelayMessage = RawUsageDelayMessage;
        }

        private static void ParseFieldUsageAnimation(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUsageAnimation;
            if ((RawUsageAnimation = Value as string) != null)
                This.ParseUsageAnimation(RawUsageAnimation, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageAnimation");
        }

        private void ParseUsageAnimation(string RawUsageAnimation, ParseErrorInfo ErrorInfo)
        {
            RecipeUsageAnimation ConvertedRecipeUsageAnimation;
            StringToEnumConversion<RecipeUsageAnimation>.TryParse(RawUsageAnimation, out ConvertedRecipeUsageAnimation, ErrorInfo);
            UsageAnimation = ConvertedRecipeUsageAnimation;
        }

        private static void ParseFieldOtherRequirements(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList AsArrayList;
            Dictionary<string, object> AsDictionary;

            if ((AsArrayList = Value as ArrayList) != null)
            {
                foreach (object Item in AsArrayList)
                {
                    if ((AsDictionary = Item as Dictionary<string, object>) != null)
                        This.ParseOtherRequirements(AsDictionary, ErrorInfo);
                    else
                        ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements");
                }
            }

            else if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseOtherRequirements(AsDictionary, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements");
        }

        public void ParseOtherRequirements(Dictionary<string, object> RawOtherRequirements, ParseErrorInfo ErrorInfo)
        {
            AbilityRequirement ParsedOtherRequirement;
            JsonObjectParser<AbilityRequirement>.InitAsSubitem("OtherRequirements", RawOtherRequirements, out ParsedOtherRequirement, ErrorInfo);

            AbilityRequirement ConvertedAbilityRequirement = ParsedOtherRequirement.ToSpecificAbilityRequirement(ErrorInfo);
            OtherRequirementList.Add(ConvertedAbilityRequirement);
        }

        private static void ParseFieldCosts(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawCosts;
            if ((RawCosts = Value as ArrayList) != null)
                This.ParseCosts(RawCosts, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Costs");
        }

        private void ParseCosts(ArrayList RawCosts, ParseErrorInfo ErrorInfo)
        {
            List<RecipeCost> ParsedCostList;
            JsonObjectParser<RecipeCost>.InitAsSublist(RawCosts, out ParsedCostList, ErrorInfo);

            if (ParsedCostList.Count == 0)
                Cost = null;
            else if (ParsedCostList.Count == 1)
                Cost = ParsedCostList[0];
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Costs");
        }

        private static void ParseFieldNumResultItems(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNumResultItems((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe NumResultItems");
        }

        private void ParseNumResultItems(int RawNumResultItems, ParseErrorInfo ErrorInfo)
        {
            this.RawNumResultItems = RawNumResultItems;
        }

        private static void ParseFieldUsageAnimationEnd(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUsageAnimationEnd;
            if ((RawUsageAnimationEnd = Value as string) != null)
                This.ParseUsageAnimationEnd(RawUsageAnimationEnd, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageAnimationEnd");
        }

        private void ParseUsageAnimationEnd(string RawUsageAnimationEnd, ParseErrorInfo ErrorInfo)
        {
            UsageAnimationEnd = RawUsageAnimationEnd;
        }

        private static void ParseFieldResetTimeInSeconds(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseResetTimeInSeconds((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ResetTimeInSeconds");
        }

        private void ParseResetTimeInSeconds(int RawResetTimeInSeconds, ParseErrorInfo ErrorInfo)
        {
            this.RawResetTimeInSeconds = RawResetTimeInSeconds;
        }

        private static void ParseFieldDyeColor(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDyeColor;
            if ((RawDyeColor = Value as string) != null)
                This.ParseDyeColor(RawDyeColor, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe DyeColor");
        }

        private void ParseDyeColor(string RawDyeColor, ParseErrorInfo ErrorInfo)
        {
            uint NewColor;
            if (Tools.TryParseColor(RawDyeColor, out NewColor))
                DyeColor = NewColor;
            else
                ErrorInfo.AddInvalidString("Item DyeColor", RawDyeColor);
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

            if (ParseDecomposeEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

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

            ErrorInfo.AddMissingEnum("RecipeEffect", RawEffect);
            return false;
        }

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

        private bool ParseExtractEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string ExtractPattern = "ExtractTSysPower(";
            if (RawEffect.StartsWith(ExtractPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.ExtractTSysPower;
                string Extracted = RawEffect.Substring(ExtractPattern.Length, RawEffect.Length - ExtractPattern.Length - 1);
                string[] ExtractedSplit = Extracted.Split(',');

                if (ExtractedSplit.Length == 3)
                {
                    Augment ConvertedAugment;
                    DecomposeSkill ConvertedSkill;
                    DecomposeMaterial ConvertedMaterial;
                    if (StringToEnumConversion<Augment>.TryParse(ExtractedSplit[0], out ConvertedAugment, ErrorInfo) &&
                        StringToEnumConversion<DecomposeSkill>.TryParse(ExtractedSplit[1], out ConvertedSkill, ErrorInfo) &&
                        StringToEnumConversion<DecomposeMaterial>.TryParse(ExtractedSplit[2], out ConvertedMaterial, ErrorInfo))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.ExtractedAugment = ConvertedAugment;
                        NewResultEffect.Skill = ConvertedSkill;
                        NewResultEffect.Material = ConvertedMaterial;
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

                if (RepairedSplit.Length == 3)
                {
                    float RepairMinEfficiency;
                    FloatFormat RepairMinEfficiencyFormat;
                    float RepairMaxEfficiency;
                    FloatFormat RepairMaxEfficiencyFormat;
                    int RepairCooldown;
                    if (Tools.TryParseFloat(RepairedSplit[0], out RepairMinEfficiency, out RepairMinEfficiencyFormat) &&
                        Tools.TryParseFloat(RepairedSplit[1], out RepairMaxEfficiency, out RepairMaxEfficiencyFormat) &&
                        int.TryParse(RepairedSplit[2], out RepairCooldown))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.RepairMinEfficiency = RepairMinEfficiency;
                        NewResultEffect.RepairMinEfficiencyFormat = RepairMinEfficiencyFormat;
                        NewResultEffect.RepairMaxEfficiency = RepairMaxEfficiency;
                        NewResultEffect.RepairMaxEfficiencyFormat = RepairMaxEfficiencyFormat;
                        NewResultEffect.RepairCooldown = RepairCooldown;
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

                            if (float.TryParse(EnhanceDataSplit[0], System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out AddedQuantity) &&
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

        private static void ParseFieldRewardSkill(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRewardSkill;
            if ((RawRewardSkill = Value as string) != null)
                This.ParseRewardSkill(RawRewardSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe RewardSkill");
        }

        private void ParseRewardSkill(string RawRewardSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedRewardSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawRewardSkill, out ParsedRewardSkill, ErrorInfo);
            RewardSkill = ParsedRewardSkill;

            UpdateAnySkillIcon();
        }

        private static void ParseFieldRewardSkillXp(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardSkillXp((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe RewardSkillXp");
        }

        private void ParseRewardSkillXp(int RawRewardSkillXp, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardSkillXp = RawRewardSkillXp;
        }

        private static void ParseFieldRewardSkillXpFirstTime(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardSkillXpFirstTime((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe RewardSkillXpFirstTime");
        }

        private void ParseRewardSkillXpFirstTime(int RawRewardSkillXpFirstTime, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardSkillXpFirstTime = RawRewardSkillXpFirstTime;
        }

        private void UpdateAnySkillIcon()
        {
            if (RawIconId.HasValue)
            {
                if (Skill != PowerSkill.Internal_None)
                {
                    if (!PgJsonObjects.Skill.AnyIconTable.ContainsKey(Skill))
                        PgJsonObjects.Skill.AnyIconTable.Add(Skill, RawIconId.Value);
                }

                else if (RewardSkill != PowerSkill.Internal_None)
                {
                    if (!PgJsonObjects.Skill.AnyIconTable.ContainsKey(RewardSkill))
                        PgJsonObjects.Skill.AnyIconTable.Add(RewardSkill, RawIconId.Value);
                }
            }
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
                string AsString = Tools.ColorToString(DyeColor.Value);
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

                case RecipeEffect.DecomposeItemByTSysLevels:
                    Content = GenerateDecomposeContent(Generator, ResultEffect);
                    break;

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

        public string GenerateDecomposeContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + "(" + ResultEffect.Material + "," + ResultEffect.Skill + ")";
        }

        public string GenerateExtractContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + "(" + ResultEffect.ExtractedAugment + "," + ResultEffect.Skill + "," + ResultEffect.Material + ")";
        }

        public string GenerateRepairContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + "(" + Tools.FloatToString(ResultEffect.RepairMinEfficiency, ResultEffect.RepairMinEfficiencyFormat) + "," + Tools.FloatToString(ResultEffect.RepairMaxEfficiency, ResultEffect.RepairMaxEfficiencyFormat) + "," + ResultEffect.RepairCooldown + ")";
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
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, TextMaps.RecipeEffectStringMap) + ResultEffect.Enhancement.ToString() + "(" + ResultEffect.AddedQuantity.ToString(CultureInfo.InvariantCulture.NumberFormat) + "," + ResultEffect.ConsumedEnhancementPoints + ")";
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (RawIconId.HasValue)
                {
                    AddWithFieldSeparator(ref Result, Name);
                    AddWithFieldSeparator(ref Result, Description);
                    foreach (RecipeItem Item in IngredientList)
                        AddWithFieldSeparator(ref Result, Item.CombinedDescription);
                    foreach (RecipeItem Item in ResultItemList)
                        AddWithFieldSeparator(ref Result, Item.CombinedDescription);
                    if (Skill != PowerSkill.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[Skill]);
                    foreach (RecipeResultEffect Item in ResultEffectList)
                        AddWithFieldSeparator(ref Result, Item.CombinedEffect);
                    if (SortSkill != PowerSkill.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[SortSkill]);
                    AddWithFieldSeparator(ref Result, CombinedKeywords);
                    if (ActionLabel != RecipeAction.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.RecipeActionTextMap[ActionLabel]);
                    AddWithFieldSeparator(ref Result, UsageDelayMessage);
                    if (UsageAnimation != RecipeUsageAnimation.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.RecipeUsageAnimationTextMap[UsageAnimation]);
                    foreach (AbilityRequirement Requirement in OtherRequirementList)
                        AddWithFieldSeparator(ref Result, Requirement.TextContent);
                    if (Cost != null)
                        AddWithFieldSeparator(ref Result, Cost.CombinedCost);
                    if (RewardSkill != PowerSkill.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[RewardSkill]);
                }

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            foreach (RecipeItem Item in IngredientList)
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (RecipeItem Item in ResultItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            if (Skill != PowerSkill.Internal_None && Skill != PowerSkill.AnySkill && Skill != PowerSkill.Unknown)
                ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);

            if (SortSkill != PowerSkill.Internal_None && SortSkill != PowerSkill.AnySkill && SortSkill != PowerSkill.Unknown)
                ConnectedSortSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, SortSkill, ConnectedSortSkill, ref IsSortSkillParsed, ref IsConnected, this);

            if (RewardSkill != PowerSkill.Internal_None && RewardSkill != PowerSkill.AnySkill && RewardSkill != PowerSkill.Unknown)
                ConnectedRewardSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RewardSkill, ConnectedRewardSkill, ref IsRewardSkillParsed, ref IsConnected, this);

            foreach (AbilityRequirement Item in OtherRequirementList)
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            if (Cost != null)
                Cost.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            return IsConnected;
        }

        public static bool ConnectTableByInternalName(ParseErrorInfo ErrorInfo, Dictionary<string, Recipe> RecipeTable, List<string> ConnectedList, Dictionary<string, Recipe> ConnectedTable)
        {
            bool Connected = false;

            foreach (string s in ConnectedList)
            {
                bool Found = false;
                foreach (KeyValuePair<string, Recipe> Entry in RecipeTable)
                    if (Entry.Value.InternalName == s)
                    {
                        Found = true;
                        Connected = true;
                        if (ConnectedTable.ContainsKey(s))
                            ErrorInfo.AddDuplicateString("Recipe", s);
                        else
                            ConnectedTable.Add(Entry.Key, Entry.Value);
                        break;
                    }

                if (!Found)
                    ErrorInfo.AddMissingKey(s);
            }

            return Connected;
        }

        public static Recipe ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, Recipe> RecipeTable, string RawRecipeName, Recipe ParsedRecipe, ref bool IsRawRecipeParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawRecipeParsed)
                return ParsedRecipe;

            IsRawRecipeParsed = true;

            if (RawRecipeName == null)
                return null;

            foreach (KeyValuePair<string, Recipe> Entry in RecipeTable)
                if (Entry.Value.InternalName == RawRecipeName)
                {
                    IsConnected = true;
                    Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
                }

            ErrorInfo.AddMissingKey(RawRecipeName);
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
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Recipe"; } }
        #endregion
    }
}
