namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserItemSkillLink : Parser
{
    public override object CreateItem()
    {
        return new PgItemSkillLink();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgItemSkillLink AsPgItemSkillLink)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgItemSkillLink, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgItemSkillLink item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (contentTable.Count == 0)
            return Program.ReportFailure(parsedFile, parsedKey, $"At least one skill expected in a skill link");

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            PgSkill ParsedSkill = null!;
            if (!ParserSkill.Parse((PgSkill valueSkill) => ParsedSkill = valueSkill, Key, parsedFile, parsedKey))
                return false;

            int ParsedLevel = -1;
            if (!SetIntProperty((int valueInt) => ParsedLevel = valueInt, Value))
                return false;

            if (item.SkillTable.ContainsKey(ParsedSkill.Key))
                return Program.ReportFailure(parsedFile, parsedKey, $"Skill {Key} already parsed in skill link");

            item.SkillTable.Add(ParsedSkill.Key, ParsedLevel);
        }

        return true;
    }
}
