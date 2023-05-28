namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserAbilityAmmo : Parser
{
    public override object CreateItem()
    {
        return new PgAbilityAmmo();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAbilityAmmo AsPgAbilityAmmo)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgAbilityAmmo, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAbilityAmmo item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "ItemKeyword":
                    Result = StringToEnumConversion<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => item.ItemKeyword = valueEnum, Value);
                    break;
                case "Count":
                    Result = SetIntProperty((int valueInt) => item.RawCount = valueInt, Value);
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
