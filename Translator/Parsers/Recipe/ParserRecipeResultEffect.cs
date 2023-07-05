namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserRecipeResultEffect : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<RecipeResultEffectType, VariadicObjectHandler> HandlerTable = new Dictionary<RecipeResultEffectType, VariadicObjectHandler>()
    {
        { RecipeResultEffectType.Special, FinishItemSpecial },
        { RecipeResultEffectType.AddItemTSysPower, FinishItemAddItemTSysPower },
        { RecipeResultEffectType.ExtractTSysPower, FinishItemExtractTSysPower },
        { RecipeResultEffectType.RepairItemDurability, FinishItemRepairItemDurability },
        { RecipeResultEffectType.CraftSimpleTSysItem, FinishItemCraftSimpleTSysItem },
        { RecipeResultEffectType.ConsumeItemUses, FinishItemConsumeItemUses },
        { RecipeResultEffectType.TSysCraftedEquipment, FinishItemTSysCraftedEquipment },
        { RecipeResultEffectType.CraftingEnhanceItem, FinishItemCraftingEnhanceItem },
        { RecipeResultEffectType.GiveTSysItem, FinishItemGiveTSysItem },
        { RecipeResultEffectType.CreateMiningSurvey, FinishItemCreateMiningSurvey },
        { RecipeResultEffectType.CreateGeologySurvey, FinishItemCreateGeologySurvey },
        { RecipeResultEffectType.Tiered, FinishItemTiered },
        { RecipeResultEffectType.PermanentlyRaiseMaxTempestEnergy, FinishItemPermanentlyRaiseMaxTempestEnergy },
        { RecipeResultEffectType.SpawnPremonition, FinishItemSpawnPremonition },
        { RecipeResultEffectType.BrewItem, FinishItemBrewItem },
        { RecipeResultEffectType.PolymorphRabbitPermanent, FinishItemPolymorphRabbitPermanent },
        { RecipeResultEffectType.AddItemTSysPowerWax, FinishItemAddItemTSysPowerWax },
        { RecipeResultEffectType.DeltaCurFairyEnergy, FinishItemDeltaCurFairyEnergy },
        { RecipeResultEffectType.Teleport, FinishItemTeleport },
        { RecipeResultEffectType.AdjustRecipeReuseTime, FinishItemAdjustRecipeReuseTime },
        { RecipeResultEffectType.CraftingResetItem, FinishItemCraftingResetItem },
        { RecipeResultEffectType.SendItemToSaddlebag, FinishItemSendItemToSaddlebag },
        { RecipeResultEffectType.TransmogItemAppearance, FinishItemTransmogItemAppearance },
    };

    private static Dictionary<RecipeResultEffectType, List<string>> KnownFieldTable = new Dictionary<RecipeResultEffectType, List<string>>()
    {
        { RecipeResultEffectType.Special, new List<string>() { "Type", "Effect" } },
        { RecipeResultEffectType.AddItemTSysPower, new List<string>() { "Type", "Slot", "Tier" } },
        { RecipeResultEffectType.ExtractTSysPower, new List<string>() { "Type", "Augment", "Skill", "MinLevel", "MaxLevel" } },
        { RecipeResultEffectType.RepairItemDurability, new List<string>() { "Type", "RepairMinEfficiency", "RepairMaxEfficiency", "RepairCooldown", "MinLevel", "MaxLevel" } },
        { RecipeResultEffectType.CraftSimpleTSysItem, new List<string>() { "Type", "Item" } },
        { RecipeResultEffectType.ConsumeItemUses, new List<string>() { "Type", "Keyword", "ConsumedUses" } },
        { RecipeResultEffectType.TSysCraftedEquipment, new List<string>() { "Type", "Boost", "IsCamouflaged", "BoostLevel", "AdditionalEnchantments", "BoostedAnimal" } },
        { RecipeResultEffectType.CraftingEnhanceItem, new List<string>() { "Type", "Enhancement", "AddedQuantity", "ConsumedEnhancementPoints" } },
        { RecipeResultEffectType.GiveTSysItem, new List<string>() { "Type", "Item" } },
        { RecipeResultEffectType.CreateMiningSurvey, new List<string>() { "Type", "Effect", "Item" } },
        { RecipeResultEffectType.CreateGeologySurvey, new List<string>() { "Type", "Effect", "Item" } },
        { RecipeResultEffectType.Tiered, new List<string>() { "Type", "Keyword", "Tier" } },
        { RecipeResultEffectType.PermanentlyRaiseMaxTempestEnergy, new List<string>() { "Type", "Delta" } },
        { RecipeResultEffectType.SpawnPremonition, new List<string>() { "Type", "DurationInSeconds" } },
        { RecipeResultEffectType.BrewItem, new List<string>() { "Type", "BrewLine", "BrewStrength", "BrewParts", "BrewResults" } },
        { RecipeResultEffectType.PolymorphRabbitPermanent, new List<string>() { "Type", "Color" } },
        { RecipeResultEffectType.AddItemTSysPowerWax, new List<string>() { "Type", "PowerWaxType", "PowerLevel", "MaxHitCount" } },
        { RecipeResultEffectType.DeltaCurFairyEnergy, new List<string>() { "Type", "Delta" } },
        { RecipeResultEffectType.Teleport, new List<string>() { "Type", "AreaName", "Other" } },
        { RecipeResultEffectType.AdjustRecipeReuseTime, new List<string>() { "Type", "AdjustedReuseTime", "MoonPhase" } },
        { RecipeResultEffectType.CraftingResetItem, new List<string>() { "Type" } },
        { RecipeResultEffectType.SendItemToSaddlebag, new List<string>() { "Type" } },
        { RecipeResultEffectType.TransmogItemAppearance, new List<string>() { "Type" } },
    };

    private static Dictionary<RecipeResultEffectType, List<string>> HandledTable = new Dictionary<RecipeResultEffectType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("Type"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Quest objective is missing a Type qualifier");

        object TypeValue = contentTable["Type"];

        if (!(TypeValue is string AsTypeString))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        if (!StringToEnumConversion<RecipeResultEffectType>.TryParse(AsTypeString, out RecipeResultEffectType resultEffectType))
            return false;

        if (!HandlerTable.ContainsKey(resultEffectType))
            return Program.ReportFailure(parsedFile, parsedKey, $"Objective {AsTypeString} has no handler");

        Debug.Assert(KnownFieldTable.ContainsKey(resultEffectType));

        VariadicObjectHandler Handler = HandlerTable[resultEffectType];
        List<string> KnownFieldList = KnownFieldTable[resultEffectType];
        List<string> UsedFieldList = new List<string>();

        if (!Handler(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
            return false;

        if (!HandledTable.ContainsKey(resultEffectType))
            HandledTable.Add(resultEffectType, new List<string>());

        List<string> ReportedFieldList = HandledTable[resultEffectType];
        foreach (string FieldName in UsedFieldList)
            if (!ReportedFieldList.Contains(FieldName))
                ReportedFieldList.Add(FieldName);

        return true;
    }

    public static bool FinalizeParsing()
    {
        return Finalizer<RecipeResultEffectType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemSpecial(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultRecipeEffect NewItem = new PgRecipeResultRecipeEffect();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Effect":
                        Result = StringToEnumConversion<RecipeEffect>.SetEnum((RecipeEffect valueEnum) => NewItem.Effect = valueEnum, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemAddItemTSysPower(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultAddShamanicPower NewItem = new PgRecipeResultAddShamanicPower();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Slot":
                        Result = StringToEnumConversion<ShamanicSlotPower>.SetEnum((ShamanicSlotPower valueEnum) => NewItem.Slot = valueEnum, Value);
                        break;
                    case "Tier":
                        Result = SetIntProperty((int valueInt) => NewItem.RawTier = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemExtractTSysPower(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultExtractAugment NewItem = new PgRecipeResultExtractAugment();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Augment":
                        Result = StringToEnumConversion<Augment>.SetEnum((Augment valueEnum) => NewItem.Augment = valueEnum, Value);
                        break;
                    case "Skill":
                        Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value);
                        break;
                    case "MinLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMinLevel = valueInt, Value);
                        break;
                    case "MaxLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMaxLevel = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemRepairItemDurability(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultRepairItemDurability NewItem = new PgRecipeResultRepairItemDurability();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "RepairMinEfficiency":
                        Result = SetFloatProperty((float valueFloat) => NewItem.RawRepairMinEfficiency = valueFloat, Value);
                        break;
                    case "RepairMaxEfficiency":
                        Result = SetFloatProperty((float valueFloat) => NewItem.RawRepairMaxEfficiency = valueFloat, Value);
                        break;
                    case "RepairCooldown":
                        Result = Inserter<PgQuestTime>.SetItemProperty((PgQuestTime valueQuestTime) => NewItem.RawRepairCooldown = valueQuestTime.ToTime(), Value);
                        break;
                    case "MinLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMinLevel = valueInt, Value);
                        break;
                    case "MaxLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMaxLevel = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemCraftSimpleTSysItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultCraftSimpleTSysItem NewItem = new PgRecipeResultCraftSimpleTSysItem();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Item":
                        Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = PgObject.GetItemKey(valueItem), Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemConsumeItemUses(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultConsumeItemUses NewItem = new PgRecipeResultConsumeItemUses();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Keyword":
                        Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                        break;
                    case "ConsumedUses":
                        Result = SetIntProperty((int valueInt) => NewItem.RawConsumedUse = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemTSysCraftedEquipment(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultCraftedEquipment NewItem = new PgRecipeResultCraftedEquipment();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Boost":
                        Result = StringToEnumConversion<CraftedBoost>.SetEnum((CraftedBoost valueEnum) => NewItem.Boost = valueEnum, Value);
                        break;
                    case "IsCamouflaged":
                        Result = SetBoolProperty((bool valueBool) => NewItem.RawIsCamouflaged = valueBool, Value);
                        break;
                    case "BoostLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawBoostLevel = valueInt, Value);
                        break;
                    case "AdditionalEnchantments":
                        Result = SetIntProperty((int valueInt) => NewItem.RawAdditionalEnchantments = valueInt, Value);
                        break;
                    case "BoostedAnimal":
                        Result = StringToEnumConversion<Appearance>.SetEnum((Appearance valueEnum) => NewItem.BoostedAnimal = valueEnum, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemCraftingEnhanceItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultEnhancedItem NewItem = new PgRecipeResultEnhancedItem();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Enhancement":
                        Result = StringToEnumConversion<EnhancementEffect>.SetEnum((EnhancementEffect valueEnum) => NewItem.Enhancement = valueEnum, Value);
                        break;
                    case "AddedQuantity":
                        Result = SetFloatProperty((float valueFloat) => NewItem.RawAddedQuantity = valueFloat, Value);
                        break;
                    case "ConsumedEnhancementPoints":
                        Result = SetIntProperty((int valueInt) => NewItem.RawConsumedEnhancementPoints = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemGiveTSysItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultGiveTSysItem NewItem = new PgRecipeResultGiveTSysItem();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Item":
                        Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = PgObject.GetItemKey(valueItem), Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemCreateMiningSurvey(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultCreateMiningSurvey NewItem = new PgRecipeResultCreateMiningSurvey();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Effect":
                        break;
                    case "Item":
                        Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = PgObject.GetItemKey(valueItem), Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemCreateGeologySurvey(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultCreateGeologySurvey NewItem = new PgRecipeResultCreateGeologySurvey();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Effect":
                        break;
                    case "Item":
                        Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = PgObject.GetItemKey(valueItem), Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemTiered(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultEffectWithTier NewItem = new PgRecipeResultEffectWithTier();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Keyword":
                        Result = StringToEnumConversion<EffectKeyword>.SetEnum((EffectKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                        break;
                    case "Tier":
                        Result = SetIntProperty((int valueInt) => NewItem.RawTier = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemPermanentlyRaiseMaxTempestEnergy(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultRaiseMaxTempestRaiseEnergy NewItem = new PgRecipeResultRaiseMaxTempestRaiseEnergy();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Delta":
                        Result = SetIntProperty((int valueInt) => NewItem.RawRaiseEnergy = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemSpawnPremonition(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultSpawnPremonition NewItem = new PgRecipeResultSpawnPremonition();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "DurationInSeconds":
                        Result = SetIntProperty((int valueInt) => NewItem.Duration = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemBrewItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultBrewItem NewItem = new PgRecipeResultBrewItem();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "BrewLine":
                        Result = SetIntProperty((int valueInt) => NewItem.RawBrewLine = valueInt, Value);
                        break;
                    case "BrewStrength":
                        Result = SetIntProperty((int valueInt) => NewItem.RawBrewStrength = valueInt, Value);
                        break;
                    case "BrewParts":
                        Result = StringToEnumConversion<RecipeItemKey>.TryParseList(Value, NewItem.BrewPartList);
                        break;
                    case "BrewResults":
                        Result = StringToEnumConversion<RecipeResultKey>.TryParseList(Value, NewItem.BrewResultList);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemPolymorphRabbitPermanent(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultPolymorph NewItem = new PgRecipeResultPolymorph();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Color":
                        Result = SetColorProperty((uint valueColor) => NewItem.RawColor = valueColor, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemAddItemTSysPowerWax(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultWaxItem NewItem = new PgRecipeResultWaxItem();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "PowerWaxType":
                        Result = StringToEnumConversion<PowerWaxType>.SetEnum((PowerWaxType valueEnum) => NewItem.PowerWaxType = valueEnum, Value);
                        break;
                    case "PowerLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawPowerLevel = valueInt, Value);
                        break;
                    case "MaxHitCount":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMaxHitCount = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemDeltaCurFairyEnergy(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultDeltaFairyEnergy NewItem = new PgRecipeResultDeltaFairyEnergy();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Delta":
                        Result = SetIntProperty((int valueInt) => NewItem.RawDelta = -valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemTeleport(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultTeleport NewItem = new PgRecipeResultTeleport();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "AreaName":
                        Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => NewItem.Area = valueEnum, Value);
                        break;
                    case "Other":
                        Result = SetStringProperty((string valueString) => NewItem.Other = valueString, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemAdjustRecipeReuseTime(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultAdjustRecipeReuseTime NewItem = new PgRecipeResultAdjustRecipeReuseTime();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "AdjustedReuseTime":
                        Result = Inserter<PgQuestTime>.SetItemProperty((PgQuestTime valueQuestTime) => NewItem.RawAdjustedReuseTime = valueQuestTime.ToTime(), Value);
                        break;
                    case "MoonPhase":
                        Result = StringToEnumConversion<MoonPhases>.SetEnum((MoonPhases valueEnum) => NewItem.MoonPhase = valueEnum, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemCraftingResetItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultCraftingResetItem NewItem = new PgRecipeResultCraftingResetItem();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemSendItemToSaddlebag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultSendItemToSaddlebag NewItem = new PgRecipeResultSendItemToSaddlebag();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemTransmogItemAppearance(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgRecipeResultTransmogItemAppearance NewItem = new PgRecipeResultTransmogItemAppearance();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }
}
