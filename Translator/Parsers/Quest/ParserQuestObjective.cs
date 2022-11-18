namespace Translator
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using PgJsonReader;
    using PgObjects;

    public class ParserQuestObjective : Parser
    {
        public override object CreateItem()
        {
            return null!;
        }

        private static Dictionary<QuestObjectiveType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestObjectiveType, VariadicObjectHandler>()
        {
            { QuestObjectiveType.Kill, FinishItemKill },
            { QuestObjectiveType.Scripted, FinishItemScripted },
            { QuestObjectiveType.MultipleInteractionFlags, FinishItemMultipleInteractionFlags },
            { QuestObjectiveType.Collect, FinishItemCollect },
            { QuestObjectiveType.InteractionFlag, FinishItemInteractionFlag },
            { QuestObjectiveType.Deliver, FinishItemDeliver },
            { QuestObjectiveType.Have, FinishItemHave },
            { QuestObjectiveType.Harvest, FinishItemHarvest },
            { QuestObjectiveType.TipPlayer, FinishItemTipPlayer },
            { QuestObjectiveType.Special, FinishItemSpecial },
            { QuestObjectiveType.GiveGift, FinishItemGiveGift },
            { QuestObjectiveType.UseItem, FinishItemUseItem },
            { QuestObjectiveType.UseRecipe, FinishItemUseRecipe },
            { QuestObjectiveType.KillElite, FinishItemKillElite },
            { QuestObjectiveType.SayInChat, FinishItemSayInChat },
            { QuestObjectiveType.BeAttacked, FinishItemBeAttacked },
            { QuestObjectiveType.Bury, FinishItemBury },
            { QuestObjectiveType.UseAbility, FinishItemUseAbility },
            { QuestObjectiveType.UniqueSpecial, FinishItemUniqueSpecial },
            { QuestObjectiveType.GuildGiveItem, FinishItemGuildGiveItem },
            { QuestObjectiveType.GuildKill, FinishItemGuildKill },
            { QuestObjectiveType.DruidKill, FinishItemDruidKill },
            { QuestObjectiveType.DruidScripted, FinishItemDruidScripted },
            { QuestObjectiveType.Loot, FinishItemLoot },
            { QuestObjectiveType.ScriptedReceiveItem, FinishItemScriptedReceiveItem },
            { QuestObjectiveType.UseAbilityOnTargets, FinishItemUseAbilityOnTargets },
            { QuestObjectiveType.CompleteQuest, FinishItemCompleteQuest },
            { QuestObjectiveType.MeetRequirements, FinishItemMeetRequirements },
            { QuestObjectiveType.ScriptAtomicInt, FinishItemScriptAtomicInt },
        };

        private static Dictionary<QuestObjectiveType, List<string>> KnownFieldTable = new Dictionary<QuestObjectiveType, List<string>>()
        {
            { QuestObjectiveType.Kill, new List<string>() { "Type", "Target", "Description", "AbilityKeyword", "Requirements", "GroupId", "Number", "InternalName" } },
            { QuestObjectiveType.Scripted, new List<string>() { "Type", "Description", "Requirements", "IsHiddenUntilEarlierObjectivesComplete", "GroupId", "Number" } },
            { QuestObjectiveType.MultipleInteractionFlags, new List<string>() { "Type", "Description", "InteractionFlags", "Number" } },
            { QuestObjectiveType.Collect, new List<string>() { "Type", "Target", "Description", "Requirements", "ItemName", "GroupId", "Number", "InternalName" } },
            { QuestObjectiveType.InteractionFlag, new List<string>() { "Type", "Target", "Description", /*"InteractionFlag",*/ "GroupId", "Number" } },
            { QuestObjectiveType.Deliver, new List<string>() { "Type", "Target", "Description", "ItemName", "NumToDeliver", "IsHiddenUntilEarlierObjectivesComplete", "Number", "InternalName" } },
            { QuestObjectiveType.Have, new List<string>() { "Type", "Target", "Description", "ItemName", "GroupId", "Number", "InternalName" } },
            { QuestObjectiveType.Harvest, new List<string>() { "Type", "Target", "Description", "ItemName", "Requirements", "GroupId", "Number" } },
            { QuestObjectiveType.TipPlayer, new List<string>() { "Type", "Description", "MinAmount", "Number" } },
            { QuestObjectiveType.Special, new List<string>() { "Type", "Target", "Description", "MinAmount", "StringParam", "MaxAmount", "Requirements", "GroupId", "Number" } },
            { QuestObjectiveType.GiveGift, new List<string>() { "Type", "Description", "MinFavorReceived", "MaxFavorReceived", "Number" } },
            { QuestObjectiveType.UseItem, new List<string>() { "Type", "Target", "Description", "ItemName", "Requirements", "GroupId", "BehaviorId", "Number", "InternalName" } },
            { QuestObjectiveType.UseRecipe, new List<string>() { "Type", "Target", "Description", "Skill", "ResultItemKeyword", "GroupId", "Number" } },
            { QuestObjectiveType.KillElite, new List<string>() { "Type", "Target", "Description", "Number", "Requirements" } },
            { QuestObjectiveType.SayInChat, new List<string>() { "Type", "Target", "Description", "GroupId", "Number" } },
            { QuestObjectiveType.BeAttacked, new List<string>() { "Type", "Target", "Description", "AnatomyType", "GroupId", "Number" } },
            { QuestObjectiveType.Bury, new List<string>() { "Type", "Target", "Description", "AnatomyType", "Number" } },
            { QuestObjectiveType.UseAbility, new List<string>() { "Type", "Target", "Description", "Number" } },
            { QuestObjectiveType.UniqueSpecial, new List<string>() { "Type", "Target", "Description", "GroupId", "Number" } },
            { QuestObjectiveType.GuildGiveItem, new List<string>() { "Type", "Target", "Description", "ItemName", "ItemKeyword", "GroupId", "Number" } },
            { QuestObjectiveType.GuildKill, new List<string>() { "Type", "Target", "Description", "GroupId", "Number" } },
            { QuestObjectiveType.DruidKill, new List<string>() { "Type", "Target", "Description", "GroupId", "Number" } },
            { QuestObjectiveType.DruidScripted, new List<string>() { "Type", "Target", "Description", "Number" } },
            { QuestObjectiveType.Loot, new List<string>() { "Type", "Target", "Description", "ItemName", "MonsterTypeTag", "GroupId", "Number" } },
            { QuestObjectiveType.ScriptedReceiveItem, new List<string>() { "Type", "Target", "Description", "Item", "Number" } },
            { QuestObjectiveType.UseAbilityOnTargets, new List<string>() { "Type", "Target", "Description", "AbilityKeyword", "Number", "Requirements" } },
            { QuestObjectiveType.CompleteQuest, new List<string>() { "Type", "Target", "Description", "IsHiddenUntilEarlierObjectivesComplete", "GroupId", "Number" } },
            { QuestObjectiveType.MeetRequirements, new List<string>() { "Type", "Description", "GroupId", "Number", "Requirements" } },
            { QuestObjectiveType.ScriptAtomicInt, new List<string>() { "Type", "Description", "GroupId", "Number", "Target" } },
        };

        private static Dictionary<QuestObjectiveType, List<string>> HandledTable = new Dictionary<QuestObjectiveType, List<string>>();

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item != null)
                return Program.ReportFailure("Unexpected failure");

            if (!contentTable.ContainsKey("Type"))
                return Program.ReportFailure(parsedFile, parsedKey, $"Quest objective is missing a Type qualifier");

            object TypeValue = contentTable["Type"];

            if (!(TypeValue is string AsTypeString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

            if (!StringToEnumConversion<QuestObjectiveType>.TryParse(AsTypeString, out QuestObjectiveType objectiveType))
                return false;

            if (!HandlerTable.ContainsKey(objectiveType))
                return Program.ReportFailure(parsedFile, parsedKey, $"Objective {objectiveType} has no handler");

            Debug.Assert(KnownFieldTable.ContainsKey(objectiveType));

            VariadicObjectHandler Handler = HandlerTable[objectiveType];
            List<string> KnownFieldList = KnownFieldTable[objectiveType];
            List<string> UsedFieldList = new List<string>();

            if (!Handler(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
                return false;

            if (!HandledTable.ContainsKey(objectiveType))
                HandledTable.Add(objectiveType, new List<string>());

            List<string> ReportedFieldList = HandledTable[objectiveType];
            foreach (string FieldName in UsedFieldList)
                if (!ReportedFieldList.Contains(FieldName))
                    ReportedFieldList.Add(FieldName);

            return true;
        }

        public static bool FinalizeParsing()
        {
            return Finalizer<QuestObjectiveType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
        }

        private static bool ParseCommonFields(PgQuestObjective item, string key, object value)
        {
            bool Result = true;

            switch (key)
            {
                case "Description":
                    Result = SetStringProperty((string valueString) => item.Description = valueString, value);
                    break;
                case "IsHiddenUntilEarlierObjectivesComplete":
                    Result = SetBoolProperty((bool valueBool) => item.RawMustCompleteEarlierObjectivesFirst = valueBool, value);
                    break;
                case "GroupId":
                    Result = SetIntProperty((int valueInt) => item.RawGroupId = valueInt, value);
                    break;
                case "Number":
                    Result = SetIntProperty((int valueInt) => item.RawNumber = valueInt, value);
                    break;
                case "InternalName":
                    Result = SetStringProperty((string valueString) => item.InternalName = valueString, value);
                    break;
                default:
                    Result = Program.ReportFailure("Unexpected failure");
                    break;
            }

            return Result;
        }

        private static bool FinishItemKill(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveKill NewItem = new PgQuestObjectiveKill();

            bool Result = true;
            AbilityKeyword RequirementKeyword = AbilityKeyword.Internal_None;
            PgQuestObjectiveRequirement Requirement = null!;

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
                        case "Target":
                            Result = ParseKillTarget(NewItem, Value, parsedFile, parsedKey);
                            break;
                        case "AbilityKeyword":
                            Result = StringToEnumConversion<AbilityKeyword>.SetEnum((AbilityKeyword valueEnum) => RequirementKeyword = valueEnum, Value);
                            break;
                        case "Requirements":
                            if (Value is List<object>)
                            {
                                Result = Inserter<PgQuestObjectiveRequirement>.AddKeylessArray(NewItem.QuestObjectiveRequirementList, Value);
                            }
                            else
                            {
                                Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestRequirement) => Requirement = valueQuestRequirement, Value);
                            }
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                        case "InternalName":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (RequirementKeyword != AbilityKeyword.Internal_None)
                {
                    PgQuestObjectiveRequirementUseAbility NewRequirement = new PgQuestObjectiveRequirementUseAbility() { Keyword = RequirementKeyword };

                    ParsingContext.AddSuplementaryObject(NewRequirement);
                    NewItem.QuestObjectiveRequirementList.Add(NewRequirement);
                }

                if (Requirement != null)
                    NewItem.QuestObjectiveRequirementList.Add(Requirement);

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool ParseKillTarget(PgQuestObjectiveKill item, object value, string parsedFile, string parsedKey)
        {
            bool Result;

            if (value is List<object> AsList)
            {
                if (AsList.Count < 1)
                    Result = Program.ReportFailure($"List {AsList} should not be empty");
                else if (AsList.Count == 1)
                {
                    if (AsList[0] is string ValueString)
                        Result = StringToEnumConversion<QuestObjectiveTarget>.SetEnum((QuestObjectiveTarget valueEnum) => item.Target = valueEnum, ValueString);
                    else
                        Result = Program.ReportFailure($"List {AsList} should contain strings");
                }
                else if (AsList.Count == 2)
                {
                    if (AsList[0] is string ValueString1 && AsList[1] is string ValueString2)
                    {
                        bool Result1 = StringToEnumConversion<QuestObjectiveTarget>.SetEnum((QuestObjectiveTarget valueEnum) => item.Target = valueEnum, ValueString1);

                        bool Result2;
                        if (ValueString2.StartsWith("Area:"))
                        {
                            string AreaName = ValueString2.Substring(5);
                            Result2 = StringToEnumConversion<MapAreaName>.TryParse(AreaName, out MapAreaName ParsedAreaName);

                            if (Result2)
                            {
                                PgQuestObjectiveRequirementAreaEventOff NewRequirement = new PgQuestObjectiveRequirementAreaEventOff() { AreaName = ParsedAreaName };

                                ParsingContext.AddSuplementaryObject(NewRequirement);
                                item.QuestObjectiveRequirementList.Add(NewRequirement);
                            }
                        }
                        else
                            Result2 = Program.ReportFailure($"{ValueString2} expected to be an area");

                        Result = Result1 && Result2;
                    }
                    else
                        Result = Program.ReportFailure($"List {AsList} should contain strings");
                }
                else
                    Result = Program.ReportFailure($"List {AsList} too big");
            }
            else
                Result = StringToEnumConversion<QuestObjectiveTarget>.SetEnum((QuestObjectiveTarget valueEnum) => item.Target = valueEnum, value);
            
            return Result;
        }

        private static bool FinishItemScripted(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveScripted NewItem = new PgQuestObjectiveScripted();

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
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestRequirement) => NewItem.QuestObjectiveRequirement = valueQuestRequirement, Value);
                            break;
                        case "Description":
                        case "IsHiddenUntilEarlierObjectivesComplete":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemMultipleInteractionFlags(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveMultipleInteractionFlags NewItem = new PgQuestObjectiveMultipleInteractionFlags();

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
                        case "InteractionFlags":
                            Result = StringToEnumConversion<InteractionFlag>.TryParseList(Value, NewItem.InteractionFlagList);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemCollect(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("ItemName") && contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent item info");
            else if (!contentTable.ContainsKey("ItemName") && !contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Missing item info");
            else if (contentTable.ContainsKey("ItemName"))
                return FinishItemCollectItem(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
            else
                return FinishItemCollectItemKeyword(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
        }

        private static bool FinishItemCollectItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveCollectItem NewItem = new PgQuestObjectiveCollectItem();

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
                        case "ItemName":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.AddKeylessArray(NewItem.QuestObjectiveRequirementList, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                        case "InternalName":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("ItemName"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing item name");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemCollectItemKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveCollectItemKeyword NewItem = new PgQuestObjectiveCollectItemKeyword();

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
                        case "Target":
                            Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                        case "InternalName":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("Target"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemInteractionFlag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveInteractionFlag NewItem = new PgQuestObjectiveInteractionFlag();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        /*case "InteractionFlag":
                            Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.InteractionFlag = valueEnum, Value);
                            break;*/
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                /*if (!contentTable.ContainsKey("InteractionFlag"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing interaction flag");*/

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemDeliver(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveDeliver NewItem = new PgQuestObjectiveDeliver();

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
                        case "Target":
                            Result = Inserter<PgQuestObjectiveDeliver>.SetNpc((PgNpcLocation npcLocation) => NewItem.DeliverNpc = npcLocation, Value, parsedFile, parsedKey);
                            break;
                        case "ItemName":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "NumToDeliver":
                            Result = SetIntProperty((int valueInt) => NewItem.RawNumToDeliver = valueInt, Value);
                            break;
                        case "Description":
                        case "IsHiddenUntilEarlierObjectivesComplete":
                        case "Number":
                        case "InternalName":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.DeliverNpc == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing NPC");
                if (NewItem.Item_Key == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing item");
                if ((NewItem.RawNumToDeliver != null && NewItem.Number > 1) || (NewItem.RawNumToDeliver == null && NewItem.RawNumber.HasValue && NewItem.RawNumber.Value != 1))
                    return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent number to deliver");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemHave(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("ItemName") && contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent item info");
            else if (!contentTable.ContainsKey("ItemName") && !contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Missing item info");
            else if (contentTable.ContainsKey("ItemName"))
                return FinishItemHaveItem(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
            else
                return FinishItemHaveItemKeyword(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
        }

        private static bool FinishItemHaveItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveHaveItem NewItem = new PgQuestObjectiveHaveItem();

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
                        case "ItemName":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
                            break;
                        case "InternalName":
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("ItemName"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing item name");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemHaveItemKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveHaveItemKeyword NewItem = new PgQuestObjectiveHaveItemKeyword();

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
                        case "Target":
                            Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("Target"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemHarvest(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("ItemName") && contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent item info");
            else if (!contentTable.ContainsKey("ItemName") && !contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Missing item info");
            else if (contentTable.ContainsKey("ItemName"))
                return FinishItemHarvestItem(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
            else
                return FinishItemHarvestItemKeyword(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
        }

        private static bool FinishItemHarvestItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveHarvestItem NewItem = new PgQuestObjectiveHarvestItem();

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
                        case "ItemName":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.AddKeylessArray(NewItem.QuestObjectiveRequirementList, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("ItemName"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing item name");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemHarvestItemKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveHarvestItemKeyword NewItem = new PgQuestObjectiveHarvestItemKeyword();

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
                        case "Target":
                            Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.AddKeylessArray(NewItem.QuestObjectiveRequirementList, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("Target"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemTipPlayer(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveTipPlayer NewItem = new PgQuestObjectiveTipPlayer();

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
                        case "MinAmount":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMinAmount = valueInt, Value);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!NewItem.RawMinAmount.HasValue)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing min amount");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemSpecial(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveSpecial NewItem = new PgQuestObjectiveSpecial();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "MinAmount":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMinAmount = valueInt, Value);
                            break;
                        case "StringParam":
                            Result = ParseStringParam(NewItem, Value, parsedFile, parsedKey);
                            break;
                        case "MaxAmount":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMaxAmount = valueInt, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestRequirement) => NewItem.QuestObjectiveRequirement = valueQuestRequirement, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.RawMinAmount.HasValue)
                {
                    if (!NewItem.RawNumber.HasValue || (NewItem.RawMinAmount.Value != 1 && NewItem.RawMinAmount.Value != NewItem.RawNumber.Value))
                        return Program.ReportFailure(parsedFile, parsedKey, "Invalid objective number");
                }

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool ParseStringParam(PgQuestObjectiveSpecial item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueString))
                return Program.ReportFailure(parsedFile, parsedKey, $"{value} was expected to be string");

            if (ValueString.StartsWith("Genetics_"))
            {
                string AnatomySkillName = "Anatomy_" + ValueString.Substring(9);
                if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => item.AnatomySkill_Key = valueSkill.Key, AnatomySkillName))
                    return false;
            }
            else
                item.StringParam = ValueString;

            return true;
        }

        private static bool FinishItemGiveGift(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveGiveGift NewItem = new PgQuestObjectiveGiveGift();

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
                        case "MinFavorReceived":
                            Result = SetFloatProperty((float valueFloat) => NewItem.RawMinFavorReceived = valueFloat, Value);
                            break;
                        case "MaxFavorReceived":
                            Result = SetFloatProperty((float valueFloat) => NewItem.RawMaxFavorReceived = valueFloat, Value);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!NewItem.RawMinFavorReceived.HasValue && !NewItem.RawMaxFavorReceived.HasValue)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing favor");
                if (NewItem.RawMinFavorReceived.HasValue && NewItem.RawMaxFavorReceived.HasValue)
                    return Program.ReportFailure(parsedFile, parsedKey, "Both min and max favor");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemUseItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("ItemName") && contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent item info");
            else if (!contentTable.ContainsKey("ItemName") && !contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Missing item info");
            else if (contentTable.ContainsKey("ItemName"))
                return FinishItemUseItemLink(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
            else
                return FinishItemUseItemKeyword(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
        }

        private static bool FinishItemUseItemLink(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveUseItem NewItem = new PgQuestObjectiveUseItem();

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
                        case "ItemName":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestRequirement) => NewItem.QuestObjectiveRequirement = valueQuestRequirement, Value);
                            break;
                        case "BehaviorId":
                            Result = SetStringProperty((string valueString) => NewItem.BehaviorId = valueString, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                        case "InternalName":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("ItemName"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing item name");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemUseItemKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveUseItemKeyword NewItem = new PgQuestObjectiveUseItemKeyword();

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
                        case "Target":
                            Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestRequirement) => NewItem.QuestObjectiveRequirement = valueQuestRequirement, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("Target"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemUseRecipe(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveUseRecipe NewItem = new PgQuestObjectiveUseRecipe();

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
                        case "Target":
                            Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => NewItem.Target_Key = valueRecipe.Key, Value, ErrorControl.IgnoreIfNotFound);
                            if (!Result)
                                Result = StringToEnumConversion<RecipeKeyword>.SetEnum((RecipeKeyword valueEnum) => NewItem.TargetKeyword = valueEnum, Value);
                            break;
                        case "Skill":
                            Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = valueSkill.Key, Value);
                            break;
                        case "ResultItemKeyword":
                            Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.ResultItemKeyword = valueEnum, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if ((NewItem.Skill_Key != null && NewItem.TargetKeyword != RecipeKeyword.Internal_None) ||
                    (NewItem.Skill_Key != null && NewItem.ResultItemKeyword != ItemKeyword.Internal_None) ||
                    (NewItem.ResultItemKeyword != ItemKeyword.Internal_None && NewItem.TargetKeyword != RecipeKeyword.Internal_None))
                    return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent recipe");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemKillElite(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveKillElite NewItem = new PgQuestObjectiveKillElite();

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
                        case "Target":
                            Result = StringToEnumConversion<QuestObjectiveTarget>.SetEnum((QuestObjectiveTarget valueEnum) => NewItem.Target = valueEnum, Value);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestObjectiveRequirement) => NewItem.QuestRequirement = valueQuestObjectiveRequirement, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemSayInChat(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveSayInChat NewItem = new PgQuestObjectiveSayInChat();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemBeAttacked(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveBeAttacked NewItem = new PgQuestObjectiveBeAttacked();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "AnatomyType":
                            Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.AnatomySkill_Key = valueSkill.Key, $"Anatomy_{Value}");
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.Target.Length == 0 && NewItem.AnatomySkill_Key == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target or skill");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemBury(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveBury NewItem = new PgQuestObjectiveBury();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "AnatomyType":
                            Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.AnatomySkill_Key = valueSkill.Key, $"Anatomy_{Value}");
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.Target.Length == 0 && NewItem.AnatomySkill_Key == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target or skill");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemUseAbility(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveUseAbility NewItem = new PgQuestObjectiveUseAbility();

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
                        case "Target":
                            Result = StringToEnumConversion<AbilityKeyword>.SetEnum((AbilityKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("Target"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemUniqueSpecial(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveUniqueSpecial NewItem = new PgQuestObjectiveUniqueSpecial();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemGuildGiveItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("ItemName") && contentTable.ContainsKey("ItemKeyword"))
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent item info");
            else if (!contentTable.ContainsKey("ItemName") && !contentTable.ContainsKey("ItemKeyword"))
                return Program.ReportFailure(parsedFile, parsedKey, "Missing item info");
            else if (contentTable.ContainsKey("ItemName"))
                return FinishItemGuildGiveItemLink(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
            else
                return FinishItemGuildGiveItemKeyword(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
        }

        private static bool FinishItemGuildGiveItemLink(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveGuildGiveItem NewItem = new PgQuestObjectiveGuildGiveItem();

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
                        case "Target":
                            Result = Inserter<PgQuestObjectiveDeliver>.SetNpc((PgNpcLocation npcLocation) => NewItem.DeliverNpc = npcLocation, Value, parsedFile, parsedKey);
                            break;
                        case "ItemName":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("ItemName"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing item name");

                if (NewItem.DeliverNpc == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing deliver NPC");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemGuildGiveItemKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveGuildGiveItemKeyword NewItem = new PgQuestObjectiveGuildGiveItemKeyword();

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
                        case "Target":
                            Result = Inserter<PgQuestObjectiveDeliver>.SetNpc((PgNpcLocation npcLocation) => NewItem.DeliverNpc = npcLocation, Value, parsedFile, parsedKey);
                            break;
                        case "ItemKeyword":
                            Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("Target"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                if (NewItem.DeliverNpc == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing deliver NPC");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemGuildKill(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveGuildKill NewItem = new PgQuestObjectiveGuildKill();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.Target.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemDruidKill(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveDruidKill NewItem = new PgQuestObjectiveDruidKill();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.Target.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemDruidScripted(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveDruidScripted NewItem = new PgQuestObjectiveDruidScripted();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.Target.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemLoot(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("ItemName") && contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent item info");
            else if (!contentTable.ContainsKey("ItemName") && !contentTable.ContainsKey("Target"))
                return Program.ReportFailure(parsedFile, parsedKey, "Missing item info");
            else if (contentTable.ContainsKey("ItemName"))
                return FinishItemLootItem(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
            else
                return FinishItemLootItemKeyword(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
        }

        private static bool FinishItemLootItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveLootItem NewItem = new PgQuestObjectiveLootItem();

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
                        case "ItemName":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "MonsterTypeTag":
                            Result = StringToEnumConversion<MonsterTypeTag>.SetEnum((MonsterTypeTag valueEnum) => NewItem.MonsterTypeTag = valueEnum, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("ItemName"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing item name");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemLootItemKeyword(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveLootItemKeyword NewItem = new PgQuestObjectiveLootItemKeyword();

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
                        case "Target":
                            Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "MonsterTypeTag":
                            Result = StringToEnumConversion<MonsterTypeTag>.SetEnum((MonsterTypeTag valueEnum) => NewItem.MonsterTypeTag = valueEnum, Value);
                            break;
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("Target"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemScriptedReceiveItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveScriptedReceiveItem NewItem = new PgQuestObjectiveScriptedReceiveItem();

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
                        case "Target":
                            Result = Inserter<PgQuestObjectiveScriptedReceiveItem>.SetNpc((PgNpcLocation npcLocation) => NewItem.DeliverNpc = npcLocation, Value, parsedFile, parsedKey);
                            break;
                        case "Item":
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = valueItem.Key, Value);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.DeliverNpc == null || NewItem.Item_Key == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing target NPC or item");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemUseAbilityOnTargets(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveUseAbilityOnTargets NewItem = new PgQuestObjectiveUseAbilityOnTargets();

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
                        case "Target":
                            Result = SetStringProperty((string valueString) => NewItem.Target = valueString, Value);
                            break;
                        case "AbilityKeyword":
                            Result = StringToEnumConversion<AbilityKeyword>.SetEnum((AbilityKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
                            break;
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestRequirement) => NewItem.QuestObjectiveRequirement = valueQuestRequirement, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (!contentTable.ContainsKey("AbilityKeyword"))
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing ability keyword");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemCompleteQuest(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveCompleteQuest NewItem = new PgQuestObjectiveCompleteQuest();

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
                        case "Target":
                            Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => NewItem.TargetQuest_Key = valueQuest.Key, Value);
                            break;
                        case "Description":
                        case "IsHiddenUntilEarlierObjectivesComplete":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                if (NewItem.TargetQuest_Key == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing quest");
                if (NewItem.RawNumber.HasValue && NewItem.RawNumber.Value > 1)
                    return Program.ReportFailure(parsedFile, parsedKey, "Too many quests");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemMeetRequirements(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveMeetRequirements NewItem = new PgQuestObjectiveMeetRequirements();

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
                        case "Requirements":
                            Result = Inserter<PgQuestObjectiveRequirement>.SetItemProperty((PgQuestObjectiveRequirement valueQuestObjectiveRequirement) => NewItem.QuestRequirement = valueQuestObjectiveRequirement, Value);
                            break;
                        case "GroupId":
                        case "Description":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemScriptAtomicInt(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveScriptAtomicInt NewItem = new PgQuestObjectiveScriptAtomicInt();

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
                        case "Description":
                        case "GroupId":
                        case "Number":
                            Result = ParseCommonFields(NewItem, Key, Value);
                            break;
                        case "Target":
                            Result = StringToEnumConversion<QuestObjectiveTarget>.SetEnum((QuestObjectiveTarget valueEnum) => NewItem.Target = valueEnum, Value);
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
                if (NewItem.Description.Length == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "Missing description");

                item = NewItem;
                return true;
            }
            else
                return false;
        }
    }
}
