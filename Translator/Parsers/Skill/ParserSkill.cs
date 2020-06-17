namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System;
    using System.Collections.Generic;
    using System.Xml.Schema;
    using System.Diagnostics;

    public class ParserSkill : Parser
    {
        public static bool Parse(Action<PgSkill> setter, object value, string parsedFile, string parsedKey, ErrorControl errorControl = ErrorControl.Normal)
        {
            if (!(value is string ValueKey))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            if (ValueKey == "Unknown")
            {
                setter(PgSkill.Unknown);
                return true;
            }
            else if (ValueKey == "AnySkill")
            {
                setter(PgSkill.AnySkill);
                return true;
            }
            else
                return Inserter<PgSkill>.SetItemByKey(setter, value, errorControl);
        }

        public override object CreateItem()
        {
            return new PgSkill();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgSkill AsPgSkill))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgSkill, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgSkill item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "Id":
                        Result = SetIntProperty((int valueInt) => item.RawId = valueInt, Value);
                        break;
                    case "Description":
                        Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                        break;
                    case "HideWhenZero":
                        Result = SetBoolProperty((bool valueBool) => item.RawHideWhenZero = valueBool, Value);
                        break;
                    case "XpTable":
                        Result = Inserter<PgXpTable>.SetItemByInternalName((PgXpTable valueXpTable) => item.XpTable = valueXpTable, Value);
                        break;
                    case "AdvancementTable":
                        Result = ParseAdvancementTable(item, Value, parsedFile, parsedKey);
                        break;
                    case "Combat":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsCombatSkill = valueBool, Value);
                        break;
                    case "TSysCompatibleCombatSkills":
                        Result = Inserter<PgSkill>.AddArrayByKey(item.CompatibleCombatSkillList, Value);
                        break;
                    case "MaxBonusLevels":
                        Result = SetIntProperty((int valueInt) => item.RawMaxBonusLevels = valueInt, Value);
                        break;
                    case "InteractionFlagLevelCaps":
                        Result = Inserter<PgLevelCapInteractionList>.SetItemProperty((PgLevelCapInteractionList valueLevelCapInteractionList) => item.InteractionFlagLevelCapList = valueLevelCapInteractionList, Value);
                        break;
                    case "AdvancementHints":
                        Result = Inserter<PgAdvancementHint>.AddKeylessArray(item.AdvancementHintList, Value);
                        break;
                    case "Rewards":
                        Result = Inserter<PgRewardList>.SetItemProperty((PgRewardList valueRewardList) => item.RewardList = valueRewardList, Value);
                        break;
                    case "Reports":
                        Result = Inserter<PgReportList>.SetItemProperty((PgReportList valueReportList) => item.ReportList = valueReportList, Value);
                        break;
                    case "Name":
                        Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                        break;
                    case "Parents":
                        Result = Inserter<PgSkill>.AddArrayByKey(item.ParentSkillList, Value);
                        break;
                    case "SkipBonusLevelsIfSkillUnlearned":
                        Result = SetBoolProperty((bool valueBool) => item.RawSkipBonusLevelsIfSkillUnlearned = valueBool, Value);
                        break;
                    case "AuxCombat":
                        Result = SetBoolProperty((bool valueBool) => item.RawAuxCombat = valueBool, Value);
                        break;
                    case "RecipeIngredientKeywords":
                        Result = StringToEnumConversion<ItemKeyword>.TryParseList(Value, item.RecipeIngredientKeywordList);
                        break;
                    case "_RecipeIngredientKeywords":
                        Result = StringToEnumConversion<ItemKeyword>.TryParseList(Value, item.RecipeIngredientKeywordList);
                        break;
                    case "GuestLevelCap":
                        Result = SetIntProperty((int valueInt) => item.RawGuestLevelCap = valueInt, Value);
                        break;
                    case "IsFakeCombatSkill":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsFakeCombatSkill = valueBool, Value);
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

        private bool ParseAdvancementTable(PgSkill item, object value, string parsedFile, string parsedKey)
        {
            if (value == null)
                return true;

            if (!(value is string TableString))
                return Program.ReportFailure($"Value '{value}' was expected to be a string");

            Dictionary<string, ParsingContext> Table = ParsingContext.ObjectKeyTable[typeof(PgAdvancementTable)];

            foreach (KeyValuePair<string, ParsingContext> Entry in Table)
            {
                ParsingContext Context = Entry.Value;

                if (!(Context.Item is PgAdvancementTable AsAdvancementTable))
                    return Program.ReportFailure($"Object '{Context.Item}' was unexpected");

                if (AsAdvancementTable.InternalName == TableString)
                {
                    item.AdvancementTable = AsAdvancementTable;
                    return true;
                }
            }

            return Program.ReportFailure($"Advancement table '{TableString}' not found");
        }

        public static void FillAssociationTables()
        {
            Dictionary<string, ParsingContext> SkillContextTable = ParsingContext.ObjectKeyTable[typeof(PgSkill)];

            foreach (KeyValuePair<string, ParsingContext> Entry in SkillContextTable)
                FillAssociationTable((PgSkill)Entry.Value.Item);
        }

        private static void FillAssociationTable(PgSkill skill)
        {
            FillAssociationTablePower(skill);
            FillAssociationTableAbility(skill);
        }

        private static void FillAssociationTablePower(PgSkill skill)
        {
            Dictionary<string, ParsingContext> PowerContextTable = ParsingContext.ObjectKeyTable[typeof(PgPower)];

            foreach (KeyValuePair<string, ParsingContext> Entry in PowerContextTable)
            {
                string PowerKey = Entry.Key;
                PgPower Power = (PgPower)Entry.Value.Item;

                if (Power.Skill != skill)
                    continue;
                if (Power.IsUnavailable)
                    continue;
                if (Power.PowerTierList.TierList.Count == 0)
                    continue;

                foreach (ItemSlot Slot in Power.SlotList)
                {
                    if (!skill.AssociationTablePower.ContainsKey(Slot))
                        skill.AssociationTablePower.Add(Slot, new List<string>());

                    List<string> PowerListBySlot = skill.AssociationTablePower[Slot];
                    PowerListBySlot.Add(PowerKey);
                }
            }
        }

        private static void FillAssociationTableAbility(PgSkill skill)
        {
            Dictionary<string, ParsingContext> AbilityContextTable = ParsingContext.ObjectKeyTable[typeof(PgAbility)];

            foreach (KeyValuePair<string, ParsingContext> Entry in AbilityContextTable)
            {
                string AbilityKey = Entry.Key;
                PgAbility Ability = (PgAbility)Entry.Value.Item;

                if (Ability.Skill != skill)
                    continue;
                if (Ability.KeywordList.Contains(AbilityKeyword.Lint_NotLearnable) && Ability.Name != "Sword Slash")
                    continue;

                skill.AssociationListAbility.Add(AbilityKey);
            }
        }

        public static void UpdateIconsAndNames()
        {
            Dictionary<string, ParsingContext> SkillParsingTable = ParsingContext.ObjectKeyTable[typeof(PgSkill)];
            foreach (KeyValuePair<string, ParsingContext> Entry in SkillParsingTable)
            {
                PgSkill Skill = (PgSkill)Entry.Value.Item;

                Skill.IconId = SkillToIcon(Skill);

                Debug.Assert(Skill.ObjectIconId != 0);
                Debug.Assert(Skill.ObjectName.Length > 0);
            }
        }

        public static int SkillToIcon(PgSkill skill)
        {
            int Result = 0;

            Dictionary<string, ParsingContext> AbilityParsingTable = ParsingContext.ObjectKeyTable[typeof(PgAbility)];
            foreach (KeyValuePair<string, ParsingContext> Entry in AbilityParsingTable)
            {
                PgAbility Ability = (PgAbility)Entry.Value.Item;
                if (Ability.Skill == skill && Ability.IconId != 0)
                {
                    if (Result == 0 || Ability.KeywordList.Contains(AbilityKeyword.BasicAttack))
                    {
                        Result = Ability.IconId;
                        break;
                    }
                }
            }

            return Result;
        }
    }
}
