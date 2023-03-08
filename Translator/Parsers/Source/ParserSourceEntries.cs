﻿namespace Translator
{
    using System;
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserSourceEntries : Parser
    {
        public static bool Parse(Action<PgSourceEntries> setter, object value, string parsedFile, string parsedKey, ErrorControl errorControl = ErrorControl.Normal)
        {
            if (!(value is string ValueKey))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            return Inserter<PgSourceEntries>.SetItemByKey(setter, ValueKey, errorControl);
        }

        public override object CreateItem()
        {
            return new PgSourceEntries();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgSourceEntries AsPgSourceEntries)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgSourceEntries, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgSourceEntries item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "entries":
                        Result = Inserter<PgSource>.AddKeylessArray(item.EntryList, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            if (Result)
            {
            }

            return Result;
        }
    }
}