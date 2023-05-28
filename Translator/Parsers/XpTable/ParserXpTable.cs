namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserXpTable : Parser
{
    public override object CreateItem()
    {
        return new PgXpTable();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgXpTable AsPgXpTable)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgXpTable, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgXpTable item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "InternalName":
                    Result = ParseInternamName(item, Value, parsedFile, parsedKey);
                    break;
                case "XpAmounts":
                    Result = ParseXpAmounts(item, Value, parsedFile, parsedKey);
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

    private bool ParseInternamName(PgXpTable item, object value, string parsedFile, string parsedKey)
    {
        if (!(value is string ValueString))
            return Program.ReportFailure($"Value '{value}' was expected to be a string");

        item.InternalName = ValueString;
        StringToEnumConversion<XpTableEnum>.SetEnum((XpTableEnum valueEnum) => item.AsEnum = valueEnum, value);

        return true;
    }

    private bool ParseXpAmounts(PgXpTable item, object value, string parsedFile, string parsedKey)
    {
        if (!(value is List<object> ArrayAmount))
            return Program.ReportFailure($"Value '{value}' was expected to be a list");

        int Level = 0;
        int TotalXp = 0;

        foreach (object Item in ArrayAmount)
        {
            if (!(Item is int ValueAmount))
                return Program.ReportFailure($"Value '{Item}' was expected to be an int");

            TotalXp += ValueAmount;
            Level++;

            PgXpTableLevel NewLevel = new PgXpTableLevel() { RawLevel = Level, RawXp = ValueAmount, RawTotalXp = TotalXp };

            ParsingContext.AddSuplementaryObject(NewLevel);
            item.XpAmountList.Add(NewLevel);
        }

        return true;
    }
}
