namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParserEffect : Parser
    {
        public override object CreateItem()
        {
            return new PgEffect();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgEffect AsPgEffect))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgEffect, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgEffect item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
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
                    case "Desc":
                        Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                        break;
                    case "IconId":
                        Result = SetIntProperty((int valueInt) => item.RawIconId = valueInt, Value);
                        break;
                    case "DisplayMode":
                        Result = StringToEnumConversion<EffectDisplayMode>.SetEnum((EffectDisplayMode valueEnum) => item.DisplayMode = valueEnum, Value);
                        break;
                    case "SpewText":
                        Result = SetStringProperty((string valueString) => item.SpewText = valueString, Value);
                        break;
                    case "Particle":
                        Result = StringToEnumConversion<EffectParticle>.SetEnum((EffectParticle valueEnum) => item.Particle = valueEnum, Value);
                        break;
                    case "StackingType":
                        Result = StringToEnumConversion<EffectStackingType>.SetEnum((EffectStackingType valueEnum) => item.StackingType = valueEnum, Value);
                        break;
                    case "StackingPriority":
                        Result = SetIntProperty((int valueInt) => item.RawStackingPriority = valueInt, Value);
                        break;
                    case "Duration":
                        ParseDuration(item, Value, parsedFile, parsedKey);
                        break;
                    case "Keywords":
                        Result = StringToEnumConversion<EffectKeyword>.TryParseList(Value, item.KeywordList);
                        break;
                    case "AbilityKeywords":
                        Result = StringToEnumConversion<AbilityKeyword>.TryParseList(Value, item.AbilityKeywordList);
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

        private bool ParseDuration(PgEffect item, object value, string parsedFile, string parsedKey)
        {
            int ValueInt;

            if (value is int AsIntDirect)
                ValueInt = AsIntDirect;
            else if (value is string AsString && int.TryParse(AsString, out int AsInt))
                ValueInt = AsInt;
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Value {value} was expected to be an int");

            return SetIntProperty((int valueInt) => item.RawDuration = valueInt, ValueInt);
        }

        public static void UpdateIconsAndNames()
        {
            Dictionary<string, ParsingContext> EffectParsingTable = ParsingContext.ObjectKeyTable[typeof(PgEffect)];
            foreach (KeyValuePair<string, ParsingContext> Entry in EffectParsingTable)
            {
                PgEffect Effect = (PgEffect)Entry.Value.Item;

                if (Effect.IconId != 0)
                    Effect.FriendlyIconId = Effect.IconId;
                else
                    Effect.FriendlyIconId = PgObject.EffectIconId;

                if (Effect.Name.Length > 0)
                    Effect.FriendlyName = Effect.Name;
                else
                    Effect.FriendlyName = "(no name)";

                Debug.Assert(Effect.ObjectIconId != 0);
                Debug.Assert(Effect.ObjectName.Length > 0);
            }
        }
    }
}
