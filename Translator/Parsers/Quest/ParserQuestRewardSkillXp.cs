namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserQuestRewardSkillXp : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        PgQuestRewardCollection NewRewardCollection = new PgQuestRewardCollection();
        if (!FinishItem(NewRewardCollection, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey))
            return false;

        item = NewRewardCollection;
        return true;
    }

    private bool FinishItem(PgQuestRewardCollection item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string SkillKey = Entry.Key;
            object Value = Entry.Value;

            PgSkill ParsedSkill = null!;
            if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, SkillKey))
                return false;

            if (!(Value is int XpValue))
                return Program.ReportFailure($"Value '{Value}' was expected to be an int");

            PgQuestRewardSkillXp NewReward = new PgQuestRewardSkillXp() { Skill_Key = ParsedSkill.Key, RawXp = XpValue };
            item.Add(NewReward);
        }

        return true;
    }
}
