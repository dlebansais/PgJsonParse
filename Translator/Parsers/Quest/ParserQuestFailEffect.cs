namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserQuestFailEffect : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<QuestFailEffectType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestFailEffectType, VariadicObjectHandler>()
    {
        { QuestFailEffectType.ClearInteractionFlag, FinishItemClearInteractionFlag },
        { QuestFailEffectType.RingFailureMessage, FinishItemRingFailureMessage },
    };

    private static Dictionary<QuestFailEffectType, List<string>> KnownFieldTable = new Dictionary<QuestFailEffectType, List<string>>()
    {
        { QuestFailEffectType.ClearInteractionFlag, new List<string>() { "Type", "InteractionFlag" } },
        { QuestFailEffectType.RingFailureMessage, new List<string>() { "Type" } },
    };

    private static Dictionary<QuestFailEffectType, List<string>> HandledTable = new Dictionary<QuestFailEffectType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("Type"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Quest fail effect is missing a Type qualifier");

        object TypeValue = contentTable["Type"];

        if (!(TypeValue is string AsTypeString))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        if (!StringToEnumConversion<QuestFailEffectType>.TryParse(AsTypeString, out QuestFailEffectType failEffectType))
            return false;

        if (!HandlerTable.ContainsKey(failEffectType))
            return Program.ReportFailure(parsedFile, parsedKey, $"FailEffect {failEffectType} has no handler");

        Debug.Assert(KnownFieldTable.ContainsKey(failEffectType));

        VariadicObjectHandler Handler = HandlerTable[failEffectType];
        List<string> KnownFieldList = KnownFieldTable[failEffectType];
        List<string> UsedFieldList = new List<string>();

        if (!Handler(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
            return false;

        if (!HandledTable.ContainsKey(failEffectType))
            HandledTable.Add(failEffectType, new List<string>());

        List<string> ReportedFieldList = HandledTable[failEffectType];
        foreach (string FieldName in UsedFieldList)
            if (!ReportedFieldList.Contains(FieldName))
                ReportedFieldList.Add(FieldName);

        return true;
    }

    public static bool FinalizeParsing()
    {
        return Finalizer<QuestFailEffectType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemClearInteractionFlag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestFailEffectClearInteractionFlag NewItem = new PgQuestFailEffectClearInteractionFlag();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    case "InteractionFlag":
                        Result = SetStringProperty((string valueString) => NewItem.InteractionFlag = valueString, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            if (NewItem.InteractionFlag.Length == 0)
                return Program.ReportFailure(parsedFile, parsedKey, "Missing interactionFlag");

            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemRingFailureMessage(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestFailEffectRingFailureMessage NewItem = new PgQuestFailEffectRingFailureMessage();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "Type":
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }
}
