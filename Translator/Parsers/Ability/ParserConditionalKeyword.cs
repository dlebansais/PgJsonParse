namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserConditionalKeyword : Parser
{
    public override object CreateItem()
    {
        return new PgConditionalKeyword();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgConditionalKeyword AsPgConditionalKeyword)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgConditionalKeyword, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgConditionalKeyword item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        string? KeywordRead = null;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "IsDefault":
                    Result = SetBoolProperty((bool valueBool) => item.RawIsDefault = valueBool, Value);
                    break;
                case "EffectKeywordMustExist":
                    Result = StringToEnumConversion<EffectKeyword>.SetEnum((EffectKeyword valueEnum) => item.EffectKeywordMustExist = valueEnum, Value);
                    break;
                case "EffectKeywordMustNotExist":
                    Result = StringToEnumConversion<EffectKeyword>.SetEnum((EffectKeyword valueEnum) => item.EffectKeywordMustNotExist = valueEnum, Value);
                    break;
                case "Keyword":
                    KeywordRead = Value as string;
                    Result = StringToEnumConversion<AbilityKeyword>.SetEnum((AbilityKeyword valueEnum) => item.Keyword = valueEnum, Value);
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
            if (item.Keyword == AbilityKeyword.Internal_None)
            {
                Program.ReportFailure($"Keyword '{KeywordRead}' expected in ConditionalKeyword");
            }
        }

        return Result;
    }
}
