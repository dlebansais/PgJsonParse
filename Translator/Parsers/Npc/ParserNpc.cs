﻿namespace Translator
{
    using System;
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserNpc : Parser
    {
        public override object CreateItem()
        {
            return new PgNpc();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgNpc AsPgNpc)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgNpc, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgNpc item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
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

            if (Result)
            {
                if (item.AreaFriendlyName == null)
                    return Program.ReportFailure(parsedFile, parsedKey, "No area friendly name");

                item.IconId = PgObject.NpcIconId;
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
            return StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => item.AreaName = valueEnum, ValueAreaName);
        }
    }
}
