namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System;
    using System.Collections.Generic;

    public class ParserNpc : Parser
    {
        public override object CreateItem()
        {
            return new PgNpc();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgNpc AsPgNpc))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgNpc, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgNpc item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Name":
                        Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                        break;
                    case "AreaName":
                        Result = ParseAreaName(item, Value, parsedFile, parsedKey);
                        break;
                    case "AreaFriendlyName":
                        Result = SetStringProperty((string valueString) => item.AreaFriendlyName = valueString, Value);
                        break;
                    case "Preferences":
                        Result = Inserter<PgNpcPreference>.AddKeylessArray(item.PreferenceList, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }

        private bool ParseAreaName(PgNpc item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueKey))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            if (!ValueKey.StartsWith("Area"))
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid area name '{ValueKey}'");

            string ValueAreaName = ValueKey.Substring(4);
            return Inserter<MapAreaName>.SetEnum((MapAreaName valueEnum) => item.AreaName = valueEnum, ValueAreaName);
        }
    }
}
