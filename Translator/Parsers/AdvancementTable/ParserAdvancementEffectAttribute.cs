﻿namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserAdvancementEffectAttribute : Parser
{
    public override object CreateItem()
    {
        return new PgAdvancementEffectAttribute();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAdvancementEffectAttribute AsPgAdvancementEffectAttribute)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgAdvancementEffectAttribute, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAdvancementEffectAttribute item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgAttribute ParsedAttribute = null!;
        float? ParsedValue = null;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Attribute":
                    Result = Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, Value);
                    break;
                case "Value":
                    Result = SetFloatProperty((float valueFloat) => ParsedValue = valueFloat, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (ParsedAttribute is null || ParsedValue is null)
            Result = Program.ReportFailure(parsedFile, parsedKey, "Invalid advancement effect attribute");
        else
        {
            item.Attribute_Key = PgObject.GetItemKey(ParsedAttribute);
            item.RawValue = ParsedValue;
        }

        return Result;
    }
}
