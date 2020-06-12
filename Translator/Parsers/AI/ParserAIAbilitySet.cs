namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserAIAbilitySet : Parser
    {
        public override object CreateItem()
        {
            return new PgAIAbilitySet();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgAIAbilitySet AsPgAIAbilitySet))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAIAbilitySet, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAIAbilitySet item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!(Value is ParsingContext Context))
                    return Program.ReportFailure($"Value '{Value}' was expected to be a context");

                if (item.AIAbilityList.Exists((PgAIAbility item) => item.Key == Key))
                    return Program.ReportFailure($"AI Ability {Key} already added");

                if (!(Context.Item is PgAIAbility AsAIAbility))
                    return Program.ReportFailure($"Object '{Value}' was unexpected");

                AsAIAbility.Key = Key;
                item.AIAbilityList.Add(AsAIAbility);
            }

            return true;
        }
    }
}
