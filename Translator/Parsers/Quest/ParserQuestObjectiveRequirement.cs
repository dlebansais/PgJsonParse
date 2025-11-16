namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserQuestObjectiveRequirement : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<QuestObjectiveRequirementType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestObjectiveRequirementType, VariadicObjectHandler>()
    {
        { QuestObjectiveRequirementType.TimeOfDay, FinishItemTimeOfDay },
        { QuestObjectiveRequirementType.HasEffectKeyword, FinishItemHasEffectKeyword },
        { QuestObjectiveRequirementType.Appearance, FinishItemAppearance },
        { QuestObjectiveRequirementType.AreaEventOff, FinishItemAreaEventOff },
        { QuestObjectiveRequirementType.ActiveCombatSkill, FinishItemActiveCombatSkill },
        { QuestObjectiveRequirementType.PetCount, FinishItemPetCount },
        { QuestObjectiveRequirementType.EntityPhysicalState, FinishEntityPhysicalState },
        { QuestObjectiveRequirementType.EquipmentSlotEmpty, FinishEquipmentSlotEmpty },
        { QuestObjectiveRequirementType.HangOutCompleted, FinishHangOutCompleted },
        { QuestObjectiveRequirementType.UseAbility, FinishItemUseAbility },
        { QuestObjectiveRequirementType.MinSkillLevel, FinishItemMinSkillLevel },
        { QuestObjectiveRequirementType.HasMountInStable, FinishItemHasMountInStable },
        //{ QuestObjectiveRequirementType.InCombatWithElite, FinishItemInCombatWithElite },
        //{ QuestObjectiveRequirementType.MonsterTargetLevel, FinishItemMonsterTargetLevel },
        //{ QuestObjectiveRequirementType.FullMoon, FinishItemFullMoon },
    };

    private static Dictionary<QuestObjectiveRequirementType, List<string>> KnownFieldTable = new Dictionary<QuestObjectiveRequirementType, List<string>>()
    {
        { QuestObjectiveRequirementType.TimeOfDay, new List<string>() { "T", "MinHour", "MaxHour" } },
        { QuestObjectiveRequirementType.HasEffectKeyword, new List<string>() { "T", "Keyword" } },
        { QuestObjectiveRequirementType.Appearance, new List<string>() { "T", "Appearance" } },
        { QuestObjectiveRequirementType.AreaEventOff, new List<string>() { "T", "Daytime", "AreaName" } },
        { QuestObjectiveRequirementType.ActiveCombatSkill, new List<string>() { "T", "Skill" } },
        { QuestObjectiveRequirementType.PetCount, new List<string>() { "T", "MinCount", "MaxCount", "PetTypeTag" } },
        { QuestObjectiveRequirementType.EntityPhysicalState, new List<string>() { "T", "AllowedStates" } },
        { QuestObjectiveRequirementType.EquipmentSlotEmpty, new List<string>() { "T", "Slot" } },
        { QuestObjectiveRequirementType.HangOutCompleted, new List<string>() { "T", "HangOut" } },
        { QuestObjectiveRequirementType.UseAbility, new List<string>() { "T", "AbilityKeyword" } },
        { QuestObjectiveRequirementType.MinSkillLevel, new List<string>() { "T", "Level", "Skill" } },
        { QuestObjectiveRequirementType.HasMountInStable, new List<string>() { "T", "MinimumMountsNeeded" } },
        //{ QuestObjectiveRequirementType.InCombatWithElite, new List<string>() { "T", "MinLevel" } },
        //{ QuestObjectiveRequirementType.MonsterTargetLevel, new List<string>() { "T", "MinLevel" } },
        //{ QuestObjectiveRequirementType.FullMoon, new List<string>() { "T" } },
    };

    private static Dictionary<QuestObjectiveRequirementType, List<string>> HandledTable = new Dictionary<QuestObjectiveRequirementType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("T"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Quest objective requirement is missing a Type qualifier");

        object TypeValue = contentTable["T"];

        if (!(TypeValue is string AsTypeString))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        if (!StringToEnumConversion<QuestObjectiveRequirementType>.TryParse(AsTypeString, out QuestObjectiveRequirementType requirementType))
            return false;

        if (!HandlerTable.ContainsKey(requirementType))
            return Program.ReportFailure(parsedFile, parsedKey, $"Objective requirement {requirementType} has no handler");

        Debug.Assert(KnownFieldTable.ContainsKey(requirementType));

        VariadicObjectHandler Handler = HandlerTable[requirementType];
        List<string> KnownFieldList = KnownFieldTable[requirementType];
        List<string> UsedFieldList = new List<string>();

        if (!Handler(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
            return false;

        if (!HandledTable.ContainsKey(requirementType))
            HandledTable.Add(requirementType, new List<string>());

        List<string> ReportedFieldList = HandledTable[requirementType];
        foreach (string FieldName in UsedFieldList)
            if (!ReportedFieldList.Contains(FieldName))
                ReportedFieldList.Add(FieldName);

        return true;
    }

    public static bool FinalizeParsing()
    {
        return Finalizer<QuestObjectiveRequirementType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemTimeOfDay(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementTimeOfDay NewItem = new PgQuestObjectiveRequirementTimeOfDay();

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
                    case "T":
                        break;
                    case "MinHour":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMinHour = valueInt, Value);
                        break;
                    case "MaxHour":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMaxHour = valueInt, Value);
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
            if (!NewItem.RawMinHour.HasValue || !NewItem.RawMaxHour.HasValue)
                Result = Program.ReportFailure(parsedFile, parsedKey, "Missing Min/Max hour");

            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemHasEffectKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementHasEffectKeyword NewItem = new PgQuestObjectiveRequirementHasEffectKeyword();

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
                    case "T":
                        break;
                    case "Keyword":
                        Result = StringToEnumConversion<EffectKeyword>.SetEnum((EffectKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
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

    private static bool FinishItemAppearance(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementAppearance NewItem = new PgQuestObjectiveRequirementAppearance();

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
                    case "T":
                        break;
                    case "Appearance":
                        Result = StringToEnumConversion<Appearance>.SetEnum((Appearance valueEnum) => NewItem.Appearance = valueEnum, Value);
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

    private static bool FinishItemAreaEventOff(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementAreaEventOff NewItem = new PgQuestObjectiveRequirementAreaEventOff();

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
                    case "T":
                        break;
                    case "Daytime":
                        Result = SetBoolProperty((bool valueBool) => NewItem.RawDaytime = valueBool, Value);
                        break;
                    case "AreaName":
                        Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => NewItem.AreaName = valueEnum, Value);
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

    private static bool FinishItemActiveCombatSkill(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementActiveCombatSkill NewItem = new PgQuestObjectiveRequirementActiveCombatSkill();

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
                    case "T":
                        break;
                    case "Skill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
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

    private static bool FinishItemPetCount(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementPetCount NewItem = new PgQuestObjectiveRequirementPetCount();

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
                    case "T":
                        break;
                    case "MinCount":
                        Result = SetIntProperty((int valueInt) => NewItem.MinCount = valueInt, Value);
                        break;
                    case "MaxCount":
                        Result = SetIntProperty((int valueInt) => NewItem.MaxCount = valueInt, Value);
                        break;
                    case "PetTypeTag":
                        Result = StringToEnumConversion<RecipeKeyword>.SetEnum((RecipeKeyword valueEnum) => NewItem.PetTypeTag = valueEnum, Value);
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

    private static bool FinishEntityPhysicalState(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementEntityPhysicalState NewItem = new PgQuestObjectiveRequirementEntityPhysicalState();

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
                    case "T":
                        break;
                    case "AllowedStates":
                        Result = StringToEnumConversion<AllowedState>.SetEnum((AllowedState valueEnum) => NewItem.AllowedState = valueEnum, Value);
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

    private static bool FinishEquipmentSlotEmpty(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementEquipmentSlotEmpty NewItem = new PgQuestObjectiveRequirementEquipmentSlotEmpty();

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
                    case "T":
                        break;
                    case "Slot":
                        Result = StringToEnumConversion<ItemSlot>.SetEnum((ItemSlot valueEnum) => NewItem.Slot = valueEnum, Value);
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

    private static bool FinishHangOutCompleted(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementHangOutCompleted NewItem = new PgQuestObjectiveRequirementHangOutCompleted();

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
                    case "T":
                        break;
                    case "HangOut":
                        Result = StringToEnumConversion<HangOut>.SetEnum((HangOut valueEnum) => NewItem.HangOut = valueEnum, Value);
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

    private static bool FinishItemUseAbility(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementUseAbility NewItem = new PgQuestObjectiveRequirementUseAbility();

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
                    case "T":
                        break;
                    case "AbilityKeyword":
                        Result = StringToEnumConversion<AbilityKeyword>.SetEnum((AbilityKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
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

    private static bool FinishItemMinSkillLevel(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementMinSkillLevel NewItem = new PgQuestObjectiveRequirementMinSkillLevel();

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
                    case "T":
                        break;
                    case "Level":
                        Result = SetIntProperty((int valueInt) => NewItem.RawLevel = valueInt, Value);
                        break;
                    case "Skill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
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

    private static bool FinishItemHasMountInStable(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementHasMountInStable NewItem = new PgQuestObjectiveRequirementHasMountInStable();

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
                    case "T":
                        break;
                    case "MinimumMountsNeeded":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMinimumMountsNeeded = valueInt, Value);
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

    /*
    private static bool FinishItemInCombatWithElite(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementInCombatWithElite NewItem = new PgQuestObjectiveRequirementInCombatWithElite();

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
                    case "T":
                        break;
                    case "MinLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMinLevel = valueInt, Value);
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

    private static bool FinishItemMonsterTargetLevel(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementMonsterTargetLevel NewItem = new PgQuestObjectiveRequirementMonsterTargetLevel();

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
                    case "T":
                        break;
                    case "MinLevel":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMinLevel = valueInt, Value);
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

    private static bool FinishItemFullMoon(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestObjectiveRequirementFullMoon NewItem = new PgQuestObjectiveRequirementFullMoon();

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
                    case "T":
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
    */
}
