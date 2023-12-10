namespace Translator;

using System;
using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserItem : Parser
{
    public override object CreateItem()
    {
        return new PgItem();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgItem AsPgItem)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgItem, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgItem item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "BestowRecipes":
                    Result = Inserter<PgRecipe>.AddPgObjectArrayByInternalName<PgRecipe>(item.BestowRecipeList, Value);
                    break;
                case "BestowAbility":
                    Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.BestowAbility_Key = PgObject.GetItemKey(valueAbility), Value);
                    break;
                case "BestowQuest":
                    Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => item.BestowQuest_Key = PgObject.GetItemKey(valueQuest), Value);
                    break;
                case "AllowPrefix":
                    Result = SetBoolProperty((bool valueBool) => item.SetAllowPrefix(valueBool), Value);
                    break;
                case "AllowSuffix":
                    Result = SetBoolProperty((bool valueBool) => item.SetAllowSuffix(valueBool), Value);
                    break;
                case "CraftPoints":
                    Result = SetIntProperty((int valueInt) => item.RawCraftPoints = valueInt, Value);
                    break;
                case "CraftingTargetLevel":
                    Result = SetIntProperty((int valueInt) => item.RawCraftingTargetLevel = valueInt, Value);
                    break;
                case "Description":
                    Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                    break;
                case "DroppedAppearance":
                    Result = Inserter<PgDroppedAppearance>.SetItemProperty((PgDroppedAppearance valueDroppedAppearance) => item.DroppedAppearance = valueDroppedAppearance, Value);
                    break;
                case "EffectDescriptions":
                    Result = Inserter<PgItemEffect>.AddKeylessArray(item.EffectDescriptionList, Value);
                    break;
                case "DyeColor":
                    Result = SetColorProperty((uint valueColor) => item.RawDyeColor = valueColor, Value);
                    break;
                case "EquipAppearance":
                    Result = SetStringProperty((string valueString) => item.EquipAppearance = valueString, Value); // TODO: parse
                    break;
                case "EquipSlot":
                    Result = StringToEnumConversion<ItemSlot>.SetEnum((ItemSlot valueEnum) => item.EquipSlot = valueEnum, Value);
                    break;
                case "FoodDescription":
                    Result = SetStringProperty((string valueString) => item.FoodDescription = valueString, Value); // TODO: parse
                    break;
                case "IconId":
                    Result = SetIconIdProperty((int valueInt) => item.RawIconId = valueInt, Value);
                    break;
                case "InternalName":
                    Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                    break;
                case "IsTemporary":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsTemporary(valueBool), Value);
                    break;
                case "IsCrafted":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsCrafted(valueBool), Value);
                    break;
                case "Keywords":
                    Result = Inserter<PgItemKeywordValues>.AddKeylessArray(item.KeywordValuesList, Value);
                    break;
                case "MacGuffinQuestName":
                    Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => item.MacGuffinQuest_Key = PgObject.GetItemKey(valueQuest), Value);
                    break;
                case "MaxCarryable":
                    Result = SetIntProperty((int valueInt) => item.RawMaxCarryable = valueInt, Value);
                    break;
                case "MaxOnVendor":
                    Result = SetIntProperty((int valueInt) => item.RawMaxOnVendor = valueInt, Value);
                    break;
                case "MaxStackSize":
                    Result = SetIntProperty((int valueInt) => item.RawMaxStackSize = valueInt, Value);
                    break;
                case "Name":
                    Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                    break;
                case "RequiredAppearance":
                    Result = StringToEnumConversion<Appearance>.SetEnum((Appearance valueEnum) => item.RequiredAppearance = valueEnum, Value);
                    break;
                case "SkillRequirements":
                    Result = Inserter<PgItemSkillLink>.SetItemProperty((PgItemSkillLink valueItemSkillLink) => item.SkillRequirementTable = valueItemSkillLink.SkillTable, Value);
                    break;
                case "StockDye":
                    Result = Inserter<PgStockDye>.SetItemProperty((PgStockDye valueStockDye) => item.StockDye = valueStockDye, Value);
                    break;
                case "TSysProfile":
                    Result = Inserter<PgProfile>.SetItemByKey((PgProfile valueProfile) => item.Profile_Key = PgObject.GetItemKey(valueProfile), Value);
                    break;
                case "Value":
                    Result = SetFloatProperty((float valueFloat) => item.RawValue = valueFloat, Value);
                    break;
                case "NumberOfUses":
                    Result = SetIntProperty((int valueInt) => item.RawNumberOfUses = valueInt, Value);
                    break;
                case "DestroyWhenUsedUp":
                    Result = SetBoolProperty((bool valueBool) => item.SetDestroyWhenUsedUp(valueBool), Value);
                    break;
                case "Behaviors":
                    Result = Inserter<PgItemBehavior>.AddKeylessArray(item.BehaviorList, Value);
                    break;
                case "DynamicCraftingSummary":
                    Result = SetStringProperty((string valueString) => item.DynamicCraftingSummary = valueString, Value);
                    break;
                case "IsSkillRequirementsDefaults":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsSkillReqsDefaults(valueBool), Value);
                    break;
                case "BestowTitle":
                    Result = Inserter<PgPlayerTitle>.SetItemByKey((PgPlayerTitle valuePlayerTitle) => item.BestowTitle_Key = PgObject.GetItemKey(valuePlayerTitle), Value.ToString());
                    break;
                case "BestowLoreBook":
                    Result = Inserter<PgLoreBook>.SetItemByKey((PgLoreBook valueLoreBook) => item.BestowLoreBook_Key = PgObject.GetItemKey(valueLoreBook), Value.ToString());
                    break;
                case "Lint_VendorNpc":
                    Result = StringToEnumConversion<WorkOrderSign>.SetEnum((WorkOrderSign valueEnum) => item.LintVendorNpc = valueEnum, Value);
                    break;
                case "MountedAppearance":
                    Result = SetStringProperty((string valueString) => item.MountedAppearance = valueString, Value);
                    break;
                case "AttuneOnPickup":
                    Result = SetBoolProperty((bool valueBool) => item.SetAttuneOnPickup(valueBool), Value);
                    break;
                case "IgnoreAlreadyKnownBestowals":
                    Result = SetBoolProperty((bool valueBool) => item.SetIgnoreAlreadyKnownBestowals(valueBool), Value);
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
            foreach (PgItemKeywordValues KeywordValues in item.KeywordValuesList)
                if (StringToEnumConversion<RecipeItemKey>.TryParse(KeywordValues.Keyword.ToString(), out RecipeItemKey ParsedKey, ErrorControl.IgnoreIfNotFound))
                    item.RecipeItemKeyList.Add(ParsedKey);
        }

        return Result;
    }
}
