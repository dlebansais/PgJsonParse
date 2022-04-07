namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserDirectedGoal : Parser
    {
        public override object CreateItem()
        {
            return new PgDirectedGoal();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgDirectedGoal AsPgDirectedGoal)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgDirectedGoal, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgDirectedGoal item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Id":
                        Result = SetIntProperty((int valueInt) => item.Key = valueInt.ToString(), Value);
                        break;
                    case "Label":
                        Result = SetStringProperty((string valueString) => item.Label = valueString, Value);
                        break;
                    case "Zone":
                        Result = SetStringProperty((string valueString) => item.Zone = valueString, Value);
                        break;
                    case "IsCategoryGate":
                        Result = ParseIsCategoryGate(contentTable, parsedFile, parsedKey);
                        break;
                    case "LargeHint":
                        Result = SetStringProperty((string valueString) => item.LargeHint = Tools.CleanedUpString(valueString), Value);
                        break;
                    case "SmallHint":
                        Result = SetStringProperty((string valueString) => item.SmallHint = Tools.CleanedUpString(valueString), Value);
                        break;
                    case "CategoryGateId":
                        Result = ParseCategoryGateId(item, Value, contentTable, parsedFile, parsedKey);
                        break;
                    case "ForRaces":
                        Result = StringToEnumConversion<Race>.TryParseList(Value, item.ForRaceList);
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

        private bool ParseIsCategoryGate(Dictionary<string, object> contentTable, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("CategoryGateId"))
                return Program.ReportFailure(parsedFile, parsedKey, $"Directed Goal is a Category Gate when it points to another");

            return true;
        }

        private bool ParseCategoryGateId(PgDirectedGoal item, object value, Dictionary<string, object> contentTable, string parsedFile, string parsedKey)
        {
            if (contentTable.ContainsKey("IsCategoryGate"))
                return Program.ReportFailure(parsedFile, parsedKey, "Directed Goal is a Category Gate when it points to another");

            if (!(value is int AsInt))
                return Program.ReportFailure(parsedFile, parsedKey, "Int value expected for a Category Gate ID");

            return Inserter<PgDirectedGoal>.SetItemById((PgDirectedGoal valueDirectedGoal) => item.CategoryGate_Key = valueDirectedGoal.Key, AsInt);
        }
    }
}
