namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserQuestRewardSkillXp : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestRewardSkillXp();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgQuestRewardSkillXp AsPgQuestRewardSkillXp))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgQuestRewardSkillXp, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgQuestRewardSkillXp item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string SkillKey = Entry.Key;
                object Value = Entry.Value;

                PgSkill ParsedSkill = null;
                if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, SkillKey))
                    return false;

                if (!(Value is int XpValue))
                    return Program.ReportFailure($"Value '{Value}' was expected to be an int");

                item.XpTable.Add(ParsedSkill, XpValue);
            }

            return true;
        }
    }
}
