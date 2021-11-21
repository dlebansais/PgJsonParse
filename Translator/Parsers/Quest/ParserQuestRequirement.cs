namespace Translator
{
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
        };

        private static Dictionary<QuestRequirementType, List<string>> KnownFieldTable = new Dictionary<QuestRequirementType, List<string>>()
        {
            { QuestRequirementType.MinFavorLevel, new List<string>() { "T", "Npc", "Level" /*, "Quest"*/ } },
            { QuestRequirementType.Race, new List<string>() { "T", "AllowedRace", "DisallowedRace" } },
            { QuestRequirementType.QuestCompleted, new List<string>() { "T", "Quest" } },
            { QuestRequirementType.IsWarden, new List<string>() { "T" } },
            { QuestRequirementType.AreaEventOn, new List<string>() { "T", "AreaEvent" } },
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
            { QuestRequirementType.AreaEventOff, new List<string>() { "T", "AreaEvent" } },
            { QuestRequirementType.QuestCompletedRecently, new List<string>() { "T", "Quest" } },
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
                        case "Level":
                            Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.FavorLevel = valueEnum, Value);
                            break;
                        /*case "Quest":
                            Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.QuestList.Add(valueQuest), Value);
                            break;*/
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
                            Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.QuestList.Add(valueQuest), Value);
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
                        case "AreaEvent":
                            Result = ParseAreaEvent(NewItem, Value, parsedFile, parsedKey);
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

        private static bool ParseAreaEvent(PgQuestRequirementAreaEventOn newItem, object value, string parsedFile, string parsedKey)
        {
            if (!ParseAreaEvent(value, parsedFile, parsedKey, out MapAreaName AreaName))
                return false;

            newItem.AreaName = AreaName;
            return true;
        }

        private static bool ParseAreaEvent(PgQuestRequirementAreaEventOff newItem, object value, string parsedFile, string parsedKey)
        {
            if (!ParseAreaEvent(value, parsedFile, parsedKey, out MapAreaName AreaName))
                return false;

            newItem.AreaName = AreaName;
            return true;
        }

        public static bool ParseAreaEvent(object value, string parsedFile, string parsedKey, out MapAreaName areaName)
        {
            areaName = MapAreaName.Internal_None;

            if (!(value is string AreaString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            if (AreaString == "Daytime")
            {
                areaName = MapAreaName.Daytime;
                StringToEnumConversion<MapAreaName>.SetCustomParsedEnum(areaName);
                return true;
            }

            if (AreaString == "PovusNightlyQuest")
            {
                areaName = MapAreaName.PovusNightlyQuest;
                StringToEnumConversion<MapAreaName>.SetCustomParsedEnum(areaName);
                return true;
            }

            int AreaIndex = AreaString.LastIndexOf('_');
            if (AreaIndex > 0)
            {
                int KeyIndex = AreaString.LastIndexOf('_', AreaIndex - 1);
                if (KeyIndex > 0)
                {
                    string AreaName = AreaString.Substring(AreaIndex + 1);
                    string KeyName = AreaString.Substring(KeyIndex + 1, AreaIndex - KeyIndex - 1);
                    string QuestName = AreaString.Substring(0, KeyIndex);

                    if (AreaName == "Ilmari")
                        AreaName = "Desert1";
                    else if (AreaName == "Kur")
                        AreaName = "KurMountains";

                    MapAreaName ParsedAreaName = MapAreaName.Internal_None;
                    bool Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => ParsedAreaName = valueEnum, AreaName);
                    areaName = ParsedAreaName;
                    return Result;
                }
            }

            return Program.ReportFailure(parsedFile, parsedKey, $"Unknown area '{AreaString}'");
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
                            Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill = valueSkill, Value);
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
                            Result = SetStringProperty((string valueString) => NewItem.HangOut = valueString, Value);
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
                            Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.Quest = valueQuest, Value);
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
                            Result = ParseRule(NewItem, Value, parsedFile, parsedKey);
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

        private static bool ParseRule(PgQuestRequirementRuntimeBehaviorRuleSet item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueString))
                return Program.ReportFailure($"Value '{value}' was expected to be a string");

            string Rule;

            if (ValueString == "ChristmasQuests")
                Rule = "During Christmas Quests";
            else
                Rule = ValueString;

            item.Rule = Rule;
            return true;
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
                        case "AreaEvent":
                            Result = ParseAreaEvent(NewItem, Value, parsedFile, parsedKey);
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
                            Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.QuestList.Add(valueQuest), Value);
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
}
