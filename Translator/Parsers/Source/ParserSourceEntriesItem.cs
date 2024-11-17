namespace Translator;

using System;
using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserSourceEntriesItem : Parser
{
    public static bool Parse(Action<PgSourceEntriesItem> setter, object value, string parsedFile, string parsedKey, ErrorControl errorControl = ErrorControl.Normal)
    {
        if (!(value is string ValueKey))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

        return Inserter<PgSourceEntriesItem>.SetItemByKey(setter, ValueKey, errorControl);
    }

    public override object CreateItem()
    {
        return new PgSourceEntriesItem();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgSourceEntriesItem AsPgSourceEntriesItem)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgSourceEntriesItem, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgSourceEntriesItem item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Entries":
                    Result = Inserter<PgSource>.AddKeylessArray(item.EntryList, Value);
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
            foreach (PgSource Source in item.EntryList)
                Source.IsItem = true;
        }

        return Result;
    }
}
