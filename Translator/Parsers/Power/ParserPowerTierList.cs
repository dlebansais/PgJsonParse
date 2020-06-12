namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserPowerTierList : Parser
    {
        public override object CreateItem()
        {
            return new PgPowerTierList();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgPowerTierList AsPgPowerTierList))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgPowerTierList, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgPowerTierList item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!Key.StartsWith("id_") || !int.TryParse(Key.Substring(3), out int Level))
                    return Program.ReportFailure($"Invalid power tier key '{Key}'");

                if (!(Value is ParsingContext Context))
                    return Program.ReportFailure($"Value '{Value}' was expected to be a context");

                if (!(Context.Item is PgPowerTier AsPowerTier))
                    return Program.ReportFailure($"Object '{Value}' was unexpected");

                item.TierTable.Add(Level, AsPowerTier);
            }

            return true;
        }
    }
}
