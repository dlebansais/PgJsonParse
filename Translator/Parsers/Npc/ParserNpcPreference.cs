namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserNpcPreference : Parser
    {
        public override object CreateItem()
        {
            return new PgNpcPreference();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgNpcPreference AsPgNpcPreference))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgNpcPreference, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgNpcPreference item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Keywords":
                        Result = ParseKeywords(item, Value, parsedFile, parsedKey);
                        break;
                    case "Pref":
                        Result = SetFloatProperty((float valueFloat) => item.RawPreference = valueFloat, Value);
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

        private bool ParseKeywords(PgNpcPreference item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ArrayKeyword))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a list");

            foreach (object Item in ArrayKeyword)
            {
                if (!(Item is string ValueKeyword))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Value '{Item}' was expected to be a string");

                if (!ParseKeyword(item, ValueKeyword, parsedFile, parsedKey))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Key '{Item}' was found but for the wrong object type");
            }

            return true;
        }

        private bool ParseKeyword(PgNpcPreference item, string valueKeyword, string parsedFile, string parsedKey)
        {
            if (valueKeyword.StartsWith("MinValue:"))
                return ParseKeywordAsMinValue(item, valueKeyword.Substring(9), parsedFile, parsedKey);
            else if (valueKeyword.StartsWith("SkillPrereq:"))
                return ParseKeywordAsSkillRequirement(item, valueKeyword.Substring(12), parsedFile, parsedKey);
            else if (valueKeyword.StartsWith("EquipmentSlot:"))
                return ParseKeywordAsEquipmentSlot(item, valueKeyword.Substring(14), parsedFile, parsedKey);
            else if (valueKeyword.StartsWith("MinRarity:"))
                return ParseKeywordAsMinRarity(item, valueKeyword.Substring(10), parsedFile, parsedKey);
            else if (valueKeyword.StartsWith("Rarity:"))
                return ParseKeywordAsRarity(item, valueKeyword.Substring(7), parsedFile, parsedKey);
            else if (StringToEnumConversion<ItemKeyword>.TryParse(valueKeyword, out ItemKeyword ParsedItemKeyword))
            {
                item.ItemKeywordList.Add(ParsedItemKeyword);
                return true;
            }
            else
                return false;
        }

        private bool ParseKeywordAsMinValue(PgNpcPreference item, string value, string parsedFile, string parsedKey)
        {
            if (int.TryParse(value, out int AsInt))
                return SetIntProperty((int valueInt) => item.RawMinValueRequirement = valueInt, AsInt);
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be an int");
        }

        private bool ParseKeywordAsSkillRequirement(PgNpcPreference item, string value, string parsedFile, string parsedKey)
        {
            return Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => item.SkillRequirement = valueSkill, value);
        }

        private bool ParseKeywordAsEquipmentSlot(PgNpcPreference item, string value, string parsedFile, string parsedKey)
        {
            return Inserter<ItemSlot>.SetEnum((ItemSlot valueEnum) => item.SlotRequirement = valueEnum, value);
        }

        private bool ParseKeywordAsMinRarity(PgNpcPreference item, string value, string parsedFile, string parsedKey)
        {
            if (value == "Uncommon")
                item.MinRarityRequirement = RecipeItemKey.MinRarity_Uncommon;
            else if (value == "Rare")
                item.MinRarityRequirement = RecipeItemKey.MinRarity_Rare;
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid minimum rarity '{value}'");

            StringToEnumConversion<RecipeItemKey>.SetCustomParsedEnum(item.MinRarityRequirement);
            return true;
        }

        private bool ParseKeywordAsRarity(PgNpcPreference item, string value, string parsedFile, string parsedKey)
        {
            if (value == "Uncommon")
                item.RarityRequirement = RecipeItemKey.Rarity_Uncommon;
            else if (value == "Common")
                item.RarityRequirement = RecipeItemKey.Rarity_Common;
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid rarity '{value}'");

            StringToEnumConversion<RecipeItemKey>.SetCustomParsedEnum(item.RarityRequirement);
            return true;
        }
    }
}
