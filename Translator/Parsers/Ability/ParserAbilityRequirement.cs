namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParserAbilityRequirement : Parser
    {
        public override object CreateItem()
        {
            return null;
        }

        private static Dictionary<OtherRequirementType, AbilityRequirementHandler> HandlerTable = new Dictionary<OtherRequirementType, AbilityRequirementHandler>()
        {
            { OtherRequirementType.IsLycanthrope, FinishItemIsLycanthrope },
            { OtherRequirementType.HasEffectKeyword, FinishItemHasEffectKeyword },
            { OtherRequirementType.FullMoon, FinishItemIsFullMoon },
            { OtherRequirementType.IsHardcore, FinishItemIsHardcore },
            { OtherRequirementType.DruidEventState, FinishItemDruidEventState },
            { OtherRequirementType.PetCount, FinishItemPetCount },
            { OtherRequirementType.RecipeKnown, FinishItemRecipeKnown },
            { OtherRequirementType.IsNotInCombat, FinishItemIsNotInCombat },
            { OtherRequirementType.IsLongtimeAnimal, FinishItemIsLongtimeAnimal },
            { OtherRequirementType.InHotspot, FinishItemInHotspot },
            { OtherRequirementType.HasInventorySpaceFor, FinishItemHasInventorySpaceFor },
            { OtherRequirementType.IsVegetarian, FinishItemIsVegetarian },
            { OtherRequirementType.InGraveyard, FinishItemIsInGraveyard },
            { OtherRequirementType.EquippedItemKeyword, FinishItemEquippedItemKeyword },
            { OtherRequirementType.InteractionFlagSet, FinishItemInteractionFlagSet },
            { OtherRequirementType.IsVolunteerGuide, FinishItemIsVolunteerGuide },
            { OtherRequirementType.IsNotGuest, FinishItemIsNotGuest },
            { OtherRequirementType.IsNotInHotspot, FinishItemNotInHotspot },
        };

        private static Dictionary<OtherRequirementType, List<string>> KnownFieldTable = new Dictionary<OtherRequirementType, List<string>>()
        {
            { OtherRequirementType.IsLycanthrope, new List<string>() { "T" } },
            { OtherRequirementType.HasEffectKeyword, new List<string>() { "T", "Keyword" } },
            { OtherRequirementType.FullMoon, new List<string>() { "T" } },
            { OtherRequirementType.IsHardcore, new List<string>() { "T" } },
            { OtherRequirementType.DruidEventState, new List<string>() { "T", "DisallowedStates" } },
            { OtherRequirementType.PetCount, new List<string>() { "T", "MaxCount", "PetTypeTag" } },
            { OtherRequirementType.RecipeKnown, new List<string>() { "T", "Recipe" } },
            { OtherRequirementType.IsNotInCombat, new List<string>() { "T" } },
            { OtherRequirementType.IsLongtimeAnimal, new List<string>() { "T" } },
            { OtherRequirementType.InHotspot, new List<string>() { "T", "Name" } },
            { OtherRequirementType.HasInventorySpaceFor, new List<string>() { "T", "Item" } },
            { OtherRequirementType.IsVegetarian, new List<string>() { "T" } },
            { OtherRequirementType.InGraveyard, new List<string>() { "T" } },
            { OtherRequirementType.EquippedItemKeyword, new List<string>() { "T", "MinCount", "MaxCount", "Keyword" } },
            { OtherRequirementType.InteractionFlagSet, new List<string>() { "T", "InteractionFlag" } },
            { OtherRequirementType.IsVolunteerGuide, new List<string>() { "T" } },
            { OtherRequirementType.IsNotGuest, new List<string>() { "T" } },
            { OtherRequirementType.IsNotInHotspot, new List<string>() { "T", "Name" } },
        };

        private static Dictionary<OtherRequirementType, List<string>> HandledTable = new Dictionary<OtherRequirementType, List<string>>();

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (item != null)
                return Program.ReportFailure("Unexpected failure");

            if (!contentTable.ContainsKey("T"))
                return Program.ReportFailure(parsedFile, parsedKey, $"Ability Requirement is missing a T type qualifier");

            object TypeValue = contentTable["T"];

            if (!(TypeValue is string AsTypeString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value {TypeValue} was expected to be a string");

            if (!StringToEnumConversion<OtherRequirementType>.TryParse(AsTypeString, out OtherRequirementType requirementType))
                return false;

            if (!HandlerTable.ContainsKey(requirementType))
                return Program.ReportFailure(parsedFile, parsedKey, $"Requirement {requirementType} has no handler");

            Debug.Assert(KnownFieldTable.ContainsKey(requirementType));

            AbilityRequirementHandler Handler = HandlerTable[requirementType];
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
            foreach (KeyValuePair<OtherRequirementType, AbilityRequirementHandler> Entry in HandlerTable)
            {
                OtherRequirementType Key = Entry.Key;

                if (!HandledTable.ContainsKey(Key))
                    return Program.ReportFailure($"Requirement {Key} was not handled");

                List<string> ReportedFieldList = HandledTable[Key];
                List<string> ExpectedFieldList = KnownFieldTable[Key];

                foreach (string FieldName in ExpectedFieldList)
                    if (!ReportedFieldList.Contains(FieldName))
                        return Program.ReportFailure($"Field {FieldName} for object {Key} was expected but never handled");
            }

            return true;
        }

        private static bool FinishItemIsLycanthrope(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsLycanthrope NewItem = new PgAbilityRequirementIsLycanthrope();

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

        private static bool FinishItemHasEffectKeyword(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementHasEffectKeyword NewItem = new PgAbilityRequirementHasEffectKeyword();

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
                            Result = Inserter<AbilityKeyword>.SetEnum((AbilityKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsFullMoon(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsFullMoon NewItem = new PgAbilityRequirementIsFullMoon();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsHardcore(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsHardcore NewItem = new PgAbilityRequirementIsHardcore();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemDruidEventState(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementDruidEventState NewItem = new PgAbilityRequirementDruidEventState();

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
                        case "DisallowedStates":
                            Result = Inserter<DisallowedState>.SetEnum((DisallowedState valueEnum) => NewItem.DisallowedState = valueEnum, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemPetCount(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementPetCount NewItem = new PgAbilityRequirementPetCount();

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
                        case "MaxCount":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMaxCount = valueInt, Value);
                            break;
                        case "PetTypeTag":
                            Result = Inserter<RecipeKeyword>.SetEnum((RecipeKeyword valueEnum) => NewItem.PetTypeTag = valueEnum, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemRecipeKnown(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementRecipeKnown NewItem = new PgAbilityRequirementRecipeKnown();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsNotInCombat(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsNotInCombat NewItem = new PgAbilityRequirementIsNotInCombat();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsLongtimeAnimal(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsLongtimeAnimal NewItem = new PgAbilityRequirementIsLongtimeAnimal();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemInHotspot(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementInHotspot NewItem = new PgAbilityRequirementInHotspot();

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
                        case "Name":
                            Result = SetStringProperty((string valueString) => NewItem.Name = valueString, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemHasInventorySpaceFor(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementHasInventorySpaceFor NewItem = new PgAbilityRequirementHasInventorySpaceFor();

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
                            Result = Inserter<PgItem>.SetItemByInternalName((PgItem valueItem) => NewItem.Item = valueItem, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsVegetarian(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsVegetarian NewItem = new PgAbilityRequirementIsVegetarian();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsInGraveyard(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsInGraveyard NewItem = new PgAbilityRequirementIsInGraveyard();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemEquippedItemKeyword(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementEquippedItemKeyword NewItem = new PgAbilityRequirementEquippedItemKeyword();

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
                        case "MinCount":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMinCount = valueInt, Value);
                            break;
                        case "MaxCount":
                            Result = SetIntProperty((int valueInt) => NewItem.RawMaxCount = valueInt, Value);
                            break;
                        case "Keyword":
                            Result = Inserter<AbilityKeyword>.SetEnum((AbilityKeyword valueEnum) => NewItem.Keyword = valueEnum, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemInteractionFlagSet(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementInteractionFlagSet NewItem = new PgAbilityRequirementInteractionFlagSet();

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
                            Result = SetStringProperty((string valueString) => NewItem.InteractionFlag = valueString, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsVolunteerGuide(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsVolunteerGuide NewItem = new PgAbilityRequirementIsVolunteerGuide();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemIsNotGuest(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementIsNotGuest NewItem = new PgAbilityRequirementIsNotGuest();

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
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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

        private static bool FinishItemNotInHotspot(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey)
        {
            PgAbilityRequirementNotInHotspot NewItem = new PgAbilityRequirementNotInHotspot();

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
                        case "Name":
                            Result = SetStringProperty((string valueString) => NewItem.Name = valueString, Value);
                            break;
                        default:
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
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
