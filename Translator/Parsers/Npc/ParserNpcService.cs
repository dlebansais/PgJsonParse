namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserNpcService : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<NpcServiceType, VariadicObjectHandler> HandlerTable = new Dictionary<NpcServiceType, VariadicObjectHandler>()
    {
        { NpcServiceType.AnimalHusbandry, FinishItemAnimalHusbandry },
        { NpcServiceType.Barter, FinishItemBarter },
        { NpcServiceType.Consignment, FinishItemConsignment },
        { NpcServiceType.GuildQuests, FinishItemGuildQuests },
        { NpcServiceType.InstallAugments, FinishItemInstallAugments },
        { NpcServiceType.Stables, FinishItemStables },
        { NpcServiceType.Storage, FinishItemStorage },
        { NpcServiceType.Store, FinishItemStore },
        { NpcServiceType.Training, FinishItemTraining },
    };

    private static Dictionary<NpcServiceType, List<string>> KnownFieldTable = new Dictionary<NpcServiceType, List<string>>()
    {
        { NpcServiceType.AnimalHusbandry, new List<string>() { "Type", "Favor" } },
        { NpcServiceType.Barter, new List<string>() { "Type", "Favor", "AdditionalUnlocks" } },
        { NpcServiceType.Consignment, new List<string>() { "Type", "Favor", "ItemTypes", "Unlocks" } },
        { NpcServiceType.GuildQuests, new List<string>() { "Type", "Favor" } },
        { NpcServiceType.InstallAugments, new List<string>() { "Type", "Favor", "LevelRanges" } },
        { NpcServiceType.Stables, new List<string>() { "Type", "Favor" } },
        { NpcServiceType.Storage, new List<string>() { "Type", "Favor", "ItemDescriptions", "SpaceIncreases" } },
        { NpcServiceType.Store, new List<string>() { "Type", "Favor", "CapIncreases" } },
        { NpcServiceType.Training, new List<string>() { "Type", "Favor", "Skills", "Unlocks" } },
    };

    private static Dictionary<NpcServiceType, List<string>> HandledTable = new Dictionary<NpcServiceType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("Type"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Npc service is missing a Type qualifier");

        object TypeValue = contentTable["Type"];

        if (!(TypeValue is string AsTypeString))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        if (!StringToEnumConversion<NpcServiceType>.TryParse(AsTypeString, out NpcServiceType serviceType))
            return false;

        if (!HandlerTable.ContainsKey(serviceType))
            return Program.ReportFailure(parsedFile, parsedKey, $"Service {serviceType} has no handler");

        Debug.Assert(KnownFieldTable.ContainsKey(serviceType));

        VariadicObjectHandler Handler = HandlerTable[serviceType];
        List<string> KnownFieldList = KnownFieldTable[serviceType];
        List<string> UsedFieldList = new List<string>();

        if (!Handler(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
            return false;

        if (!HandledTable.ContainsKey(serviceType))
            HandledTable.Add(serviceType, new List<string>());

        List<string> ReportedFieldList = HandledTable[serviceType];
        foreach (string FieldName in UsedFieldList)
            if (!ReportedFieldList.Contains(FieldName))
                ReportedFieldList.Add(FieldName);

        return true;
    }

    public static bool FinalizeParsing()
    {
        return Finalizer<NpcServiceType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemAnimalHusbandry(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceAnimalHusbandry NewItem = new PgNpcServiceAnimalHusbandry();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
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

    private static bool FinishItemBarter(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceBarter NewItem = new PgNpcServiceBarter();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
                        break;
                    case "AdditionalUnlocks":
                        Result = StringToEnumConversion<Favor>.TryParseList(Value, NewItem.AdditionalUnlockList);
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

    private static bool FinishItemConsignment(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceConsignment NewItem = new PgNpcServiceConsignment();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
                        break;
                    case "ItemTypes":
                        Result = StringToEnumConversion<ItemKeyword>.TryParseList(Value, NewItem.ItemTypeList);
                        break;
                    case "Unlocks":
                        Result = StringToEnumConversion<Favor>.TryParseList(Value, NewItem.UnlockList);
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

    private static bool FinishItemGuildQuests(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceGuildQuests NewItem = new PgNpcServiceGuildQuests();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
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

    private static bool FinishItemInstallAugments(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceInstallAugments NewItem = new PgNpcServiceInstallAugments();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
                        break;
                    case "LevelRanges":
                        Result = Inserter<PgNpcLevelRange>.AddKeylessArray(NewItem.LevelRangeList, Value);
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

    private static bool FinishItemStables(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceStables NewItem = new PgNpcServiceStables();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
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

    private static bool FinishItemStore(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceStore NewItem = new PgNpcServiceStore();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
                        break;
                    case "CapIncreases":
                        Result = StringToEnumConversion<Favor>.TryParseList(Value, NewItem.CapIncreaseList);
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

    private static bool FinishItemStorage(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceStorage NewItem = new PgNpcServiceStorage();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
                        break;
                    case "ItemDescriptions":
                        Result = ParseItemDescriptions(NewItem, Value, parsedFile, parsedKey);
                        break;
                    case "SpaceIncreases":
                        Result = StringToEnumConversion<Favor>.TryParseList(Value, NewItem.SpaceIncreaseList);
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

    private static bool ParseItemDescriptions(PgNpcServiceStorage item, object value, string parsedFile, string parsedKey)
    {
        if (!(value is List<object> ObjectList))
            return Program.ReportFailure($"Value '{value}' was expected to be a list");

        foreach (object Item in ObjectList)
        {
            if (Item is string ValueString)
                item.ItemDescriptionList.Add(ValueString);
            else
                return Program.ReportFailure($"ItemDescription '{Item}' was expected to be a string");
        }

        return true;
    }

    private static bool FinishItemTraining(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgNpcServiceTraining NewItem = new PgNpcServiceTraining();

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
                    case "Favor":
                        Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => NewItem.Favor = valueEnum, Value);
                        break;
                    case "Skills":
                        Result = ParseSkills(NewItem, Value, parsedFile, parsedKey);
                        break;
                    case "Unlocks":
                        Result = StringToEnumConversion<Favor>.TryParseList(Value, NewItem.UnlockList);
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

    private static bool ParseSkills(PgNpcServiceTraining item, object value, string parsedFile, string parsedKey)
    {
        if (!(value is List<object> ObjectList))
            return Program.ReportFailure($"Value '{value}' was expected to be a list");

        foreach (object Item in ObjectList)
        {
            if (Item is string SkillKey)
            {
                PgSkill ParsedSkill = null!;

                if (SkillKey == PgSkill.Unknown.Name)
                    ParsedSkill = PgSkill.Unknown;
                else if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, SkillKey))
                    return false;

                item.Skill_Keys.Add(ParsedSkill.Key);
            }
            else
                return Program.ReportFailure($"Skill '{Item}' was expected to be a string");
        }

        return true;
    }
}
