namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserItemEffect : Parser
{
    public override object CreateItem()
    {
        return new PgItemEffect();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgItemEffect AsPgItemEffect)
            return Program.ReportFailure("Unexpected failure");

        if (FinishItem(ref AsPgItemEffect, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey))
        {
            item = AsPgItemEffect;
            return true;
        }

        return false;
    }

    private bool FinishItem(ref PgItemEffect itemEffect, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        string? Description = null;
        PgAttribute? ParsedAttribute = null;
        float? ParsedEffect = null;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Description":
                    Result = SetStringProperty((string valueString) => Description = valueString, Value);
                    break;
                case "AttributeName":
                    Result = Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, Value);
                    break;
                case "AttributeEffect":
                    Result = SetFloatProperty((float valueFloat) => ParsedEffect = valueFloat, Value);
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
            if (Description is not null)
                itemEffect = new PgItemEffectSimple() { Description = Description };
            else if (ParsedAttribute is not null && ParsedEffect is not null)
                itemEffect = new PgItemEffectAttribute() { Attribute_Key = PgObject.GetItemKey(ParsedAttribute), AttributeEffect = ParsedEffect.Value, Label = ParsedAttribute.Label };
            else
                Result = Program.ReportFailure(parsedFile, parsedKey, "ItemEffect has no appearance");
        }

        return Result;
    }
}
