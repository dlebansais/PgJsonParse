namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserAttribute : Parser
    {
        public override object CreateItem()
        {
            return new PgAttribute();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgAttribute AsPgAttribute))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAttribute, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAttribute item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Label":
                        Result = SetStringProperty((string valueString) => item.Label = valueString, Value);
                        break;
                    case "IconIds":
                        Result = ParseIcondIds(item, Value, parsedFile, parsedKey);
                        break;
                    case "Tooltip":
                        Result = SetStringProperty((string valueString) => item.Tooltip = valueString, Value);
                        break;
                    case "DisplayType":
                        Result = Inserter<DisplayType>.SetEnum((DisplayType valueEnum) => item.DisplayType = valueEnum, Value);
                        break;
                    case "IsHidden":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsHidden = valueBool, Value);
                        break;
                    case "DisplayRule":
                        Result = Inserter<DisplayRule>.SetEnum((DisplayRule valueEnum) => item.DisplayRule = valueEnum, Value);
                        break;
                    case "DefaultValue":
                        Result = SetFloatProperty((float valueFloat) => item.RawDefaultValue = valueFloat, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }

        private bool ParseIcondIds(PgAttribute item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> AsObjectList))
                return Program.ReportFailure(parsedFile, parsedKey, "Array of int expected for icon IDs");

            foreach (object ObjectItem in AsObjectList)
            {
                if (!(ObjectItem is int AsInt))
                    return Program.ReportFailure(parsedFile, parsedKey, "Int expected for an icon ID");

                if (item.IconIdList.Contains(AsInt))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Duplicate icon ID {AsInt}");

                item.IconIdList.Add(AsInt);
            }

            return true;
        }
    }
}
