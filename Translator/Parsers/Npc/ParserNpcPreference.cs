namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserNpcPreference : Parser
    {
        public override object CreateItem()
        {
            return new PgNpcPreference();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgNpcPreference AsPgNpcPreference)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgNpcPreference, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgNpcPreference item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
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
                    case "Favor":
                        Result = ParseFavor(item, Value, parsedFile, parsedKey);
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
                if (item.ItemKeywordList.Count == 0 && item.SkillRequirement == null && item.MinRarityRequirement == RecipeItemKey.Internal_None && item.RarityRequirement == RecipeItemKey.Internal_None && item.RawMinValueRequirement == null && item.SlotRequirement == ItemSlot.Internal_None)
                    return Program.ReportFailure(parsedFile, parsedKey, "Empty preference list");

                if (item.MinRarityRequirement != RecipeItemKey.Internal_None && item.RarityRequirement != RecipeItemKey.Internal_None)
                    return Program.ReportFailure(parsedFile, parsedKey, "Conflicting preference rarity");

                if (item.RawPreference == null || item.RawPreference == 0)
                    return Program.ReportFailure(parsedFile, parsedKey, "No preference value");
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
            return StringToEnumConversion<ItemSlot>.SetEnum((ItemSlot valueEnum) => item.SlotRequirement = valueEnum, value);
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

        private bool ParseFavor(PgNpcPreference item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string FavorString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            Favor ParsedFavor;

            if (FavorString == "Error")
                ParsedFavor = Favor.Internal_None;
            else if (!StringToEnumConversion<Favor>.TryParse(FavorString, out ParsedFavor))
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown favor level '{FavorString}'");

            item.PreferenceFavor = ParsedFavor;
            return true;
        }
    }
}
