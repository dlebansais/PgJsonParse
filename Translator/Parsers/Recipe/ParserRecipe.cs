namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;
    using System;

    public class ParserRecipe : Parser
    {
        public override object CreateItem()
        {
            return new PgRecipe();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgRecipe AsPgRecipe))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgRecipe, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgRecipe item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Description":
                        Result = SetStringProperty((string valueString) => item.Description = Tools.CleanedUpString(valueString), Value);
                        break;
                    case "IconId":
                        Result = SetIntProperty((int valueInt) => item.RawIconId = valueInt, Value);
                        break;
                    case "Ingredients":
                        Result = Inserter<PgRecipeItem>.AddKeylessArray(item.IngredientList, Value);
                        break;
                    case "InternalName":
                        Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                        break;
                    case "Name":
                        Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                        break;
                    case "ResultItems":
                        Result = Inserter<PgRecipeItem>.AddKeylessArray(item.ResultItemList, Value);
                        break;
                    case "Skill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => item.Skill = valueSkill, Value, parsedFile, parsedKey);
                        break;
                    case "SkillLevelReq":
                        Result = SetIntProperty((int valueInt) => item.RawSkillLevelReq = valueInt, Value);
                        break;
                    case "ResultEffects":
                        Result = ParseResultEffects(item, Value, parsedFile, parsedKey);
                        break;
                    case "SortSkill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => item.SortSkill = valueSkill, Value, parsedFile, parsedKey);
                        break;
                    case "Keywords":
                        Result = StringToEnumConversion<RecipeKeyword>.TryParseList(Value, item.KeywordList);
                        break;
                    case "ActionLabel":
                        Result = StringToEnumConversion<RecipeAction>.SetEnum((RecipeAction valueEnum) => item.ActionLabel= valueEnum, Value);
                        break;
                    case "UsageDelay":
                        Result = SetFloatProperty((float valueFloat) => item.RawUsageDelay = valueFloat, Value);
                        break;
                    case "UsageDelayMessage":
                        Result = SetStringProperty((string valueString) => item.UsageDelayMessage = valueString, Value);
                        break;
                    case "UsageAnimation":
                        Result = StringToEnumConversion<RecipeUsageAnimation>.SetEnum((RecipeUsageAnimation valueEnum) => item.UsageAnimation = valueEnum, Value);
                        break;
                    case "OtherRequirements":
                        Result = Inserter<PgAbilityRequirement>.AddKeylessArray(item.OtherRequirementList, Value);
                        break;
                    case "Costs":
                        Result = Inserter<PgRecipeCost>.AddKeylessArray(item.CostList, Value);
                        break;
                    case "NumResultItems":
                        Result = SetIntProperty((int valueInt) => item.RawNumResultItems = valueInt, Value);
                        break;
                    case "UsageAnimationEnd":
                        Result = SetStringProperty((string valueString) => item.UsageAnimationEnd = valueString, Value);
                        break;
                    case "ResetTimeInSeconds":
                        Result = SetIntProperty((int valueInt) => item.RawResetTime = TimeSpan.FromSeconds(valueInt), Value);
                        break;
                    case "DyeColor":
                        Result = SetColorProperty((uint valueColor) => item.DyeColor = valueColor, Value);
                        break;
                    case "RewardSkill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => item.RewardSkill = valueSkill, Value, parsedFile, parsedKey);
                        break;
                    case "RewardSkillXp":
                        Result = SetIntProperty((int valueInt) => item.RawRewardSkillXp = valueInt, Value);
                        break;
                    case "RewardSkillXpDropOffLevel":
                        Result = SetIntProperty((int valueInt) => item.RawRewardSkillXpDropOffLevel = valueInt, Value);
                        break;
                    case "RewardSkillXpDropOffPct":
                        Result = SetFloatProperty((float valueFloat) => item.RawRewardSkillXpDropOffPct = valueFloat, Value);
                        break;
                    case "RewardSkillXpDropOffRate":
                        Result = SetIntProperty((int valueInt) => item.RawRewardSkillXpDropOffRate = valueInt, Value);
                        break;
                    case "RewardSkillXpFirstTime":
                        Result = SetIntProperty((int valueInt) => item.RawRewardSkillXpFirstTime = valueInt, Value);
                        break;
                    case "SharesResetTimerWith":
                        Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => item.SharesResetTimerWith = valueRecipe, Value);
                        break;
                    case "ItemMenuLabel":
                        Result = SetStringProperty((string valueString) => item.ItemMenuLabel = valueString, Value);
                        break;
                    case "ItemMenuKeywordReq":
                        Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => item.RecipeItemKeyword = valueEnum, Value);
                        break;
                    case "IsItemMenuKeywordReqSufficient":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsItemMenuKeywordReqSufficient = valueBool, Value);
                        break;
                    case "ItemMenuCategory":
                        Result = ParseItemMenuCategory(item, Value, parsedFile, parsedKey);
                        break;
                    case "ItemMenuCategoryLevel":
                        Result = SetIntProperty((int valueInt) => item.RawItemMenuCategoryLevel = valueInt, Value);
                        break;
                    case "PrereqRecipe":
                        Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => item.PrereqRecipe = valueRecipe, Value);
                        break;
                    case "ValidationIngredientKeywords":
                        Result = StringToEnumConversion<ItemKeyword>.TryParseList(Value, item.ValidationIngredientKeywordList);
                        break;
                    case "ProtoResultItems":
                        Result = Inserter<PgRecipeItem>.AddKeylessArray(item.ProtoResultItemList, Value);
                        break;
                    case "RewardAllowBonusXp":
                        Result = SetBoolProperty((bool valueBool) => item.RawRewardAllowBonusXp = valueBool, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            if (Result)
            {
                if (!item.RawRewardSkillXp.HasValue || !item.RawRewardSkillXpFirstTime.HasValue)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing reward XP");

                if (item.RawRewardSkillXpDropOffLevel.HasValue)
                {
                    if (!item.RawRewardSkillXpDropOffPct.HasValue)
                        return Program.ReportFailure(parsedFile, parsedKey, "Missing drop off percent");
                    if (!item.RawRewardSkillXpDropOffRate.HasValue)
                        return Program.ReportFailure(parsedFile, parsedKey, "Missing drop off rate");
                }

                foreach (PgRecipeResultEffect Effect in item.ResultEffectList)
                    if (Effect is PgRecipeResultBrewItem AsBrewItem)
                    {
                        List<RecipeItemKey> BrewPartList = new List<RecipeItemKey>(AsBrewItem.BrewPartList);

                        List<RecipeItemKey> IngredientList = new List<RecipeItemKey>();
                        foreach (PgRecipeItem Ingredient in item.IngredientList)
                            if (Ingredient.ItemKeyList.Count == 1)
                                IngredientList.Add(Ingredient.ItemKeyList[0]);

                        if (BrewPartList.Count != IngredientList.Count)
                            return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent ingredient list");

                        foreach (RecipeItemKey IngredientKey in IngredientList)
                        {
                            if (!BrewPartList.Contains(IngredientKey))
                                return Program.ReportFailure(parsedFile, parsedKey, "Missing ingredient in brew result");

                            BrewPartList.Remove(IngredientKey);
                        }
                        break;
                    }

                if (item.RawNumResultItems.HasValue && item.RawNumResultItems.Value != 1)
                    return Program.ReportFailure(parsedFile, parsedKey, "Unexpected number of items");
            }

            return Result;
        }

        private bool ParseResultEffects(PgRecipe item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ArrayString))
                return Program.ReportFailure($"Value '{value}' was expected to be a list of objects");

            foreach (object Item in ArrayString)
            {
                if (!(Item is string effectString))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a string");

                if (!ParseResultEffect(effectString, parsedFile, parsedKey, out PgRecipeResultEffect RecipeResult))
                    return false;

                ParsingContext.AddSuplementaryObject(RecipeResult);
                item.ResultEffectList.Add(RecipeResult);
            }

            return true;
        }

        private bool ParseResultEffect(string effectString, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            if (StringToEnumConversion<RecipeEffect>.TryParse(effectString, out RecipeEffect ParsedRecipeEffect, ErrorControl.IgnoreIfNotFound))
            {
                recipeResult = new PgRecipeResultRecipeEffect() { Effect = ParsedRecipeEffect };
                return true;
            }

            int ParameterStartIndex = effectString.IndexOf('(');
            int ParameterEndIndex = effectString.LastIndexOf(')');
            string EffectName = (ParameterStartIndex < 0 || ParameterEndIndex < effectString.Length - 1) ? effectString : effectString.Substring(0, ParameterStartIndex);
            string EffectParameter = (ParameterStartIndex < 0 || ParameterEndIndex < effectString.Length - 1) ? string.Empty : effectString.Substring(ParameterStartIndex + 1, effectString.Length - 2 - ParameterStartIndex);

            bool Result;
            recipeResult = null;

            switch (EffectName)
            {
                case "ExtractTSysPower":
                    Result = ParseExtractTSysPower(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "RepairItemDurability":
                    Result = ParseRepairItemDurability(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "TSysCraftedEquipment":
                    Result = ParseTSysCraftedEquipment(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemPockets":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.Pockets, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemDarknessMod":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.DarknessMod, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemFireMod":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.FireMod, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemColdMod":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.ColdMod, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemElectricityMod":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.ElectricityMod, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemPsychicMod":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.PsychicMod, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemNatureMod":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.NatureMod, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "CraftingEnhanceItemArmor":
                    Result = ParseCraftingEnhanceItem(EnhancementEffect.Armor, EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "AddItemTSysPower":
                    Result = ParseAddItemTSysPower(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "BrewItem":
                    Result = ParseBrewItem(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "AddItemTSysPowerWax":
                    Result = ParseAddItemTSysPowerWax(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "AdjustRecipeReuseTime":
                    Result = ParseAdjustRecipeReuseTime(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "GiveTSysItem":
                    Result = ParseGiveTSysItem(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "ConsumeItemUses":
                    Result = ParseConsumeItemUses(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "DeltaCurFairyEnergy":
                    Result = ParseDeltaCurFairyEnergy(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                case "Teleport":
                    Result = ParseTeleport(EffectParameter, parsedFile, parsedKey, out recipeResult);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect '{effectString}'");
                    break;
            }

            return Result;
        }

        private bool ParseExtractTSysPower(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 4)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect ExtractTSysPower '{effectParameter}'");

            string AugmentString = Splitted[0];
            string DecomposeSkillString = Splitted[1];
            string MinLevelString = Splitted[2];
            string MaxLevelString = Splitted[3];

            if (!StringToEnumConversion<Augment>.TryParse(AugmentString, out Augment Augment))
                return false;

            PgSkill ParsedSkill = null;
            if (!ParserSkill.Parse((PgSkill valueSkill) => ParsedSkill = valueSkill, DecomposeSkillString, parsedFile, parsedKey))
                return false;

            if (!int.TryParse(MinLevelString, out int MinLevel))
                return Program.ReportFailure($"Value '{MinLevelString}' was expected to be an int");

            if (!int.TryParse(MaxLevelString, out int MaxLevel))
                return Program.ReportFailure($"Value '{MaxLevelString}' was expected to be an int");

            PgRecipeResultExtractAugment RecipeResultEffect = new PgRecipeResultExtractAugment();
            RecipeResultEffect.Augment = Augment;
            RecipeResultEffect.Skill = ParsedSkill;
            RecipeResultEffect.RawMinLevel = MinLevel;
            RecipeResultEffect.RawMaxLevel = MaxLevel;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseRepairItemDurability(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 5)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect RepairItemDurability '{effectParameter}'");

            string RepairMinEfficiencyString = Splitted[0];
            string RepairMaxEfficiencyString = Splitted[1];
            string RepairCooldownString = Splitted[2];
            string MinLevelString = Splitted[3];
            string MaxLevelString = Splitted[4];

            if (!Tools.TryParseSingle(RepairMinEfficiencyString, out float RepairMinEfficiency))
                return Program.ReportFailure($"Value '{RepairMinEfficiencyString}' was expected to be a float");

            if (!Tools.TryParseSingle(RepairMaxEfficiencyString, out float RepairMaxEfficiency))
                return Program.ReportFailure($"Value '{RepairMaxEfficiencyString}' was expected to be a float");

            if (!int.TryParse(RepairCooldownString, out int RepairCooldown))
                return Program.ReportFailure($"Value '{RepairCooldownString}' was expected to be an int");

            if (!int.TryParse(MinLevelString, out int MinLevel))
                return Program.ReportFailure($"Value '{MinLevelString}' was expected to be an int");

            if (!int.TryParse(MaxLevelString, out int MaxLevel))
                return Program.ReportFailure($"Value '{MaxLevelString}' was expected to be an int");

            PgRecipeResultRepairItemDurability RecipeResultEffect = new PgRecipeResultRepairItemDurability();
            RecipeResultEffect.RawRepairMinEfficiency = RepairMinEfficiency * 100;
            RecipeResultEffect.RawRepairMaxEfficiency = RepairMaxEfficiency * 100;
            RecipeResultEffect.RawRepairCooldown = TimeSpan.FromHours(RepairCooldown);
            RecipeResultEffect.RawMinLevel = MinLevel;
            RecipeResultEffect.RawMaxLevel = MaxLevel;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseTSysCraftedEquipment(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length == 0)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect TSysCraftedEquipment '{effectParameter}'");

            string CraftedItem = Splitted[0];

            bool IsCamouflaged;
            if (CraftedItem.Length > 0 && CraftedItem[CraftedItem.Length - 1] == 'C')
            {
                IsCamouflaged = true;
                CraftedItem = CraftedItem.Substring(0, CraftedItem.Length - 1);
            }
            else
                IsCamouflaged = false;

            int? RawBoostLevel = null;

            if (CraftedItem.Length > 0)
            {
                int DigitIndex = CraftedItem.Length;
                while (DigitIndex > 0 && char.IsDigit(CraftedItem[DigitIndex - 1]))
                    DigitIndex--;

                if (DigitIndex < CraftedItem.Length)
                {
                    string LevelString = CraftedItem.Substring(DigitIndex, CraftedItem.Length - DigitIndex);

                    if (int.TryParse(LevelString, out int ParsedLevel))
                    {
                        CraftedItem = CraftedItem.Substring(0, DigitIndex);
                        RawBoostLevel = ParsedLevel;
                    }
                }
            }

            if (!StringToEnumConversion<CraftedBoost>.TryParse(CraftedItem, out CraftedBoost Boost))
                return false;

            int? RawAdditionalEnchantments = null;
            Appearance BoostedAnimal = Appearance.Internal_None;

            if (Splitted.Length > 1)
            {
                string AdditionalEnchantmentString = Splitted[1];

                if (!int.TryParse(AdditionalEnchantmentString, out int ValueInt))
                    return Program.ReportFailure($"Value '{AdditionalEnchantmentString}' was expected to be an int");

                RawAdditionalEnchantments = ValueInt;
            }

            if (Splitted.Length > 2)
            {
                string BoostedAnimalString = Splitted[2];

                if (!StringToEnumConversion<Appearance>.TryParse(BoostedAnimalString, out BoostedAnimal))
                    return false;
            }

            PgRecipeResultCraftedEquipment RecipeResultEffect = new PgRecipeResultCraftedEquipment();
            RecipeResultEffect.Boost = Boost;
            RecipeResultEffect.RawIsCamouflaged = IsCamouflaged;
            RecipeResultEffect.RawBoostLevel = RawBoostLevel;
            RecipeResultEffect.RawAdditionalEnchantments = RawAdditionalEnchantments;
            RecipeResultEffect.BoostedAnimal = BoostedAnimal;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseCraftingEnhanceItem(EnhancementEffect enhancement, string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect CraftingEnhanceItem '{effectParameter}'");

            string AddedQuantityString = Splitted[0];
            string ConsumedEnhancementPointString = Splitted[1];

            if (!Tools.TryParseSingle(AddedQuantityString, out float AddedQuantity))
                return Program.ReportFailure($"Value '{AddedQuantityString}' was expected to be a float");

            if (!int.TryParse(ConsumedEnhancementPointString, out int ConsumedEnhancementPoints))
                return Program.ReportFailure($"Value '{ConsumedEnhancementPointString}' was expected to be an int");

            StringToEnumConversion<EnhancementEffect>.SetCustomParsedEnum(enhancement);

            PgRecipeResultEnhancedItem RecipeResultEffect = new PgRecipeResultEnhancedItem();
            RecipeResultEffect.Enhancement = enhancement;
            RecipeResultEffect.RawAddedQuantity = AddedQuantity;
            RecipeResultEffect.RawConsumedEnhancementPoints = ConsumedEnhancementPoints;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseAddItemTSysPower(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect AddItemTSysPower '{effectParameter}'");

            string SlotString = Splitted[0];
            string TierString = Splitted[1];

            if (!StringToEnumConversion<ShamanicSlotPower>.TryParse(SlotString, out ShamanicSlotPower Slot))
                return false;

            if (!int.TryParse(TierString, out int Tier))
                return Program.ReportFailure($"Value '{TierString}' was expected to be an int");

            PgRecipeResultAddShamanicPower RecipeResultEffect = new PgRecipeResultAddShamanicPower();
            RecipeResultEffect.Slot = Slot;
            RecipeResultEffect.RawTier = Tier;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseBrewItem(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 3)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect BrewItem '{effectParameter}'");

            string BrewLineString = Splitted[0];
            string BrewStrengthString = Splitted[1];
            string AllPartString = Splitted[2];

            if (!int.TryParse(BrewLineString, out int BrewLine))
                return Program.ReportFailure($"Value '{BrewLineString}' was expected to be an int");

            if (!int.TryParse(BrewStrengthString, out int BrewStrength))
                return Program.ReportFailure($"Value '{BrewStrengthString}' was expected to be an int");

            string[] PartSplit = AllPartString.Trim().Split('=');

            if (PartSplit.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect BrewItem '{effectParameter}'");

            string[] Parts = PartSplit[0].Trim().Split('+');
            string[] Results = PartSplit[1].Trim().Split('+');

            if (Parts.Length == 0 || Parts[0].Trim().Length == 0 || Results.Length == 0 || Results[0].Trim().Length == 0)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect BrewItem '{effectParameter}'");

            List<RecipeItemKey> BrewPartList = new List<RecipeItemKey>();
            foreach (string RawPart in Parts)
            {
                if (!StringToEnumConversion<RecipeItemKey>.TryParse(RawPart, out RecipeItemKey ParsedPart))
                    return false;

                BrewPartList.Add(ParsedPart);
            }

            List<RecipeResultKey> BrewResultList = new List<RecipeResultKey>();
            foreach (string RawResult in Results)
            {
                if (!StringToEnumConversion<RecipeResultKey>.TryParse(RawResult, out RecipeResultKey ParsedResult))
                    return false;

                BrewResultList.Add(ParsedResult);
            }

            PgRecipeResultBrewItem RecipeResultEffect = new PgRecipeResultBrewItem();
            RecipeResultEffect.RawBrewLine = BrewLine;
            RecipeResultEffect.RawBrewStrength = BrewStrength;
            RecipeResultEffect.BrewPartList.AddRange(BrewPartList);
            RecipeResultEffect.BrewResultList.AddRange(BrewResultList);

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseAddItemTSysPowerWax(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 3)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect AddItemTSysPowerWax '{effectParameter}'");

            string PowerWaxTypeString = Splitted[0];
            string PowerLevelString = Splitted[1];
            string MaxHitCountString = Splitted[2];

            if (!StringToEnumConversion<PowerWaxType>.TryParse(PowerWaxTypeString, out PowerWaxType PowerWaxType))
                return false;

            if (!int.TryParse(PowerLevelString, out int PowerLevel))
                return Program.ReportFailure($"Value '{PowerLevelString}' was expected to be an int");

            if (!int.TryParse(MaxHitCountString, out int MaxHitCount))
                return Program.ReportFailure($"Value '{MaxHitCountString}' was expected to be an int");

            PgRecipeResultWaxItem RecipeResultEffect = new PgRecipeResultWaxItem();
            RecipeResultEffect.PowerWaxType = PowerWaxType;
            RecipeResultEffect.RawPowerLevel = PowerLevel;
            RecipeResultEffect.RawMaxHitCount = MaxHitCount;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseAdjustRecipeReuseTime(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect AdjustRecipeReuseTime '{effectParameter}'");

            string AdjustedReuseTimeString = Splitted[0];
            string MoonPhaseString = Splitted[1];

            if (!int.TryParse(AdjustedReuseTimeString, out int AdjustedReuseTime))
                return Program.ReportFailure($"Value '{AdjustedReuseTimeString}' was expected to be an int");

            if (AdjustedReuseTime >= 0)
                return Program.ReportFailure($"Reuse time should be negative");

            if (!StringToEnumConversion<MoonPhases>.TryParse(MoonPhaseString, out MoonPhases MoonPhase))
                return false;

            PgRecipeResultAdjustRecipeReuseTime RecipeResultEffect = new PgRecipeResultAdjustRecipeReuseTime();
            RecipeResultEffect.RawAdjustedReuseTime = TimeSpan.FromSeconds(-AdjustedReuseTime);
            RecipeResultEffect.MoonPhase = MoonPhase;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseGiveTSysItem(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            PgItem ParsedItem = null;
            if (!Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => ParsedItem = valueItem, effectParameter))
                return false;

            PgRecipeResultGiveTSysItem RecipeResultEffect = new PgRecipeResultGiveTSysItem();
            RecipeResultEffect.Item = ParsedItem;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseConsumeItemUses(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect ConsumeItemUses '{effectParameter}'");

            string KeywordString = Splitted[0];
            string ConsumedUseString = Splitted[1];

            if (!StringToEnumConversion<ItemKeyword>.TryParse(KeywordString, out ItemKeyword Keyword))
                return false;

            if (!int.TryParse(ConsumedUseString, out int ConsumedUse))
                return Program.ReportFailure($"Value '{ConsumedUseString}' was expected to be an int");

            PgRecipeResultConsumeItemUses RecipeResultEffect = new PgRecipeResultConsumeItemUses();
            RecipeResultEffect.Keyword = Keyword;
            RecipeResultEffect.RawConsumedUse = ConsumedUse;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseDeltaCurFairyEnergy(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            if (!int.TryParse(effectParameter, out int Delta))
                return Program.ReportFailure($"Value '{effectParameter}' was expected to be an int");

            if (Delta >= 0)
                return Program.ReportFailure($"Delta should be negative");

            PgRecipeResultDeltaFairyEnergy RecipeResultEffect = new PgRecipeResultDeltaFairyEnergy();
            RecipeResultEffect.RawDelta = -Delta;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseTeleport(string effectParameter, string parsedFile, string parsedKey, out PgRecipeResultEffect recipeResult)
        {
            recipeResult = null;

            string[] Splitted = effectParameter.Split(',');

            if (Splitted.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect Teleport '{effectParameter}'");

            string MapAreaNameString = Splitted[0];
            string OtherString = Splitted[1];

            if (!StringToEnumConversion<MapAreaName>.TryParse(MapAreaNameString, out MapAreaName Area))
                return false;

            if (OtherString == " NewFairySpot")
                OtherString = "New Fairy Spot";
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown recipe result effect Teleport location '{OtherString}'");

            PgRecipeResultTeleport RecipeResultEffect = new PgRecipeResultTeleport();
            RecipeResultEffect.Area = Area;
            RecipeResultEffect.Other = OtherString;

            recipeResult = RecipeResultEffect;
            return true;
        }

        private bool ParseItemMenuCategory(PgRecipe item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueString))
                return Program.ReportFailure($"Value '{value}' was expected to be an int");

            if (ValueString == "TSysExtract")
                item.ItemMenuCategory = "Extract";
            else if (ValueString == "TSysDistill")
                item.ItemMenuCategory = "Distill";
            else
                return Program.ReportFailure($"Unknown menu item category '{ValueString}'");

            return true;
        }

        public static bool UpdateSource()
        {
            Dictionary<string, ParsingContext> SourceParsingTable = ParsingContext.ObjectKeyTable[typeof(PgSource)];
            Dictionary<string, ParsingContext> RecipeParsingTable = ParsingContext.ObjectKeyTable[typeof(PgRecipe)];

            foreach (KeyValuePair<string, ParsingContext> Entry in SourceParsingTable)
            {
                PgSource RecipeSource = (PgSource)Entry.Value.Item;
                string Key = RecipeSource.SourceKey;

                if (Key.StartsWith("recipe_"))
                {
                    if (!RecipeParsingTable.ContainsKey(Key))
                        return Program.ReportFailure($"Source for '{Key}' but no such object");

                    PgRecipe Recipe = (PgRecipe)RecipeParsingTable[Key].Item;
                    Recipe.SourceList.Add(RecipeSource);
                }
            }

            return true;
        }
    }
}
