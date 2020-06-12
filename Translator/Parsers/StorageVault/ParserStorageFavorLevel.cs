namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserStorageFavorLevel : Parser
    {
        public override object CreateItem()
        {
            return new PgStorageFavorLevel();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgStorageFavorLevel AsPgStorageFavorLevel))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgStorageFavorLevel, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgStorageFavorLevel item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!StringToEnumConversion<Favor>.TryParse(Key, out Favor Favor))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Unknown favor level '{Key}'");

                if (!(Value is int SlotCount))
                    return Program.ReportFailure($"Value {Value} was expected to be an int");

                item.LevelTable.Add(Favor, SlotCount);
            }

            return true;
        }
    }
}
