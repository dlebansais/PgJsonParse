namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserSpecialValue : Parser
    {
        public override object CreateItem()
        {
            return new PgSpecialValue();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgSpecialValue AsPgSpecialValue)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgSpecialValue, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgSpecialValue item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
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
                    case "Suffix":
                        Result = SetStringProperty((string valueString) => item.Suffix = valueString, Value);
                        break;
                    case "Value":
                        Result = SetFloatProperty((float valueFloat) => item.RawValue = valueFloat, Value);
                        break;
                    case "AttributesThatDelta":
                        Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatDeltaList, Value);
                        break;
                    case "AttributesThatMod":
                        Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatModList, Value);
                        break;
                    case "AttributesThatModBase":
                        Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatModBaseList, Value);
                        break;
                    case "DisplayType":
                        Result = StringToEnumConversion<DisplayType>.SetEnum((DisplayType valueEnum) => item.DisplayType = valueEnum, Value);
                        break;
                    case "SkipIfZero":
                        Result = SetBoolProperty((bool valueBool) => item.RawSkipIfZero = valueBool, Value);
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
    }
}
