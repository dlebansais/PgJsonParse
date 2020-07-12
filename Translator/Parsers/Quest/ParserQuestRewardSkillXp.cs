namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserQuestRewardSkillXp : Parser
    {
        public override object CreateItem()
        {
            return null;
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (item != null)
                return Program.ReportFailure("Unexpected failure");

            PgQuestRewardCollection NewRewardCollection = new PgQuestRewardCollection();
            if (!FinishItem(NewRewardCollection, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey))
                return false;

            item = NewRewardCollection;
            return true;
        }

        private bool FinishItem(PgQuestRewardCollection item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
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

                PgQuestRewardSkillXp NewReward = new PgQuestRewardSkillXp() { Skill = ParsedSkill, RawXp = XpValue };
                item.Add(NewReward);
            }

            return true;
        }
    }
}
