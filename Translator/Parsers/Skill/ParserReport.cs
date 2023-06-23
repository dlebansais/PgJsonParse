namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserReport : Parser
{
    public override object CreateItem()
    {
        return new PgReport();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgReport AsPgReport)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgReport, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgReport item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Report":
                    Result = SetStringProperty((string valueString) => item.Text = Tools.CleanedUpString(valueString), Value);
                    break;
                case "Level":
                    Result = SetIntProperty((int valueInt) => item.RawReportLevel = valueInt, Value);
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
