namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserAdvancementTable : Parser
    {
        public override object CreateItem()
        {
            return new PgAdvancementTable();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgAdvancementTable AsPgAdvancementTable))
                return Program.ReportFailure("Unexpected failure");

            AsPgAdvancementTable.Key = objectKey;

            string[] Split = objectKey.Split('_');
            if (Split.Length != 2)
                return Program.ReportFailure($"Invalid advancement table key '{objectKey}'");

            AsPgAdvancementTable.InternalName = Split[1];

            return FinishItem(AsPgAdvancementTable, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAdvancementTable item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string LevelKey = Entry.Key;
                object Value = Entry.Value;

                if (!LevelKey.StartsWith("Level_"))
                    return Program.ReportFailure($"Invalid advancement key format '{LevelKey}'");

                if (!int.TryParse(LevelKey.Substring(6), out int EntryLevel))
                    return Program.ReportFailure($"Invalid level in key '{LevelKey}'");

                if (item.LevelTable.ContainsKey(EntryLevel))
                    return Program.ReportFailure($"Level {EntryLevel} already added");

                if (!(Value is ParsingContext Context))
                    return Program.ReportFailure($"Value '{Value}' was expected to be a context");

                if (!(Context.Item is PgAdvancement AsAdvancement))
                    return Program.ReportFailure($"Object '{Value}' was unexpected");

                item.LevelTable.Add(EntryLevel, AsAdvancement);
            }

            return true;
        }
    }
}
