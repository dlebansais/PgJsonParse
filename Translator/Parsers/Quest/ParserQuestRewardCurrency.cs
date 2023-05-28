namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserQuestRewardCurrency : Parser
{
    public override object CreateItem()
    {
        return new PgQuestRewardCurrency();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgQuestRewardCurrency AsPgQuestRewardCurrency)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgQuestRewardCurrency, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgQuestRewardCurrency item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (contentTable.Count != 1)
            return Program.ReportFailure(parsedFile, parsedKey, "Invalid currency reward object");

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string CurrencyKey = Entry.Key;
            object Value = Entry.Value;

            if (!StringToEnumConversion<Currency>.TryParse(CurrencyKey, out Currency Currency))
                return false;

            if (!(Value is int Amount))
                return Program.ReportFailure($"Value '{Value}' was expected to be an int");

            item.Currency = Currency;
            item.RawAmount = Amount;
        }

        return true;
    }
}
