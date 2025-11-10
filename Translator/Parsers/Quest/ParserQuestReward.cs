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
        { QuestRewardType.Favor, FinishItemFavor },
        { QuestRewardType.NamedLootProfile, FinishItemNamedLootProfile },
        { QuestRewardType.SetInteractionFlag, FinishItemSetInteractionFlag },
        { QuestRewardType.ClearInteractionFlag, FinishItemClearInteractionFlag },
        { QuestRewardType.LoreBook, FinishItemLoreBook },
        { QuestRewardType.Title, FinishItemTitle },
        { QuestRewardType.ScriptedQuestObjective, FinishItemScriptedQuestObjective },
        { QuestRewardType.SkillLevel, FinishItemSkillLevel },
        { QuestRewardType.DispelFaeBombSporeBuff, FinishItemDispelFaeBombSporeBuff },
        { QuestRewardType.Effect, FinishItemEffect },
        { QuestRewardType.Item, FinishItemItem },
        { QuestRewardType.RacingXp, FinishItemRacingXp },
        { QuestRewardType.DeltaScriptAtomicInt, FinishItemDeltaScriptAtomicInt },
        { QuestRewardType.SetAccountFlag, FinishItemSetAccountFlag },
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
        { QuestRewardType.Favor, new List<string>() { "T", "Favor", "Npc" } },
        { QuestRewardType.NamedLootProfile, new List<string>() { "T", "NamedLootProfile" } },
        { QuestRewardType.SetInteractionFlag, new List<string>() { "T", "InteractionFlag" } },
        { QuestRewardType.ClearInteractionFlag, new List<string>() { "T", "InteractionFlag" } },
        { QuestRewardType.LoreBook, new List<string>() { "T", "LoreBook" } },
        { QuestRewardType.Title, new List<string>() { "T", "Title" } },
        { QuestRewardType.ScriptedQuestObjective, new List<string>() { "T", "Npc" } },
        { QuestRewardType.SkillLevel, new List<string>() { "T", "Skill", "Level" } },
        { QuestRewardType.DispelFaeBombSporeBuff, new List<string>() { "T" } },
        { QuestRewardType.Effect, new List<string>() { "T", "Effect" } },
        { QuestRewardType.Item, new List<string>() { "T", "Item", "StackSize" } },
        { QuestRewardType.RacingXp, new List<string>() { "T", "Skill", "Xp" } },
        { QuestRewardType.DeltaScriptAtomicInt, new List<string>() { "T", "InteractionFlag", "Amount" } },
        { QuestRewardType.SetAccountFlag, new List<string>() { "T", "AccountFlag" } },
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
                        Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value);
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
                        Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => NewItem.Recipe_Key = PgObject.GetItemKey(valueRecipe), Value);
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
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => NewItem.Ability_Key = PgObject.GetItemKey(valueAbility), Value);
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

    private static bool FinishItemFavor(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardFavor NewItem = new PgQuestRewardFavor();

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
                    case "Favor":
                        Result = SetIntProperty((int valueInt) => NewItem.RawFavor = valueInt, Value);
                        break;
                    case "Npc":
                        Result = Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => NewItem.FavorNpc = npcLocation, Value, parsedFile, parsedKey);
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

    private static bool FinishItemNamedLootProfile(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardNamedLootProfile NewItem = new PgQuestRewardNamedLootProfile();

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
                    case "NamedLootProfile":
                        Result = StringToEnumConversion<NamedLootProfile>.SetEnum((NamedLootProfile valueEnum) => NewItem.NamedLootProfile = valueEnum, Value);
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

    private static bool FinishItemSetInteractionFlag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        return FinishItemInteractionFlag(isSet: true, ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
    }

    private static bool FinishItemClearInteractionFlag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        return FinishItemInteractionFlag(isSet: false, ref item, contentTable, contentTypeTable, itemCollection, lastItemType, knownFieldList, usedFieldList, parsedFile, parsedKey);
    }

    private static bool FinishItemInteractionFlag(bool isSet, ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardInteractionFlag NewItem = new PgQuestRewardInteractionFlag() { IsSet = isSet };

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
                    case "InteractionFlag":
                        Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.InteractionFlag = valueEnum, Value);
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

    private static bool FinishItemLoreBook(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardLoreBook NewItem = new PgQuestRewardLoreBook();

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
                    case "LoreBook":
                        Result = Inserter<PgLoreBook>.SetItemByInternalName((PgLoreBook valueLoreBook) => NewItem.LoreBook_Key = PgObject.GetItemKey(valueLoreBook), Value);
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

    private static bool FinishItemTitle(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardPlayerTitle NewItem = new PgQuestRewardPlayerTitle();

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
                    case "Title":
                        Result = Inserter<PgPlayerTitle>.SetItemByKey((PgPlayerTitle valuePlayerTitle) => NewItem.PlayerTitle_Key = PgObject.GetItemKey(valuePlayerTitle), Value.ToString()!);
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

    private static bool FinishItemScriptedQuestObjective(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardScriptedQuestObjective NewItem = new PgQuestRewardScriptedQuestObjective();

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
                    case "Npc":
                        Result = Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => NewItem.QuestCompleteNpc = npcLocation, Value, parsedFile, parsedKey);
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

    private static bool FinishItemSkillLevel(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardSkillLevel NewItem = new PgQuestRewardSkillLevel();

        bool Result = true;

        if (contentTable.Count < 3)
            Result = Program.ReportFailure(parsedFile, parsedKey, "Missing fields in Skill Level reward");

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
                        Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value);
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

    private static bool FinishItemDispelFaeBombSporeBuff(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardDispelFaeBombSporeBuff NewItem = new PgQuestRewardDispelFaeBombSporeBuff();

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

    private static bool FinishItemEffect(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardEffect NewItem = new PgQuestRewardEffect();

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
                    case "Effect":
                        Result = Inserter<PgEffect>.SetItemByName((PgEffect valueEffect) => NewItem.Effect_Key = PgObject.GetItemKey(valueEffect), Value, ErrorControl.IgnoreIfNotFound);
                        if (!Result && Value is string ValueString)
                        {
                            Dictionary<string, ParsingContext> KeyTable = ParsingContext.ObjectKeyTable[typeof(PgEffect)];
                            foreach (KeyValuePair<string, ParsingContext> EffectEntry in KeyTable)
                            {
                                PgEffect Effect = (PgEffect)EffectEntry.Value.Item;

                                foreach (EffectKeyword Keyword in Effect.KeywordList)
                                    if (Keyword.ToString() == ValueString)
                                    {
                                        NewItem.Keyword = Keyword;
                                        Result = true;
                                        break;
                                    }

                                if (Result)
                                    break;
                            }

                            if (!Result)
                            {
                                switch (ValueString)
                                {
                                    case "HelpMsg_VidariaRenown":
                                        NewItem.Special = "Help Message (Vidaria Renown)";
                                        Result = true;
                                        break;

                                    case "IncAktaariQuestCounter":
                                        NewItem.Special = "Increment Aktaari Quest Counter";
                                        Result = true;
                                        break;

                                    case "ZhiasBlessing1":
                                        NewItem.Special = "Zhias Blessing #1";
                                        Result = true;
                                        break;

                                    case "ZhiasBlessing2":
                                        NewItem.Special = "Zhias Blessing #2";
                                        Result = true;
                                        break;

                                    case "ZhiasBlessing3":
                                        NewItem.Special = "Zhias Blessing #3";
                                        Result = true;
                                        break;

                                    case "ZhiasBlessing4":
                                        NewItem.Special = "Zhias Blessing #4";
                                        Result = true;
                                        break;

                                    default:
                                        Result = Program.ReportFailure($"Unknown effect: {ValueString}");
                                        break;
                                }
                            }
                        }

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

    private static bool FinishItemItem(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardItem NewItem = new PgQuestRewardItem();

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
                    case "Item":
                        Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item_Key = PgObject.GetItemKey(valueItem), Value);
                        break;
                    case "StackSize":
                        Result = SetIntProperty((int valueInt) => NewItem.RawStackSize = valueInt, Value);
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

    private static bool FinishItemRacingXp(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardRacingXp NewItem = new PgQuestRewardRacingXp();

        bool Result = true;

        if (contentTable.Count < 3)
            Result = Program.ReportFailure(parsedFile, parsedKey, "Missing fields in Racing Xp reward");

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
                        Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => NewItem.Skill_Key = PgObject.GetItemKey(valueSkill), Value);
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

    private static bool FinishItemDeltaScriptAtomicInt(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardDeltaScriptAtomicInt NewItem = new PgQuestRewardDeltaScriptAtomicInt();

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
                    case "Amount":
                        Result = SetIntProperty((int valueInt) => NewItem.RawAmount = valueInt, Value);
                        break;
                    case "InteractionFlag":
                        Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.InteractionFlag = valueEnum, Value);
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

    private static bool FinishItemSetAccountFlag(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
    {
        PgQuestRewardAccountFlag NewItem = new PgQuestRewardAccountFlag();

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
                    case "AccountFlag":
                        Result = StringToEnumConversion<InteractionFlag>.SetEnum((InteractionFlag valueEnum) => NewItem.AccountFlag = valueEnum, Value);
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
