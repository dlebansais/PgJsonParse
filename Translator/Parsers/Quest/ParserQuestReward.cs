namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParserQuestReward : Parser
    {
        public override object CreateItem()
        {
            return null;
        }

        private static Dictionary<QuestRewardType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestRewardType, VariadicObjectHandler>()
        {
            { QuestRewardType.SkillXp, FinishItemSkillXp },
            { QuestRewardType.Recipe, FinishItemRecipe },
            { QuestRewardType.GuildCredits, FinishItemGuildCredits },
            { QuestRewardType.CombatXp, FinishItemCombatXp },
            { QuestRewardType.GuildXp, FinishItemGuildXp },
        };

        private static Dictionary<QuestRewardType, List<string>> KnownFieldTable = new Dictionary<QuestRewardType, List<string>>()
        {
            { QuestRewardType.SkillXp, new List<string>() { "T", "Skill", "Xp" } },
            { QuestRewardType.Recipe, new List<string>() { "T", "Recipe" } },
            { QuestRewardType.GuildCredits, new List<string>() { "T", "Credits" } },
            { QuestRewardType.CombatXp, new List<string>() { "T", "Xp" } },
            { QuestRewardType.GuildXp, new List<string>() { "T", "Xp" } },
        };

        private static Dictionary<QuestRewardType, List<string>> HandledTable = new Dictionary<QuestRewardType, List<string>>();

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
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

            if (!Handler(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
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

        private static bool FinishItemSkillXp(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestRewardSkillXp NewItem = new PgQuestRewardSkillXp();

            bool Result = true;
            PgSkill ParsedSkill = null;
            int ParsedXp = 0;

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
                            Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, Value);
                            break;
                        case "Xp":
                            Result = SetIntProperty((int valueInt) => ParsedXp = valueInt, Value);
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
                NewItem.XpTable.Add(ParsedSkill, ParsedXp);
                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemRecipe(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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
                            Result = Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => NewItem.Recipe = valueRecipe, Value);
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

        private static bool FinishItemGuildCredits(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemCombatXp(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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

        private static bool FinishItemGuildXp(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
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
    }
}
