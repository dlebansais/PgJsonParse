namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserArea : Parser
{
    public override object CreateItem()
    {
        return new PgArea();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgArea AsPgArea)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgArea, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgArea item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "FriendlyName":
                    Result = SetStringProperty((string valueString) => item.FriendlyName = valueString, Value);
                    break;
                case "ShortFriendlyName":
                    Result = SetStringProperty((string valueString) => item.ShortFriendlyName = valueString, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        return Result;
    }
}
