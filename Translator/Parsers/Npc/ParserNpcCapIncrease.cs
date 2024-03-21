namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserNpcCapIncrease : Parser
{
    public override object CreateItem()
    {
        return new PgNpcCapIncrease();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgNpcCapIncrease AsPgNpcCapIncrease)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgNpcCapIncrease, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgNpcCapIncrease item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Favor":
                    Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => item.CapIncreaseFavor = valueEnum, Value);
                    break;
                case "Value":
                    Result = SetIntProperty((int valueInt) => item.RawValue = valueInt, Value);
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
            if (item.RawValue == null)
                return Program.ReportFailure(parsedFile, parsedKey, "No CapIncrease value");
        }

        return Result;
    }
}
