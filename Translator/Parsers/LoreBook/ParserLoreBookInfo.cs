namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserLoreBookInfo : Parser
    {
        public override object CreateItem()
        {
            return new PgLoreBookInfo();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgLoreBookInfo AsPgLoreBookInfo))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgLoreBookInfo, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgLoreBookInfo item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!(Value is ParsingContext Context))
                    return Program.ReportFailure($"Value '{Value}' was expected to be a context");

                if (!(Context.Item is PgLoreBookInfoCategory AsCategory))
                    return Program.ReportFailure($"Object '{Value}' was unexpected");

                AsCategory.Key = Key;
                item.Categories.Add(AsCategory);
            }

            return true;
        }
    }
}
