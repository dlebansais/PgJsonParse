namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserReward : Parser
    {
        public override object CreateItem()
        {
            return new PgReward();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgReward AsPgReward))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgReward, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgReward item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Ability":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.Ability = valueAbility, Value);
                        break;
                    case "BonusToSkill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => item.BonusSkill = valueSkill, Value, parsedFile, parsedKey);
                        break;
                    case "Recipe":
                        Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => item.Recipe = valueRecipe, Value);
                        break;
                    case "Notes":
                        Result = SetStringProperty((string valueString) => item.Notes = valueString, Value);
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
