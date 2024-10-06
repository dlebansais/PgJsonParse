namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserAreaDetail : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<AreaType, VariadicObjectHandler> HandlerTable = new Dictionary<AreaType, VariadicObjectHandler>()
    {
        { AreaType.None, FinishItemNone },
        { AreaType.Any, FinishItemAny },
        { AreaType.Apartment, FinishItemApartment },
        { AreaType.Normal, FinishItemNormal },
    };

    private static Dictionary<AreaType, List<string>> KnownFieldTable = new Dictionary<AreaType, List<string>>()
    {
        { AreaType.None, new List<string>() { "AreaType", "AreaName" } },
        { AreaType.Any, new List<string>() { "AreaType", "AreaName" } },
        { AreaType.Apartment, new List<string>() { "AreaType", "AreaName" } },
        { AreaType.Normal, new List<string>() { "AreaType", "AreaName" } },
    };

    private static Dictionary<AreaType, List<string>> HandledTable = new Dictionary<AreaType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("AreaType"))
            return Program.ReportFailure(parsedFile, parsedKey, "Storage Requirement is missing a T type qualifier");

        object TypeValue = contentTable["AreaType"];

        if (!(TypeValue is int AsTypeInt))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        AreaType requirementType = (AreaType)AsTypeInt;

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
        return Finalizer<AreaType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemNone(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgAreaDetailNone NewItem = new PgAreaDetailNone();

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
                    case "AreaType":
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

    private static bool FinishItemAny(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgAreaDetailAny NewItem = new PgAreaDetailAny();

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
                    case "AreaType":
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

    private static bool FinishItemApartment(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgAreaDetailApartment NewItem = new PgAreaDetailApartment();

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
                    case "AreaType":
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

    private static bool FinishItemNormal(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgAreaDetailNormal NewItem = new PgAreaDetailNormal();

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
                    case "AreaType":
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
}
