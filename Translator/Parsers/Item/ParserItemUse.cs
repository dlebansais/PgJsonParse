namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserItemUse : Parser
    {
        public override object CreateItem()
        {
            return new PgItemUse();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgItemUse AsPgItemUse))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgItemUse, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgItemUse item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "RecipesThatUseItem":
                        Result = ParseRecipesThatUseItem(item, Value, parsedFile, parsedKey);
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

        private bool ParseRecipesThatUseItem(PgItemUse item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ObjectList))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a list");

            foreach (object Item in ObjectList)
            {
                if (!(Item is int ObjectId))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Value '{Item}' was expected to be an int");

                string Key = $"recipe_{ObjectId}";
                PgRecipe ParsedRecipe = null;
                if (!Inserter<PgRecipe>.SetItemByKey((PgRecipe valueRecipe) => ParsedRecipe = valueRecipe, Key))
                    return false;

                if (item.RecipeList.Contains(ParsedRecipe))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Recipe '{Key}' already listed");

                item.RecipeList.Add(ParsedRecipe);
            }

            return true;
        }
    }
}
