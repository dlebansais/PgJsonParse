﻿namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserQuestRequirement : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<QuestRequirementType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestRequirementType, VariadicObjectHandler>()
    {
        { QuestRequirementType.MinFavorLevel, FinishItemMinFavorLevel },
        { QuestRequirementType.Race, FinishItemRace },
        { QuestRequirementType.QuestCompleted, FinishItemQuestCompleted },
        { QuestRequirementType.IsWarden, FinishItemIsWarden },
        { QuestRequirementType.AreaEventOn, FinishItemAreaEventOn },
        { QuestRequirementType.MinSkillLevel, FinishItemMinSkillLevel },
        { QuestRequirementType.MoonPhase, FinishItemMoonPhase },
        { QuestRequirementType.HangOutCompleted, FinishItemHangOutCompleted },
        { QuestRequirementType.InteractionFlagSet, FinishItemInteractionFlagSet },
        { QuestRequirementType.Or, FinishItemOr },
        { QuestRequirementType.GuildQuestCompleted, FinishItemGuildQuestCompleted },
        { QuestRequirementType.HasEffectKeyword, FinishItemHasEffectKeyword },
        { QuestRequirementType.RuntimeBehaviorRuleSet, FinishItemRuntimeBehaviorRuleSet },
        { QuestRequirementType.IsLongtimeAnimal, FinishItemIsLongtimeAnimal },
        { QuestRequirementType.InteractionFlagUnset, FinishItemInteractionFlagUnset },
        { QuestRequirementType.MinFavor, FinishItemMinFavor },
        { QuestRequirementType.ScriptAtomicMatches, FinishItemScriptAtomicMatches },
        { QuestRequirementType.AreaEventOff, FinishItemAreaEventOff },
        { QuestRequirementType.QuestCompletedRecently, FinishItemQuestCompletedRecently },
        { QuestRequirementType.GeneralShape, FinishItemGeneralShape },
        { QuestRequirementType.Appearance, FinishItemAppearance },
        { QuestRequirementType.AttributeMatchesScriptAtomic, FinishItemAttributeMatchesScriptAtomic },
        { QuestRequirementType.AccountFlagUnset, FinishItemAccountFlagUnset },
        { QuestRequirementType.DayOfWeek, FinishItemDayOfWeek },
        { QuestRequirementType.IsVampire, FinishItemIsVampire },
        { QuestRequirementType.MinCombatSkillLevel, FinishItemMinCombatSkillLevel },
    };

    private static Dictionary<QuestRequirementType, List<string>> KnownFieldTable = new Dictionary<QuestRequirementType, List<string>>()
    {
        { QuestRequirementType.MinFavorLevel, new List<string>() { "T", "Npc", "FavorLevel" } },
        { QuestRequirementType.Race, new List<string>() { "T", "AllowedRace", "DisallowedRace" } },
        { QuestRequirementType.QuestCompleted, new List<string>() { "T", "Quest" } },
        { QuestRequirementType.IsWarden, new List<string>() { "T" } },
        { QuestRequirementType.AreaEventOn, new List<string>() { "T", "AreaEvent", "AreaName", "EventQuest", "EventSkill" } },
        { QuestRequirementType.MinSkillLevel, new List<string>() { "T", "Level", "Skill" } },
        { QuestRequirementType.MoonPhase, new List<string>() { "T", "MoonPhase" } },
        { QuestRequirementType.HangOutCompleted, new List<string>() { "T", "HangOut" } },
        { QuestRequirementType.InteractionFlagSet, new List<string>() { "T", "InteractionFlag" } },
        { QuestRequirementType.Or, new List<string>() { "T", "List" } },
        { QuestRequirementType.GuildQuestCompleted, new List<string>() { "T", "Quest" } },
        { QuestRequirementType.HasEffectKeyword, new List<string>() { "T", "Keyword" } },
        { QuestRequirementType.RuntimeBehaviorRuleSet, new List<string>() { "T", "Rule" } },
        { QuestRequirementType.IsLongtimeAnimal, new List<string>() { "T" } },
        { QuestRequirementType.InteractionFlagUnset, new List<string>() { "T", "InteractionFlag" } },
        { QuestRequirementType.MinFavor, new List<string>() { "T", "Npc", "MinFavor" } },
        { QuestRequirementType.ScriptAtomicMatches, new List<string>() { "T", "AtomicVar", "Value" } },
        { QuestRequirementType.AreaEventOff, new List<string>() { "T", "EventQuest" } },
        { QuestRequirementType.QuestCompletedRecently, new List<string>() { "T", "Quest" } },
        { QuestRequirementType.GeneralShape, new List<string>() { "T", "Shape" } },
        { QuestRequirementType.Appearance, new List<string>() { "T", "Appearance" } },
        { QuestRequirementType.AttributeMatchesScriptAtomic, new List<string>() { "T", "Attribute", "ScriptAtomicInt" } },
        { QuestRequirementType.AccountFlagUnset, new List<string>() { "T", "AccountFlag" } },
        { QuestRequirementType.DayOfWeek, new List<string>() { "T", "DaysAllowed" } },
        { QuestRequirementType.IsVampire, new List<string>() { "T" } },
        { QuestRequirementType.MinCombatSkillLevel, new List<string>() { "T", "Level" } },
    };

    private static Dictionary<QuestRequirementType, List<string>> HandledTable = new Dictionary<QuestRequirementType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("T"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Quest requirement is missing a Type qualifier");

        object TypeValue = contentTable["T"];

        if (!(TypeValue is string AsTypeString))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        if (!StringToEnumConversion<QuestRequirementType>.TryParse(AsTypeString, out QuestRequirementType requirementType))
            return false;

        if (!HandlerTable.ContainsKey(requirementType))
            return Program.ReportFailure(parsedFile, parsedKey, $"Requirement {requirementType} has no handler");

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
        return Finalizer<QuestRequirementType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemMinFavorLevel(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementMinFavorLevel NewItem = new PgQuestRequirementMinFavorLevel();

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
                    case "Npc":
                        Result = Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => NewItem.FavorNpc = npcLocation, Value, parsedFile, parsedKey);
                        break;
                    case "FavorLevel":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.FavorLevel = valueEnum, Value);
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

    private static bool FinishItemRace(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        bool Result = true;
        Race AllowedRace = Race.Internal_None;
        Race DisallowedRace = Race.Internal_None;

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
                    case "AllowedRace":
                        Result = StringToEnumConversion<Race>.SetEnum((Race valueEnum) => AllowedRace = valueEnum, Value);
                        break;
                    case "DisallowedRace":
                        Result = StringToEnumConversion<Race>.SetEnum((Race valueEnum) => DisallowedRace = valueEnum, Value);
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
            if (AllowedRace != Race.Internal_None && DisallowedRace != Race.Internal_None)
                return Program.ReportFailure(parsedFile, parsedKey, "Unexpected allowed and disallowed race");
            else if (AllowedRace == Race.Internal_None && DisallowedRace == Race.Internal_None)
                return Program.ReportFailure(parsedFile, parsedKey, "Missing race");
            else if (AllowedRace != Race.Internal_None)
            {
                Debug.Assert(DisallowedRace == Race.Internal_None);

                PgQuestRequirementAllowedRace NewItem = new PgQuestRequirementAllowedRace() { Race = AllowedRace };
                item = NewItem;
                return true;
            }
            else
            {
                Debug.Assert(AllowedRace == Race.Internal_None);

                PgQuestRequirementDisallowedRace NewItem = new PgQuestRequirementDisallowedRace() { Race = DisallowedRace };
                item = NewItem;
                return true;
            }
        }
        else
            return false;
    }

    private static bool FinishItemQuestCompleted(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementQuestCompleted NewItem = new PgQuestRequirementQuestCompleted();

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
                    case "Quest":
                        Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.QuestList.Add(valueQuest.Key), Value);
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

    private static bool FinishItemIsWarden(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementIsWarden NewItem = new PgQuestRequirementIsWarden();

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

    private static bool FinishItemAreaEventOn(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementAreaEventOn NewItem = new PgQuestRequirementAreaEventOn();

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
                    case "AreaName":
                        Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => NewItem.AreaName = valueEnum, Value);
                        break;
                    case "EventSkill":
                        Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value);
                        break;
                    case "AreaEvent":
                        break;
                    case "EventQuest":
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
        PgQuestRequirementMinSkillLevel NewItem = new PgQuestRequirementMinSkillLevel();

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
                        Result = SetIntProperty((int valueInt) => NewItem.RawSkillLevel = valueInt, Value);
                        break;
                    case "Skill":
                        Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value);
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

    private static bool FinishItemMoonPhase(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementMoonPhase NewItem = new PgQuestRequirementMoonPhase();

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

    private static bool FinishItemHangOutCompleted(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementHangOutCompleted NewItem = new PgQuestRequirementHangOutCompleted();

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

    private static bool FinishItemInteractionFlagSet(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementInteractionFlagSet NewItem = new PgQuestRequirementInteractionFlagSet();

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
                    case "InteractionFlag":
                        Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.InteractionFlag = valueEnum, Value);
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

    private static bool FinishItemOr(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementOr NewItem = new PgQuestRequirementOr();

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
                    case "List":
                        Result = Inserter<PgQuestRequirement>.AddKeylessArray(NewItem.OrList, Value);
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

    private static bool FinishItemGuildQuestCompleted(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementGuildQuestCompleted NewItem = new PgQuestRequirementGuildQuestCompleted();

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
                    case "Quest":
                        Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.Quest_Key = PgObject.GetItemKey(valueQuest), Value);
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

    private static bool FinishItemHasEffectKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementHasEffectKeyword NewItem = new PgQuestRequirementHasEffectKeyword();

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

    private static bool FinishItemRuntimeBehaviorRuleSet(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementRuntimeBehaviorRuleSet NewItem = new PgQuestRequirementRuntimeBehaviorRuleSet();

        bool Result = true;
        string RuleKey = string.Empty;

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
                    case "Rule":
                        Result = SetStringProperty((string valueString) => RuleKey = valueString, Value);
                        if (RuleKey == "HalloweenQuests")
                            NewItem.Rule = "Halloween Quests";
                        else if (RuleKey == "During Christmas Quests")
                            NewItem.Rule = RuleKey;
                        else if (RuleKey == "TurkeysSpawnInBushes")
                            NewItem.Rule = "Turkeys Spawn In Bushes";
                        else
                            Result = Program.ReportFailure($"Unknown Rule: '{RuleKey}'");
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

    private static bool FinishItemIsLongtimeAnimal(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementIsLongtimeAnimal NewItem = new PgQuestRequirementIsLongtimeAnimal();

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

    private static bool FinishItemInteractionFlagUnset(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementInteractionFlagUnset NewItem = new PgQuestRequirementInteractionFlagUnset();

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
                    case "InteractionFlag":
                        Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.InteractionFlag = valueEnum, Value);
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

    private static bool FinishItemMinFavor(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementMinFavorLevel NewItem = new PgQuestRequirementMinFavorLevel();

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
                    case "Npc":
                        Result = Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => NewItem.FavorNpc = npcLocation, Value, parsedFile, parsedKey);
                        break;
                    case "MinFavor":
                        Result = SetIntProperty((int valueInt) => NewItem.RawMinFavor = valueInt, Value);
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

    private static bool FinishItemScriptAtomicMatches(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementScriptAtomicMatches NewItem = new PgQuestRequirementScriptAtomicMatches();

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
                    case "AtomicVar":
                        Result = SetStringProperty((string valueString) => NewItem.AtomicVar = valueString, Value);
                        break;
                    case "Value":
                        Result = SetStringProperty((string valueString) => NewItem.Value = valueString, Value);
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
        PgQuestRequirementAreaEventOff NewItem = new PgQuestRequirementAreaEventOff();

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
                    case "EventQuest":
                        Result = StringToEnumConversion<QuestKeyword>.SetEnum((QuestKeyword valueEnum) => NewItem.Quest = valueEnum, Value);
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

    private static bool FinishItemQuestCompletedRecently(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementQuestCompletedRecently NewItem = new PgQuestRequirementQuestCompletedRecently();

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
                    case "Quest":
                        Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.QuestList.Add(valueQuest.Key), Value);
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

    private static bool FinishItemGeneralShape(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementGeneralShape NewItem = new PgQuestRequirementGeneralShape();

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
                    case "Shape":
                        Result = SetStringProperty((string valueString) => NewItem.Shape = valueString, Value);
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
        PgQuestRequirementAppearance NewItem = new PgQuestRequirementAppearance();

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

    private static bool FinishItemAttributeMatchesScriptAtomic(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementAttributeMatchesScriptAtomic NewItem = new PgQuestRequirementAttributeMatchesScriptAtomic();

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
                    case "Attribute":
                        Result = SetStringProperty((string valueString) => NewItem.Attribute = valueString, Value);
                        break;
                    case "ScriptAtomicInt":
                        Result = SetStringProperty((string valueString) => NewItem.ScriptAtomicInt = valueString, Value);
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

    private static bool FinishItemAccountFlagUnset(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementAccountFlagUnset NewItem = new PgQuestRequirementAccountFlagUnset();

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
                    case "AccountFlag":
                        Result = SetStringProperty((string valueString) => NewItem.AccountFlag = valueString, Value);
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

    private static bool FinishItemDayOfWeek(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementDayOfWeek NewItem = new PgQuestRequirementDayOfWeek();

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
                    case "DaysAllowed":
                        Result = StringToEnumConversion<DayOfWeek>.TryParseList(Value, NewItem.DaysAllowedList);
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

    private static bool FinishItemIsVampire(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementIsVampire NewItem = new PgQuestRequirementIsVampire();

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

    private static bool FinishItemMinCombatSkillLevel(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRequirementMinCombatSkillLevel NewItem = new PgQuestRequirementMinCombatSkillLevel();

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
                        Result = SetIntProperty((int valueInt) => NewItem.RawSkillLevel = valueInt, Value);
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
