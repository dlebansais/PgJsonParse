namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserRewardList : Parser
{
    public override object CreateItem()
    {
        return new PgRewardList();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgRewardList AsPgRewardList)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgRewardList, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgRewardList item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            string[] Split = Key.Split('_');

            string EntryLevelString = Split[0];
            if (!int.TryParse(EntryLevelString, out int EntryLevel))
                return Program.ReportFailure($"Invalid skill reward level '{EntryLevelString}'");

            List<Race> EntryRaceList = new List<Race>();
            for (int i = 1; i < Split.Length; i++)
            {
                string EntryRaceString = Split[i];
                if (!StringToEnumConversion<Race>.TryParse(EntryRaceString, out Race EntryRace))
                    return Program.ReportFailure($"Invalid skill reward race '{EntryRaceString}'");

                EntryRaceList.Add(EntryRace);
            }

            foreach (PgReward Reward in item.List)
            {
                bool IsFound = false;
                foreach (Race Race in EntryRaceList)
                    IsFound |= Reward.RaceRestrictionList.Contains(Race);

                if (IsFound && Reward.RewardLevel == EntryLevel)
                    return Program.ReportFailure($"Level {EntryLevel} already added");
            }

            if (!(Value is ParsingContext Context))
                return Program.ReportFailure($"Value '{Value}' was expected to be a context");

            if (!(Context.Item is PgReward AsReward))
                return Program.ReportFailure($"Object '{Value}' was unexpected");

            AsReward.RawRewardLevel = EntryLevel;
            AsReward.RaceRestrictionList.AddRange(EntryRaceList);

            item.List.Add(AsReward);
        }

        return true;
    }
}
