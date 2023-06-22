namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserPlayerTitle : Parser
{
    public override object CreateItem()
    {
        return new PgPlayerTitle();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgPlayerTitle AsPgPlayerTitle)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgPlayerTitle, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgPlayerTitle item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Title":
                    Result = ParseTitle(item, Value, parsedFile, parsedKey);
                    break;
                case "Tooltip":
                    Result = SetStringProperty((string valueString) => item.Tooltip = valueString, Value);
                    break;
                case "Keywords":
                    Result = StringToEnumConversion<TitleKeyword>.TryParseList(Value, item.KeywordList);
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

    private bool ParseTitle(PgPlayerTitle item, object value, string parsedFile, string parsedKey)
    {
        if (!(value is string ValueTitle))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

        item.Title = StripStringTags(ValueTitle);
        return true;
    }

    private string StripStringTags(string s)
    {
        int TagStartIndex, TagEndIndex;

        while ((TagStartIndex = s.IndexOf('<')) >= 0 && (TagEndIndex = s.IndexOf('>', TagStartIndex)) >= 0)
        {
            s = s.Substring(0, TagStartIndex) + s.Substring(TagEndIndex + 1);
        }

        return s;
    }

    public static readonly Dictionary<string, string> TitleToKeyMap = new Dictionary<string, string>()
    {
        { "Event_Halloween_CultistOfZhiaLian", "5009" },
        { "Event_Halloween_SeniorCultistOfZhiaLian", "5010" },
        { "IncredibleGoblinKissAss", "5012" },
        { "GuideEvent_CivilServant", "5210" },
        { "GuideEvent_IKnowBunFu", "5211" },
        { "GuideEvent_SaviorOfTheGoats", "5208" },
        { "GuideEvent_AntiSaviorOfTheGoats", "5216" },
        { "Event_Halloween_HeartBeater", "5015" },
        { "Event_Halloween_Riiiiiiiiiii", "5017" },
        { "GuideEvent_TurkeyWrangler", "5212" },
        { "GuideEvent_ALittleFruity", "5219" },
        { "Warsmith", "5018" },
        { "GuideEvent_LikeABoss", "5222" },
        { "Event_TurkeyKiller", "5223" },
        { "GuideEvent_EggsellentHunter", "5209" },
        { "Event_Halloween_NotAfraidOfLungs", "5024" },
    };
}
