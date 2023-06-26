namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserStockDye : Parser
{
    public override object CreateItem()
    {
        return new PgStockDye();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgStockDye AsPgStockDye)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgStockDye, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgStockDye item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Color1":
                    Result = SetColorProperty((uint valueColor) => item.RawColor1 = valueColor, Value);
                    break;
                case "Color2":
                    Result = SetColorProperty((uint valueColor) => item.RawColor2 = valueColor, Value);
                    break;
                case "Color3":
                    Result = SetColorProperty((uint valueColor) => item.RawColor3 = valueColor, Value);
                    break;
                case "IsGlowEnabled":
                    Result = SetBoolProperty((bool valueBool) => item.IsGlowEnabled = valueBool, Value);
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
