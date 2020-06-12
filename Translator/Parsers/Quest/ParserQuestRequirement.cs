namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParserQuestRequirement : Parser
    {
        public override object CreateItem()
        {
            return null;
        }

        private static Dictionary<QuestRequirementType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestRequirementType, VariadicObjectHandler>()
        {
            { QuestRequirementType.MinFavorLevel, FinishItemMinFavorLevel },
            { QuestRequirementType.Race, FinishItemRace },
            { QuestRequirementType.QuestCompleted, FinishItemQuestCompleted },
            { QuestRequirementType.IsWarden, FinishItemIsWarden },
            { QuestRequirementType.AreaEventOn, FinishItemAreaEventOn },
            { QuestRequirementType.MinSkillLevel, FinishItemMinSkillLevel },
            { QuestRequirementType.HangOutCompleted, FinishItemHangOutCompleted },
            { QuestRequirementType.InteractionFlagSet, FinishItemInteractionFlagSet },
            { QuestRequirementType.Or, FinishItemOr },
            { QuestRequirementType.GuildQuestCompleted, FinishItemGuildQuestCompleted },
            { QuestRequirementType.HasEffectKeyword, FinishItemHasEffectKeyword },
            { QuestRequirementType.RuntimeBehaviorRuleSet, FinishItemRuntimeBehaviorRuleSet },
            { QuestRequirementType.IsLongtimeAnimal, FinishItemIsLongtimeAnimal },
        };

        private static Dictionary<QuestRequirementType, List<string>> KnownFieldTable = new Dictionary<QuestRequirementType, List<string>>()
        {
            { QuestRequirementType.MinFavorLevel, new List<string>() { "T", "Npc", "Level" } },
            { QuestRequirementType.Race, new List<string>() { "T", "AllowedRace", "DisallowedRace" } },
            { QuestRequirementType.QuestCompleted, new List<string>() { "T", "Quest" } },
            { QuestRequirementType.IsWarden, new List<string>() { "T" } },
            { QuestRequirementType.AreaEventOn, new List<string>() { "T", "AreaEvent" } },
            { QuestRequirementType.MinSkillLevel, new List<string>() { "T", "Level", "Skill" } },
            { QuestRequirementType.HangOutCompleted, new List<string>() { "T", "HangOut" } },
            { QuestRequirementType.InteractionFlagSet, new List<string>() { "T", "InteractionFlag" } },
            { QuestRequirementType.Or, new List<string>() { "T", "List" } },
            { QuestRequirementType.GuildQuestCompleted, new List<string>() { "T", "Quest" } },
            { QuestRequirementType.HasEffectKeyword, new List<string>() { "T", "Keyword" } },
            { QuestRequirementType.RuntimeBehaviorRuleSet, new List<string>() { "T", "Rule" } },
            { QuestRequirementType.IsLongtimeAnimal, new List<string>() { "T" } },
        };

        private static Dictionary<QuestRequirementType, List<string>> HandledTable = new Dictionary<QuestRequirementType, List<string>>();

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
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

            if (!Handler(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
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

        private static bool FinishItemMinFavorLevel(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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
                            Result = Inserter<Favor>.SetEnum((Favor valueEnum) => NewItem.FavorLevel = valueEnum, Value);
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

        private static bool FinishItemRace(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestRequirementRace NewItem = new PgQuestRequirementRace();

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
                        case "AllowedRace":
                            Result = Inserter<Race>.SetEnum((Race valueEnum) => NewItem.AllowedRace = valueEnum, Value);
                            break;
                        case "DisallowedRace":
                            Result = Inserter<Race>.SetEnum((Race valueEnum) => NewItem.DisallowedRace = valueEnum, Value);
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

        private static bool FinishItemQuestCompleted(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemIsWarden(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemAreaEventOn(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool ParseAreaEvent(PgQuestRequirementAreaEventOn NewItem, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string AreaString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

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

                    return Inserter<MapAreaName>.SetEnum((MapAreaName valueEnum) => NewItem.AreaName = valueEnum, AreaName);
                }
            }

            return Program.ReportFailure(parsedFile, parsedKey, $"Unknown area '{AreaString}'");
        }

        private static bool FinishItemMinSkillLevel(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemHangOutCompleted(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemInteractionFlagSet(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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
                            Result = SetStringProperty((string valueString) => NewItem.InteractionFlag = valueString, Value);
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

        private static bool FinishItemOr(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemGuildQuestCompleted(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemHasEffectKeyword(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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
                            Result = Inserter<EffectKeyword>.SetEnum((EffectKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
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

        private static bool FinishItemRuntimeBehaviorRuleSet(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemIsLongtimeAnimal(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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
    }
}
