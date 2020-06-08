namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserQuestRewardItem : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestRewardItem();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgQuestRewardItem AsPgQuestRewardItem))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgQuestRewardItem, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgQuestRewardItem item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Item":
                        //Result = Inserter<PgItem>.SetItemProperty((PgItem valueItem) => item.QuestItem = valueItem, Value);
                        Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => item.QuestItem = valueItem, Value);
                        break;
                    case "StackSize":
                        Result = SetIntProperty((int valueInt) => item.RawStackSize = valueInt, Value);
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
    }
}
