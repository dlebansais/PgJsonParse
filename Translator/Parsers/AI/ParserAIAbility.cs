namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserAIAbility : Parser
    {
        public override object CreateItem()
        {
            return new PgAIAbility();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgAIAbility AsPgAIAbility))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAIAbility, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAIAbility item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "minLevel":
                        Result = SetIntProperty((int valueInt) => item.RawMinLevel = valueInt, Value);
                        break;
                    case "maxLevel":
                        Result = SetIntProperty((int valueInt) => item.RawMaxLevel = valueInt, Value);
                        break;
                    case "minDistance":
                        Result = SetIntProperty((int valueInt) => item.RawMinDistance = valueInt, Value);
                        break;
                    case "minRange":
                        Result = SetIntProperty((int valueInt) => item.RawMinRange = valueInt, Value);
                        break;
                    case "maxRange":
                        Result = SetIntProperty((int valueInt) => item.RawMaxRange = valueInt, Value);
                        break;
                    case "cue":
                        Result = Inserter<AbilityCue>.SetEnum((AbilityCue valueEnum) => item.Cue = valueEnum, Value);
                        break;
                    case "cueVal":
                        Result = SetIntProperty((int valueInt) => item.RawCueValue = valueInt, Value);
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
