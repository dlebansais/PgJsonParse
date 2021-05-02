namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserReportList : Parser
    {
        public override object CreateItem()
        {
            return new PgReportList();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgReportList AsPgReportList)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgReportList, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgReportList item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!int.TryParse(Key, out int EntryLevel))
                    return Program.ReportFailure($"Invalid report key '{Key}'");

                if (!(Value is string EntryString))
                    return Program.ReportFailure($"Invalid report value '{Value}'");

                PgReport NewReport = new PgReport() { RawReportLevel = EntryLevel, Text = EntryString };
                ParsingContext.AddSuplementaryObject(NewReport);
                item.List.Add(NewReport);
            }

            return true;
        }
    }
}
