namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserAdvancementHint : Parser
    {
        public override object CreateItem()
        {
            return new PgAdvancementHint();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgAdvancementHint AsPgAdvancementHint)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAdvancementHint, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAdvancementHint item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!int.TryParse(Key, out int EntryLevel))
                    return Program.ReportFailure($"Invalid skill hint level '{Key}'");

                if (item.HintTable.ContainsKey(EntryLevel))
                    return Program.ReportFailure($"Level {EntryLevel} already added");

                if (!(Value is string HintString))
                    return Program.ReportFailure($"Value '{Value}' was expected to be a string");

                item.HintTable.Add(EntryLevel, HintString);
            }

            return true;
        }
    }
}
