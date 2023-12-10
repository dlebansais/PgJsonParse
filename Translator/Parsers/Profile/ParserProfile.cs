namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserProfile : Parser
{
    public override object CreateItem()
    {
        return new PgProfile();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgProfile AsPgProfile)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgProfile, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgProfile item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "EffectList":
                    Result = ParseEffectList(item, Value);
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

    private bool ParseEffectList(PgProfile item, object value)
    {
        if (value is not IEnumerable<object> CollectionValue)
            return Program.ReportFailure($"Value '{value}' was expected to be a list of strings");

        foreach (object Item in CollectionValue)
        {
            if (Item is not string StringItem)
                return Program.ReportFailure($"Item '{item}' was expected to be a string");

            PgPower? ItemPower = null;
            if (!Inserter<PgPower>.SetItemByInternalName((PgPower valuePower) => ItemPower = valuePower, StringItem) || ItemPower is null)
                return Program.ReportFailure($"Item '{item}' was expected to be the internal name of a mod");

            string ItemKey = PgObject.GetItemKey(ItemPower);

            item.EffectList_Keys.Add(ItemKey);
        }

        return true;
    }

    public static void UpdateIconsAndNames()
    {
        Dictionary<string, ParsingContext> ProfileParsingTable = ParsingContext.ObjectKeyTable[typeof(PgProfile)];
        foreach (KeyValuePair<string, ParsingContext> Entry in ProfileParsingTable)
        {
            PgProfile Profile = (PgProfile)Entry.Value.Item;
            UpdateIconsAndNames(Profile);

            Debug.Assert(Profile.ObjectIconId != 0);
            Debug.Assert(Profile.ObjectName.Length > 0);
        }
    }

    private static void UpdateIconsAndNames(PgProfile profile)
    {
        if (profile.EffectList_Keys.Count > 0)
        {
            string FirstKey = profile.EffectList_Keys[0].Substring(1);
            Dictionary<string, ParsingContext> ProfileParsingTable = ParsingContext.ObjectKeyTable[typeof(PgPower)];
            PgPower FirstPower = (PgPower)ProfileParsingTable[FirstKey].Item;

            profile.IconId = FirstPower.IconId;
        }
    }
}
