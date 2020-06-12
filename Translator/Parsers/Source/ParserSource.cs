namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParserSource : Parser
    {
        public override object CreateItem()
        {
            return null;
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (item != null)
                return Program.ReportFailure("Unexpected failure");

            if (!contentTable.ContainsKey("Type"))
                return Program.ReportFailure(parsedFile, parsedKey, "Source has no type");

            if (!(contentTable["Type"] is string TypeString))
                return Program.ReportFailure("Source type was expected to be a string");

            bool Result = false;

            switch (TypeString)
            {
                case "AutomaticFromSkill":
                    Result = ParseSourceAutomaticFromSkill(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
                    break;
                case "Item":
                    Result = ParseSourceItem(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
                    break;
                case "Training":
                    Result = ParseSourceTraining(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
                    break;
                case "Effect":
                    Result = ParseSourceEffect(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
                    break;
                case "Quest":
                    Result = ParseSourceQuest(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
                    break;
                case "Gift":
                    Result = ParseSourceGift(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
                    break;
                case "HangOut":
                    Result = ParseSourceHangOut(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Unnown source type '{TypeString}'");
                    break;
            }

            Debug.Assert(!Result || item is PgSource);

            return Result;
        }

        private bool ParseSourceAutomaticFromSkill(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;
            PgSourceAutomaticFromSkill NewSource = new PgSourceAutomaticFromSkill();

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Type":
                        break;
                    case "SkillTypeId":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => NewSource.Skill = valueSkill, Value, parsedFile, parsedKey);
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
                item = NewSource;
                return true;
            }
            else
                return false;
        }

        private bool ParseSourceItem(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;
            PgSourceItem NewSource = new PgSourceItem();

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Type":
                        break;
                    case "ItemTypeId":
                        Result = Inserter<PgItem>.SetItemByKey((PgItem valueItem) => NewSource.Item = valueItem, $"item_{Value}");
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
                item = NewSource;
                return true;
            }
            else
                return false;
        }

        private bool ParseSourceTraining(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;
            PgSourceTraining NewSource = new PgSourceTraining();

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Npc":
                        Result = Inserter<PgSource>.SetNpc((PgNpcLocation location) => NewSource.Npc = location, Value, parsedFile, parsedKey);
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
                item = NewSource;
                return true;
            }
            else
                return false;
        }

        private bool ParseSourceEffect(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!contentTable.ContainsKey("EffectName"))
                return Program.ReportFailure(parsedFile, parsedKey, "Source has no effect name");

            if (!(contentTable["EffectName"] is string EffectNameString))
                return Program.ReportFailure("Source effect name was expected to be a string");

            if (EffectNameString == "Learn Ability")
            {
                item = new PgSourceEffect() { RawIsMiscEffect = true };
                return true;
            }

            PgRecipe ParsedRecipe = null;

            if (Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => ParsedRecipe = valueRecipe, EffectNameString, ErrorControl.IgnoreIfNotFound))
            {
                item = new PgSourceRecipe() { Recipe = ParsedRecipe };
                return true;
            }

            if (Inserter<PgRecipe>.SetItemByName((PgRecipe valueRecipe) => ParsedRecipe = valueRecipe, EffectNameString, ErrorControl.IgnoreIfNotFound))
            {
                item = new PgSourceRecipe() { Recipe = ParsedRecipe };
                return true;
            }

            PgEffect ParsedEffect = null;

            if (Inserter<PgEffect>.SetItemByName((PgEffect valueEffect) => ParsedEffect = valueEffect, EffectNameString, ErrorControl.IgnoreIfNotFound))
            {
                item = new PgSourceEffect() { Effect = ParsedEffect };
                return true;
            }

            if (!contentTable.ContainsKey("EffectTypeId"))
                return Program.ReportFailure($"Unknown effect name {EffectNameString}");

            if (!(contentTable["EffectTypeId"] is string ValueString))
                return Program.ReportFailure($"Effect type id was expected to be a string");

            string EffectKey = $"effect_{ValueString}";

            if (Inserter<PgEffect>.SetItemByKey((PgEffect valueEffect) => ParsedEffect = valueEffect, EffectKey, ErrorControl.IgnoreIfNotFound))
            {
                item = new PgSourceEffect() { Effect = ParsedEffect };
                return true;
            }

            return Program.ReportFailure($"Unknown effect name {EffectNameString}");
        }

        private bool ParseSourceQuest(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;
            PgSourceQuest NewSource = new PgSourceQuest();

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Type":
                        break;
                    case "QuestId":
                        Result = Inserter<PgQuest>.SetItemByKey((PgQuest valueQuest) => NewSource.Quest = valueQuest, $"quest_{Value}");
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
                item = NewSource;
                return true;
            }
            else
                return false;
        }

        private bool ParseSourceGift(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;
            PgSourceGift NewSource = new PgSourceGift();

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Npc":
                        Result = Inserter<PgSource>.SetNpc((PgNpcLocation location) => NewSource.Npc = location, Value, parsedFile, parsedKey);
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
                item = NewSource;
                return true;
            }
            else
                return false;
        }

        private bool ParseSourceHangOut(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;
            PgSourceHangOut NewSource = new PgSourceHangOut();

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Type":
                        break;
                    case "Npc":
                        Result = Inserter<PgSource>.SetNpc((PgNpcLocation location) => NewSource.Npc = location, Value, parsedFile, parsedKey);
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
                item = NewSource;
                return true;
            }
            else
                return false;
        }
    }
}
