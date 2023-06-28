namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserQuestPreGiveEffect : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<QuestPreGiveEffectType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestPreGiveEffectType, VariadicObjectHandler>()
    {
        { QuestPreGiveEffectType.Effect, FinishItemEffect },
        { QuestPreGiveEffectType.Ability, FinishItemAbility },
        { QuestPreGiveEffectType.SetInteractionFlag, FinishItemSetInteractionFlag },
        { QuestPreGiveEffectType.ClearInteractionFlag, FinishItemClearInteractionFlag },
        { QuestPreGiveEffectType.Item, FinishItemItem },
    };

    private static Dictionary<QuestPreGiveEffectType, List<string>> KnownFieldTable = new Dictionary<QuestPreGiveEffectType, List<string>>()
    {
        { QuestPreGiveEffectType.Effect, new List<string>() { "T", "Description" } },
        { QuestPreGiveEffectType.Ability, new List<string>() { "T", "Ability" } },
        { QuestPreGiveEffectType.SetInteractionFlag, new List<string>() { "T", "InteractionFlag" } },
        { QuestPreGiveEffectType.ClearInteractionFlag, new List<string>() { "T", "InteractionFlag" } },
        { QuestPreGiveEffectType.Item, new List<string>() { "T", "Item" , "QuestGroup" } },
    };

    private static Dictionary<QuestPreGiveEffectType, List<string>> HandledTable = new Dictionary<QuestPreGiveEffectType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("T"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Quest reward is missing a Type qualifier");

        object TypeValue = contentTable["T"];

        if (!(TypeValue is string AsTypeString))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        if (!StringToEnumConversion<QuestPreGiveEffectType>.TryParse(AsTypeString, out QuestPreGiveEffectType rewardType))
            return false;

        if (!HandlerTable.ContainsKey(rewardType))
            return Program.ReportFailure(parsedFile, parsedKey, $"PreGiveEffect {rewardType} has no handler");

        Debug.Assert(KnownFieldTable.ContainsKey(rewardType));

        VariadicObjectHandler Handler = HandlerTable[rewardType];
        List<string> KnownFieldList = KnownFieldTable[rewardType];
        List<string> UsedFieldList = new List<string>();

        if (!Handler(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
            return false;

        if (!HandledTable.ContainsKey(rewardType))
            HandledTable.Add(rewardType, new List<string>());

        List<string> ReportedFieldList = HandledTable[rewardType];
        foreach (string FieldName in UsedFieldList)
            if (!ReportedFieldList.Contains(FieldName))
                ReportedFieldList.Add(FieldName);

        return true;
    }

    public static bool FinalizeParsing()
    {
        return Finalizer<QuestPreGiveEffectType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemEffect(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestPreGiveEffectSimple NewItem = new PgQuestPreGiveEffectSimple();

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
                    case "T":
                        break;
                    case "Description":
                        Result = SetStringProperty((string valueString) => NewItem.Description = valueString, Value);
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

    private static bool FinishItemAbility(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestPreGiveEffectLearnAbility NewItem = new PgQuestPreGiveEffectLearnAbility();

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
                    case "T":
                        break;
                    case "Ability":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => NewItem.Ability_Key = PgObject.GetItemKey(valueAbility), Value);
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

    private static bool FinishItemSetInteractionFlag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestPreGiveEffectSetInteractionFlag NewItem = new PgQuestPreGiveEffectSetInteractionFlag();

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
                    case "T":
                        break;
                    case "InteractionFlag":
                        Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.InteractionFlag = valueEnum, Value);
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

    private static bool FinishItemClearInteractionFlag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestPreGiveEffectClearInteractionFlag NewItem = new PgQuestPreGiveEffectClearInteractionFlag();

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
                    case "T":
                        break;
                    case "InteractionFlag":
                        Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.InteractionFlag = valueEnum, Value);
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

    private static bool FinishItemItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestPreGiveEffectItem NewItem = new PgQuestPreGiveEffectItem();

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
                    case "T":
                        break;
                    case "Item":
                        Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = PgObject.GetItemKey(valueItem), Value);
                        break;
                    case "QuestGroup":
                        Result = StringToEnumConversion<QuestGroup>.SetEnum((QuestGroup valueEnum) => NewItem.QuestGroup = valueEnum, Value);
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
