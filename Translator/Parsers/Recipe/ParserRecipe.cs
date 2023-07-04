namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserRecipe : Parser
{
    public override object CreateItem()
    {
        return new PgRecipe();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgRecipe AsPgRecipe)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgRecipe, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgRecipe item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
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
                    Result = SetIconIdProperty((int valueInt) => item.RawIconId = valueInt, Value);
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
                    Result = ParserSkill.Parse((PgSkill valueSkill) => item.Skill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
                    break;
                case "SkillLevelRequirement":
                    Result = SetIntProperty((int valueInt) => item.RawSkillLevelReq = valueInt, Value);
                    break;
                case "ResultEffects":
                    Result = Inserter<PgRecipeResultEffect>.AddKeylessArray(item.ResultEffectList, Value);
                    break;
                case "SortSkill":
                    Result = ParserSkill.Parse((PgSkill valueSkill) => item.SortSkill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
                    break;
                case "Keywords":
                    Result = StringToEnumConversion<RecipeKeyword>.TryParseList(Value, item.KeywordList);
                    break;
                case "ActionLabel":
                    Result = SetStringProperty((string valueString) => item.ActionLabel = valueString, Value);
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
                case "NumberOfResultItems":
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
                    Result = ParserSkill.Parse((PgSkill valueSkill) => item.RewardSkill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
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
                    Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => item.SharesResetTimerWith_Key = PgObject.GetItemKey(valueRecipe), Value);
                    break;
                case "ItemMenuLabel":
                    Result = SetStringProperty((string valueString) => item.ItemMenuLabel = valueString, Value);
                    break;
                case "ItemMenuKeywordRequirement":
                    Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => item.RecipeItemKeyword = valueEnum, Value);
                    break;
                case "IsItemMenuKeywordRequirementSufficient":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsItemMenuKeywordReqSufficient(valueBool), Value);
                    break;
                case "ItemMenuCategory":
                    Result = SetStringProperty((string valueString) => item.ItemMenuCategory = valueString, Value);
                    break;
                case "ItemMenuCategoryLevel":
                    Result = SetIntProperty((int valueInt) => item.RawItemMenuCategoryLevel = valueInt, Value);
                    break;
                case "PrereqRecipe":
                    Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => item.PrereqRecipe_Key = PgObject.GetItemKey(valueRecipe), Value);
                    break;
                case "ValidationIngredientKeywords":
                    Result = StringToEnumConversion<ItemKeyword>.TryParseList(Value, item.ValidationIngredientKeywordList);
                    break;
                case "ProtoResultItems":
                    Result = Inserter<PgRecipeItem>.AddKeylessArray(item.ProtoResultItemList, Value);
                    break;
                case "RewardAllowBonusXp":
                    Result = SetBoolProperty((bool valueBool) => item.SetRewardAllowBonusXp(valueBool), Value);
                    break;
                case "RequiredAttributeNonZero":
                    Result = Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => item.RequiredAttributeNonZero_Key = PgObject.GetItemKey(valueAttribute), Value);
                    break;
                case "LoopParticle":
                    Result = Inserter<PgRecipeParticle>.SetItemProperty((PgRecipeParticle valueRecipeParticle) => item.LoopParticle = valueRecipeParticle, Value);
                    break;
                case "Particle":
                    Result = Inserter<PgRecipeParticle>.SetItemProperty((PgRecipeParticle valueRecipeParticle) => item.Particle = valueRecipeParticle, Value);
                    break;
                case "MaxUses":
                    Result = SetIntProperty((int valueInt) => item.RawMaxUses = valueInt, Value);
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

    public static bool UpdateSource()
    {
        Dictionary<string, ParsingContext> RecipeSourceParsingTable = ParsingContext.ObjectKeyTable[typeof(PgSourceEntriesRecipe)];
        Dictionary<string, ParsingContext> RecipeParsingTable = ParsingContext.ObjectKeyTable[typeof(PgRecipe)];

        foreach (KeyValuePair<string, ParsingContext> Entry in RecipeSourceParsingTable)
        {
            PgSourceEntries RecipeSource = (PgSourceEntries)Entry.Value.Item;
            string Key = RecipeSource.Key;

            if (!RecipeParsingTable.ContainsKey(Key))
                return Program.ReportFailure($"Source for '{Key}' but no such object");

            PgRecipe Recipe = (PgRecipe)RecipeParsingTable[Key].Item;
            foreach (PgSource SourceEntry in RecipeSource.EntryList)
                Recipe.SourceList.Add(SourceEntry);
        }

        return true;
    }
}
