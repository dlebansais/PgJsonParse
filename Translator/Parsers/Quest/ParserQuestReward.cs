namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserQuestReward : Parser
{
    public override object CreateItem()
    {
        return null!;
    }

    private static Dictionary<QuestRewardType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestRewardType, VariadicObjectHandler>()
    {
        { QuestRewardType.SkillXp, FinishItemSkillXp },
        { QuestRewardType.Recipe, FinishItemRecipe },
        { QuestRewardType.GuildCredits, FinishItemGuildCredits },
        { QuestRewardType.CombatXp, FinishItemCombatXp },
        { QuestRewardType.GuildXp, FinishItemGuildXp },
        { QuestRewardType.Currency, FinishItemCurrency },
        { QuestRewardType.Ability, FinishItemAbility },
        { QuestRewardType.WorkOrderCurrency, FinishItemWorkOrderCurrency },
    };

    private static Dictionary<QuestRewardType, List<string>> KnownFieldTable = new Dictionary<QuestRewardType, List<string>>()
    {
        { QuestRewardType.SkillXp, new List<string>() { "T", "Skill", "Xp" } },
        { QuestRewardType.Recipe, new List<string>() { "T", "Recipe" } },
        { QuestRewardType.GuildCredits, new List<string>() { "T", "Credits" } },
        { QuestRewardType.CombatXp, new List<string>() { "T", "Xp", "Level" } },
        { QuestRewardType.GuildXp, new List<string>() { "T", "Xp" } },
        { QuestRewardType.Currency, new List<string>() { "T", "Amount", "Currency" } },
        { QuestRewardType.Ability, new List<string>() { "T", "Ability" } },
        { QuestRewardType.WorkOrderCurrency, new List<string>() { "T", "Amount", "Currency" } },
    };

    private static Dictionary<QuestRewardType, List<string>> HandledTable = new Dictionary<QuestRewardType, List<string>>();

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item != null)
            return Program.ReportFailure("Unexpected failure");

        if (!contentTable.ContainsKey("T"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Quest reward is missing a Type qualifier");

        object TypeValue = contentTable["T"];

        if (!(TypeValue is string AsTypeString))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

        if (!StringToEnumConversion<QuestRewardType>.TryParse(AsTypeString, out QuestRewardType rewardType))
            return false;

        if (!HandlerTable.ContainsKey(rewardType))
            return Program.ReportFailure(parsedFile, parsedKey, $"Reward {rewardType} has no handler");

        Debug.Assert(KnownFieldTable.ContainsKey(rewardType));

        VariadicObjectHandler Handler = HandlerTable[rewardType];
        List<string> KnownFieldList = KnownFieldTable[rewardType];
        List<string> UsedFieldList = new List<string>();

        if (!Handler(ref item, contentTable, contentTypeTable, itemCollection, lastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
            return false;

        if (!HandledTable.ContainsKey(rewardType))
            HandledTable.Add(rewardType, new List<string>());

        List<string> ReportedFieldList = HandledTable[rewardType];
        foreach (string FieldName in UsedFieldList)
            if (!ReportedFieldList.Contains(FieldName))
                ReportedFieldList.Add(FieldName);

        return true;
    }

    public static bool FinalizeParsing()
    {
        return Finalizer<QuestRewardType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
    }

    private static bool FinishItemSkillXp(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardSkillXp NewItem = new PgQuestRewardSkillXp();

        bool Result = true;

        if (contentTable.Count < 3)
            Result = Program.ReportFailure(parsedFile, parsedKey, "Missing fields in Skill Xp reward");

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Skill":
                        Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = valueSkill.Key, Value);
                        break;
                    case "Xp":
                        Result = SetIntProperty((int valueInt) => NewItem.RawXp = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemRecipe(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardRecipe NewItem = new PgQuestRewardRecipe();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Recipe":
                        Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => NewItem.Recipe_Key = valueRecipe.Key, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemGuildCredits(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardGuildCredits NewItem = new PgQuestRewardGuildCredits();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Credits":
                        Result = SetIntProperty((int valueInt) => NewItem.RawCredits = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemCombatXp(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardCombatXp NewItem = new PgQuestRewardCombatXp();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Xp":
                        Result = SetIntProperty((int valueInt) => NewItem.RawXp = valueInt, Value);
                        break;
                    case "Level":
                        Result = SetIntProperty((int valueInt) => NewItem.RawLevel = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemGuildXp(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardGuildXp NewItem = new PgQuestRewardGuildXp();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Xp":
                        Result = SetIntProperty((int valueInt) => NewItem.RawXp = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemCurrency(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardCurrency NewItem = new PgQuestRewardCurrency();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Currency":
                        Result = StringToEnumConversion<Currency>.SetEnum((Currency valueEnum) => NewItem.Currency = valueEnum, Value);
                        break;
                    case "Amount":
                        Result = SetIntProperty((int valueInt) => NewItem.RawAmount = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemAbility(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardAbility NewItem = new PgQuestRewardAbility();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Ability":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => NewItem.Ability_Key = valueAbility.Key, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }

    private static bool FinishItemWorkOrderCurrency(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardWorkOrderCurrency NewItem = new PgQuestRewardWorkOrderCurrency();

        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            if (!knownFieldList.Contains(Key))
                Result = Program.ReportFailure($"Unknown field {Key}");
            else
            {
                usedFieldList.Add(Key);

                switch (Key)
                {
                    case "T":
                        break;
                    case "Currency":
                        Result = StringToEnumConversion<Currency>.SetEnum((Currency valueEnum) => NewItem.Currency = valueEnum, Value);
                        break;
                    case "Amount":
                        Result = SetIntProperty((int valueInt) => NewItem.RawAmount = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Unexpected failure");
                        break;
                }
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            item = NewItem;
            return true;
        }
        else
            return false;
    }
}
