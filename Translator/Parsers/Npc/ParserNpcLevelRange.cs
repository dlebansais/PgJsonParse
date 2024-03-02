namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserNpcLevelRange : Parser
{
    public override object CreateItem()
    {
        return new PgNpcLevelRange();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgNpcLevelRange AsPgNpcLevelRange)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgNpcLevelRange, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgNpcLevelRange item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Min":
                    Result = SetIntProperty((int valueInt) => item.RawMin = valueInt, Value);
                    break;
                case "Max":
                    Result = SetIntProperty((int valueInt) => item.RawMax = valueInt, Value);
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
            if (item.RawMin == null)
                return Program.ReportFailure(parsedFile, parsedKey, "No LevelRange Min value");

            if (item.RawMax == null)
                return Program.ReportFailure(parsedFile, parsedKey, "No LevelRange Max value");
        }

        return Result;
    }
}
