namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserQuestTime : Parser
{
    public override object CreateItem()
    {
        return new PgQuestTime();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgQuestTime AsPgQuestTime)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgQuestTime, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgQuestTime item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Days":
                    Result = SetIntProperty((int valueInt) => item.RawDays = valueInt, Value);
                    break;
                case "Hours":
                    Result = SetIntProperty((int valueInt) => item.RawHours = valueInt, Value);
                    break;
                case "Minutes":
                    Result = SetIntProperty((int valueInt) => item.RawMinutes = valueInt, Value);
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
        }

        return Result;
    }
}
