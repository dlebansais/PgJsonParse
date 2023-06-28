namespace Translator;

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
                case "ItemKeywords":
                    Result = StringToEnumConversion<ItemKeyword>.TryParseList(Value, item.ItemKeywordList);
                    break;
                case "PreferenceMultiplier":
                    Result = SetFloatProperty((float valueFloat) => item.RawPreference = valueFloat, Value);
                    break;
                case "MinValueRequirement":
                    Result = SetIntProperty((int valueInt) => item.RawMinValueRequirement = valueInt, Value);
                    break;
                case "Favor":
                    Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => item.PreferenceFavor = valueEnum, Value);
                    break;
                case "Desire":
                    Result = StringToEnumConversion<Desire>.SetEnum((Desire valueEnum) => item.PreferenceDesire = valueEnum, Value);
                    break;
                case "SkillRequirement":
                    Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => item.SkillRequirement_Key = PgObject.GetItemKey(valueSkill), Value);
                    break;
                case "SlotRequirement":
                    Result = StringToEnumConversion<ItemSlot>.SetEnum((ItemSlot valueEnum) => item.SlotRequirement = valueEnum, Value);
                    break;
                case "MinRarityRequirement":
                    Result = StringToEnumConversion<RecipeItemKey>.SetEnum((RecipeItemKey valueEnum) => item.MinRarityRequirement = valueEnum, $"MinRarity_{Value}");
                    break;
                case "RarityRequirement":
                    Result = StringToEnumConversion<RecipeItemKey>.SetEnum((RecipeItemKey valueEnum) => item.RarityRequirement = valueEnum, $"Rarity_{Value}");
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
            if (item.ItemKeywordList.Count == 0 && item.SkillRequirement_Key == null && item.MinRarityRequirement == RecipeItemKey.Internal_None && item.RarityRequirement == RecipeItemKey.Internal_None && item.RawMinValueRequirement == null && item.SlotRequirement == ItemSlot.Internal_None)
                return Program.ReportFailure(parsedFile, parsedKey, "Empty preference list");

            if (item.MinRarityRequirement != RecipeItemKey.Internal_None && item.RarityRequirement != RecipeItemKey.Internal_None)
                return Program.ReportFailure(parsedFile, parsedKey, "Conflicting preference rarity");

            if (item.RawPreference == null || item.RawPreference == 0)
                return Program.ReportFailure(parsedFile, parsedKey, "No preference value");

            if (item.PreferenceDesire != Desire.Internal_None)
            {
                if (!DesireToPreferenceTable.ContainsKey(item.PreferenceDesire))
                    return Program.ReportFailure(parsedFile, parsedKey, "Unknown desire value");
            }
        }

        return Result;
    }

    private static Dictionary<Desire, float> DesireToPreferenceTable = new()
    {
        { Desire.Love, 2.0F },
        { Desire.Like, 1.0F },
        { Desire.Dislike, -1.0F },
        { Desire.Hate, -3.0F },
    };
}
