namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserItemKeywordValues : Parser
{
    public override object CreateItem()
    {
        return new PgItemKeywordValues();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgItemKeywordValues AsPgItemKeywordValues)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgItemKeywordValues, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgItemKeywordValues item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Keyword":
                    Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => item.Keyword = valueEnum, Value);
                    break;
                case "Values":
                    Result = ParseValues(item, Value, parsedFile, parsedKey);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (!contentTable.ContainsKey("Keyword"))
            Result = Program.ReportFailure(parsedFile, parsedKey, "ItemKeywordValues has no keyword");
        else
            StringToEnumConversion<RecipeItemKey>.TryParse(item.Keyword.ToString(), out _, ErrorControl.IgnoreIfNotFound);

        return Result;
    }

    private bool ParseValues(PgItemKeywordValues item, object value, string parsedFile, string parsedKey)
    {
        if (!(value is List<object> ObjectList))
            return Program.ReportFailure($"Value '{value}' was expected to be a list");

        foreach (object Item in ObjectList)
        {
            if (Item is float ValueFloat)
                item.Values.Add(ValueFloat);
            else if (Item is int ValueInt)
                item.Values.Add((float)ValueInt);
            else
                return Program.ReportFailure($"Value '{Item}' was expected to be a float or int");
        }

        return true;
    }
}
