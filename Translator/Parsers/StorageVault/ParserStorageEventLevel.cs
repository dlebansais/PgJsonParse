namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserStorageEventLevel : Parser
    {
        public override object CreateItem()
        {
            return new PgStorageEventList();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgStorageEventList AsPgStorageEventLevel)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgStorageEventLevel, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgStorageEventList item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!StringToEnumConversion<EventLevel>.TryParse(Key, out EventLevel Event))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Unknown favor level '{Key}'");

                if (!(Value is int SlotCount))
                    return Program.ReportFailure($"Value {Value} was expected to be an int");

                item.EventTable.Add(Event, SlotCount);
            }

            return true;
        }
    }
}
