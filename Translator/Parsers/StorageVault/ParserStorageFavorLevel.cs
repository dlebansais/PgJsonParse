namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserStorageFavorLevel : Parser
    {
        public override object CreateItem()
        {
            return new PgStorageFavorLevel();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgStorageFavorLevel AsPgStorageFavorLevel)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgStorageFavorLevel, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgStorageFavorLevel item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!StringToEnumConversion<Favor>.TryParse(Key, out Favor Favor))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Unknown favor level '{Key}'");

                if (!(Value is int SlotCount))
                    return Program.ReportFailure($"Value {Value} was expected to be an int");

                item.LevelTable.Add(new PgFavorSlotPair() { Favor = Favor, SlotCount = SlotCount });
            }

            item.LevelTable.Sort((PgFavorSlotPair p1, PgFavorSlotPair p2) => p1.SlotCount - p2.SlotCount);

            return true;
        }
    }
}
