namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParserQuestObjectiveRequirement : Parser
    {
        public override object CreateItem()
        {
            return null;
        }

        private static Dictionary<QuestObjectiveRequirementType, VariadicObjectHandler> HandlerTable = new Dictionary<QuestObjectiveRequirementType, VariadicObjectHandler>()
        {
            { QuestObjectiveRequirementType.TimeOfDay, FinishItemTimeOfDay },
            { QuestObjectiveRequirementType.HasEffectKeyword, FinishItemHasEffectKeyword },
            { QuestObjectiveRequirementType.Appearance, FinishItemAppearance },
        };

        private static Dictionary<QuestObjectiveRequirementType, List<string>> KnownFieldTable = new Dictionary<QuestObjectiveRequirementType, List<string>>()
        {
            { QuestObjectiveRequirementType.TimeOfDay, new List<string>() { "T", "MinHour", "MaxHour" } },
            { QuestObjectiveRequirementType.HasEffectKeyword, new List<string>() { "T", "Keyword" } },
            { QuestObjectiveRequirementType.Appearance, new List<string>() { "T", "Appearance" } },
        };

        private static Dictionary<QuestObjectiveRequirementType, List<string>> HandledTable = new Dictionary<QuestObjectiveRequirementType, List<string>>();

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (item != null)
                return Program.ReportFailure("Unexpected failure");

            if (!contentTable.ContainsKey("T"))
                return Program.ReportFailure(parsedFile, parsedKey, $"Quest objective requirement is missing a Type qualifier");

            object TypeValue = contentTable["T"];

            if (!(TypeValue is string AsTypeString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

            if (!StringToEnumConversion<QuestObjectiveRequirementType>.TryParse(AsTypeString, out QuestObjectiveRequirementType requirementType))
                return false;

            if (!HandlerTable.ContainsKey(requirementType))
                return Program.ReportFailure(parsedFile, parsedKey, $"Objective requirement {requirementType} has no handler");

            Debug.Assert(KnownFieldTable.ContainsKey(requirementType));

            VariadicObjectHandler Handler = HandlerTable[requirementType];
            List<string> KnownFieldList = KnownFieldTable[requirementType];
            List<string> UsedFieldList = new List<string>();

            if (!Handler(ref item, contentTable, ContentTypeTable, itemCollection, LastItemType, KnownFieldList, UsedFieldList, parsedFile, parsedKey))
                return false;

            if (!HandledTable.ContainsKey(requirementType))
                HandledTable.Add(requirementType, new List<string>());

            List<string> ReportedFieldList = HandledTable[requirementType];
            foreach (string FieldName in UsedFieldList)
                if (!ReportedFieldList.Contains(FieldName))
                    ReportedFieldList.Add(FieldName);

            return true;
        }

        public static bool FinalizeParsing()
        {
            return Finalizer<QuestObjectiveRequirementType>.FinalizeParsing(HandlerTable, HandledTable, KnownFieldTable);
        }

        private static bool FinishItemTimeOfDay(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveRequirementTimeOfDay NewItem = new PgQuestObjectiveRequirementTimeOfDay();

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
                        case "MinHour":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMinHour = valueInt, Value);
                            break;
                        case "MaxHour":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMaxHour = valueInt, Value);
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
                if (!NewItem.RawMinHour.HasValue || !NewItem.RawMaxHour.HasValue)
                    Result = Program.ReportFailure(parsedFile, parsedKey, "Missing Min/Max hour");

                item = NewItem;
                return true;
            }
            else
                return false;
        }

        private static bool FinishItemHasEffectKeyword(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveRequirementHasEffectKeyword NewItem = new PgQuestObjectiveRequirementHasEffectKeyword();

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
                        case "Keyword":
                            Result = StringToEnumConversion<EffectKeyword>.SetEnum((EffectKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
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

        private static bool FinishItemAppearance(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgQuestObjectiveRequirementAppearance NewItem = new PgQuestObjectiveRequirementAppearance();

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
                        case "Appearance":
                            Result = StringToEnumConversion<Appearance>.SetEnum((Appearance valueEnum) => NewItem.Appearance = valueEnum, Value);
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
