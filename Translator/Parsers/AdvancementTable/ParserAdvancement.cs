namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserAdvancement : Parser
    {
        public override object CreateItem()
        {
            return new PgAdvancement();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgAdvancement AsPgAdvancement)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAdvancement, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAdvancement item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                PgAttribute ParsedAttribute = null!;
                float ParsedValue = 0;

                if (Key == "MENTAL_DEFENSE_RATING")
                    continue;

                Result = Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, Key);

                if (Result)
                {
                    if (Value is float FloatValue)
                        ParsedValue = FloatValue;
                    else if (Value is int IntValue)
                        ParsedValue = IntValue;
                    else
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Unknown attribute value '{Value}'");
                }

                if (Result)
                {
                    PgAdvancementEffectAttribute NewAdvancementEffectAttribute = new PgAdvancementEffectAttribute() { Attribute_Key = ParsedAttribute.Key, RawValue = ParsedValue };

                    ParsingContext.AddSuplementaryObject(NewAdvancementEffectAttribute);
                    item.EffectAttributeList.Add(NewAdvancementEffectAttribute);
                }
            }

            return Result;
        }
    }
}
