namespace Translator
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using PgJsonReader;
    using PgObjects;

    public class ParserAttribute : Parser
    {
        public override object CreateItem()
        {
            return new PgAttribute();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgAttribute AsPgAttribute)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAttribute, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAttribute item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
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
                        Result = StringToEnumConversion<DisplayType>.SetEnum((DisplayType valueEnum) => item.DisplayType = valueEnum, Value);
                        break;
                    case "IsHidden":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsHidden = valueBool, Value);
                        break;
                    case "DisplayRule":
                        Result = StringToEnumConversion<DisplayRule>.SetEnum((DisplayRule valueEnum) => item.DisplayRule = valueEnum, Value);
                        break;
                    case "DefaultValue":
                        Result = SetFloatProperty((float valueFloat) => item.RawDefaultValue = valueFloat, Value);
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

        public static void UpdateIconsAndNames()
        {
            Dictionary<string, ParsingContext> AttributeParsingTable = ParsingContext.ObjectKeyTable[typeof(PgAttribute)];
            foreach (KeyValuePair<string, ParsingContext> Entry in AttributeParsingTable)
            {
                PgAttribute Attribute = (PgAttribute)Entry.Value.Item;

                if (Attribute.IconIdList.Count > 0)
                    Attribute.IconId = Attribute.IconIdList[0];
                else
                    Attribute.IconId = PgObject.AttributeIconId;

                if (Attribute.Label.Length > 0)
                    Attribute.ValidLabel = Attribute.Label;
                else
                    Attribute.ValidLabel = Attribute.Key;

                Debug.Assert(Attribute.ObjectIconId != 0);
                Debug.Assert(Attribute.ObjectName.Length > 0);
            }
        }
    }
}
