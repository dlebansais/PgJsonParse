namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserQuestRewardItem : Parser
{
    public override object CreateItem()
    {
        return new PgQuestRewardItem();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgQuestRewardItem AsPgQuestRewardItem)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgQuestRewardItem, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgQuestRewardItem item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Item":
                    Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => item.Item_Key = Parser.GetItemKey(valueItem), Value);
                    break;
                case "StackSize":
                    Result = SetIntProperty((int valueInt) => item.RawStackSize = valueInt, Value);
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
