namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserReportList : Parser
    {
        public override object CreateItem()
        {
            return new PgReportList();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgReportList AsPgReportList))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgReportList, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgReportList item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!int.TryParse(Key, out int EntryLevel))
                    return Program.ReportFailure($"Invalid report key '{Key}'");

                if (!(Value is string EntryString))
                    return Program.ReportFailure($"Invalid report value '{Value}'");

                item.List.Add(new PgReport() { RawReportLevel = EntryLevel, Text = EntryString });
            }

            return true;
        }
    }
}
