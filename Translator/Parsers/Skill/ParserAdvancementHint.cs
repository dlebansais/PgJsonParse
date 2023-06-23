namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserAdvancementHint : Parser
{
    public override object CreateItem()
    {
        return new PgAdvancementHint();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAdvancementHint AsPgAdvancementHint)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgAdvancementHint, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAdvancementHint item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        string? HintString = null;
        int? EntryLevel = null!;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Hint":
                    Result = SetStringProperty((string valueString) => HintString = valueString, Value);
                    break;
                case "Level":
                    Result = SetIntProperty((int valueInt) => EntryLevel = valueInt, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (Result && HintString is not null && EntryLevel is not null)
        {
            item.HintTable.Add(EntryLevel.Value, HintString);
        }
        else
            Result = Program.ReportFailure(parsedFile, parsedKey, "Invalid advancement hint");

        return true;
    }
}
