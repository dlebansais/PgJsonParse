﻿namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserRecipeItem : Parser
{
    public override object CreateItem()
    {
        return new PgRecipeItem();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgRecipeItem AsPgRecipeItem)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgRecipeItem, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgRecipeItem item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "ItemCode":
                    Result = Inserter<PgItem>.SetItemByKey((PgItem valueItem) => item.Item_Key = PgObject.GetItemKey(valueItem), Value.ToString());
                    break;
                case "StackSize":
                    Result = SetIntProperty((int valueInt) => item.RawStackSize = valueInt, Value);
                    break;
                case "PercentChance":
                    Result = SetIntProperty((int valueInt) => item.RawPercentChance = valueInt, Value);
                    break;
                case "ItemKeys":
                    Result = StringToEnumConversion<RecipeItemKey>.TryParseList(Value, item.ItemKeyList);
                    break;
                case "Description":
                    Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                    break;
                case "ChanceToConsume":
                    Result = SetIntProperty((int valueInt) => item.RawChanceToConsume = valueInt, Value);
                    break;
                case "DurabilityConsumed":
                    Result = SetIntProperty((int valueInt) => item.RawDurabilityConsumed = valueInt, Value);
                    break;
                case "AttuneToCrafter":
                    Result = SetBoolProperty((bool valueBool) => item.RawAttuneToCrafter = valueBool, Value);
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
            if (item.Item_Key == null && item.ItemKeyList.Count == 0)
                return Program.ReportFailure(parsedFile, parsedKey, "Missing item or item keys");

            if (item.Item_Key != null && item.ItemKeyList.Count > 0)
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent item or item keys");

            if (item.ItemKeyList.Count > 0 && item.Description.Length == 0)
                return Program.ReportFailure(parsedFile, parsedKey, "Empty Description");

            if (item.ItemKeyList.Count == 0 && item.Description.Length > 0)
                return Program.ReportFailure(parsedFile, parsedKey, "Unexpected Description");

            if ((item.RawPercentChance.HasValue && item.RawChanceToConsume.HasValue) || (item.RawPercentChance.HasValue && item.RawDurabilityConsumed.HasValue) || (item.RawChanceToConsume.HasValue && item.RawDurabilityConsumed.HasValue))
                return Program.ReportFailure(parsedFile, parsedKey, "Inconsistent percentage");
        }

        return Result;
    }
}
