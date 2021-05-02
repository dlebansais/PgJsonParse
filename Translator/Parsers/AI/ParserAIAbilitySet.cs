namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserAIAbilitySet : Parser
    {
        public override object CreateItem()
        {
            return new PgAIAbilitySet();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgAIAbilitySet AsPgAIAbilitySet)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAIAbilitySet, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAIAbilitySet item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!(Value is ParsingContext Context))
                    return Program.ReportFailure($"Value '{Value}' was expected to be a context");

                if (item.AIAbilityList.Exists((PgAIAbility item) => item.AbilityKey == Key))
                    return Program.ReportFailure($"AI Ability {Key} already added");

                if (!(Context.Item is PgAIAbility AsAIAbility))
                    return Program.ReportFailure($"Object '{Value}' was unexpected");

                AsAIAbility.AbilityKey = Key;
                item.AIAbilityList.Add(AsAIAbility);
            }

            return true;
        }
    }
}
