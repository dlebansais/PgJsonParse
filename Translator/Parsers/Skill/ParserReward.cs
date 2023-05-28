namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserReward : Parser
{
    public override object CreateItem()
    {
        return new PgReward();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgReward AsPgReward)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgReward, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgReward item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Ability":
                    Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.Ability_Key = valueAbility.Key, Value);
                    break;
                case "BonusToSkill":
                    Result = ParserSkill.Parse((PgSkill valueSkill) => item.BonusLevelSkill_Key = valueSkill.Key, Value, parsedFile, parsedKey);
                    break;
                case "Recipe":
                    Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => item.Recipe_Key = valueRecipe.Key, Value);
                    break;
                case "Notes":
                    Result = SetStringProperty((string valueString) => item.Notes = Tools.CleanedUpString(valueString), Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            if (item.Ability_Key == null && item.BonusLevelSkill_Key == null && item.Recipe_Key == null && item.Notes.Length == 0)
                Result = Program.ReportFailure(parsedFile, parsedKey, "Not enough rewards");
        }

        return Result;
    }

    public static int CurrencyToIcon(Currency currency)
    {
        int IconId = 0;
        string ItemName = string.Empty;

        Dictionary<string, ParsingContext> ItemParsingTable = ParsingContext.ObjectKeyTable[typeof(PgItem)];

        switch (currency)
        {
            case Currency.Gold:
                ItemName = "item_14109";
                break;
            case Currency.LiveEventCredits:
                ItemName = "item_14076";
                break;
        }

        if (ItemName.Length > 0 && ItemParsingTable.ContainsKey(ItemName))
        {
            PgItem Item = (PgItem)ItemParsingTable[ItemName].Item;
            if (Item.IconId > 0)
                IconId = Item.IconId;
        }

        return IconId;
    }
}
