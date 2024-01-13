namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserSource : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("Type"))
            return Program.ReportFailure(parsedFile, parsedKey, "Source has no type");

        if (!(contentTable["Type"] is string TypeString))
            return Program.ReportFailure("Source type was expected to be a string");

        bool Result;

        switch (TypeString)
        {
            case "Skill":
                Result = ParseSourceAutomaticFromSkill(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
                break;
            case "Item":
                Result = ParseSourceItem(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
                break;
            case "Training":
                Result = ParseSourceTraining(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
                break;
            case "Effect":
                Result = ParseSourceEffect(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
                break;
            case "Quest":
                Result = ParseSourceQuest(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
                break;
            case "NpcGift":
                Result = ParseSourceGift(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
                break;
            case "HangOut":
                Result = ParseSourceHangOut(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
                break;
            default:
                Result = Program.ReportFailure(parsedFile, parsedKey, $"Unnown source type '{TypeString}'");
                break;
        }

        Debug.Assert(!Result || item is PgSource);

        if (item is PgSource NewItem)
            NewItem.SourceKey = parsedKey;

        return Result;
    }

    private bool ParseSourceAutomaticFromSkill(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgSourceAutomaticFromSkill NewSource = new PgSourceAutomaticFromSkill();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Type":
                    break;
                case "Skill":
                    Result = ParserSkill.Parse((PgSkill valueSkill) => NewSource.Skill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
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
            item = NewSource;
            return true;
        }
        else
            return false;
    }

    private bool ParseSourceItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgSourceItem NewSource = new PgSourceItem();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Type":
                    break;
                case "ItemTypeId":
                    Result = Inserter<PgItem>.SetItemByKey((PgItem valueItem) => NewSource.Item_Key = PgObject.GetItemKey(valueItem), Value.ToString());
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
            item = NewSource;
            return true;
        }
        else
            return false;
    }

    private bool ParseSourceTraining(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgSourceTraining NewSource = new PgSourceTraining();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Type":
                    break;
                case "Npc":
                    Result = Inserter<PgSource>.SetNpc((PgNpcLocation location) => NewSource.Npc = location, Value, parsedFile, parsedKey);
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
            item = NewSource;
            return true;
        }
        else
            return false;
    }

    private bool ParseSourceEffect(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        item = new PgSourceEffect() { Effect_Key = null };
        return true;
    }

    private bool ParseSourceQuest(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgSourceQuest NewSource = new PgSourceQuest();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Type":
                    break;
                case "QuestId":
                    Result = Inserter<PgQuest>.SetItemByKey((PgQuest valueQuest) => NewSource.Quest_Key = PgObject.GetItemKey(valueQuest), Value.ToString());
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
            item = NewSource;
            return true;
        }
        else
            return false;
    }

    private bool ParseSourceGift(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgSourceGift NewSource = new PgSourceGift();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Type":
                    break;
                case "Npc":
                    Result = Inserter<PgSource>.SetNpc((PgNpcLocation location) => NewSource.Npc = location, Value, parsedFile, parsedKey);
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
            item = NewSource;
            return true;
        }
        else
            return false;
    }

    private bool ParseSourceHangOut(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgSourceHangOut NewSource = new PgSourceHangOut();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Type":
                    break;
                case "Npc":
                    Result = Inserter<PgSource>.SetNpc((PgNpcLocation location) => NewSource.Npc = location, Value, parsedFile, parsedKey);
                    break;
                case "HangOutId":
                    Result = SetIntProperty((int valueInt) => NewSource.RawHangOut = valueInt, Value);
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
            item = NewSource;
            return true;
        }
        else
            return false;
    }
}
