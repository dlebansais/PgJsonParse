namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserSpecialValue : Parser
    {
        public override object CreateItem()
        {
            return new PgSpecialValue();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgSpecialValue AsPgSpecialValue))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgSpecialValue, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgSpecialValue item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
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
                        Result = Inserter<DisplayType>.SetEnum((DisplayType valueEnum) => item.DisplayType = valueEnum, Value);
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
