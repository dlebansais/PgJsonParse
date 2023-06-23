namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserSkill : Parser
{
    public static bool Parse(Action<PgSkill> setter, object value, string parsedFile, string parsedKey, ErrorControl errorControl = ErrorControl.Normal)
    {
        if (!(value is string ValueKey))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

        if (ValueKey == "ArmorSmithing")
            ValueKey = "Armorsmithing";

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
            return Inserter<PgSkill>.SetItemByKey(setter, ValueKey, errorControl);
    }

    public override object CreateItem()
    {
        return new PgSkill();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgSkill AsPgSkill)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgSkill, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgSkill item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
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
                    Result = SetStringProperty((string valueString) => item.Description = Tools.CleanedUpString(valueString), Value);
                    break;
                case "HideWhenZero":
                    Result = SetBoolProperty((bool valueBool) => item.SetHideWhenZero(valueBool), Value);
                    break;
                case "XpTable":
                    Result = Inserter<PgXpTable>.SetItemByInternalName((PgXpTable valueXpTable) => item.XpTable = valueXpTable, Value);
                    break;
                case "AdvancementTable":
                    Result = ParseAdvancementTable(item, Value, parsedFile, parsedKey);
                    break;
                case "Combat":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsCombatSkill(valueBool), Value);
                    break;
                case "TSysCompatibleCombatSkills":
                    Result = ParseCompatibleCombatSkills(item, Value, parsedFile, parsedKey);
                    break;
                case "MaxBonusLevels":
                    Result = SetIntProperty((int valueInt) => item.RawMaxBonusLevels = valueInt, Value);
                    break;
                case "InteractionFlagLevelCaps":
                    Result = ParseInteractionFlagLevelCaps(item, Value, parsedFile, parsedKey);
                    break;
                case "AdvancementHints":
                    Result = ParseAdvancementHints(item, Value, parsedFile, parsedKey);
                    break;
                case "Rewards":
                    Result = ParseRewards(item, Value, parsedFile, parsedKey);
                    break;
                case "Reports":
                    Result = ParseReports(item, Value, parsedFile, parsedKey);
                    break;
                case "Name":
                    Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                    break;
                case "Parents":
                    Result = Inserter<PgSkill>.AddPgObjectArrayByKey<PgSkill>(item.ParentSkillList, Value);
                    break;
                case "SkipBonusLevelsIfSkillUnlearned":
                    Result = SetBoolProperty((bool valueBool) => item.SetSkipBonusLevelsIfSkillUnlearned(valueBool), Value);
                    break;
                case "AuxCombat":
                    Result = SetBoolProperty((bool valueBool) => item.SetAuxCombat(valueBool), Value);
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
                    Result = SetBoolProperty((bool valueBool) => item.SetIsFakeCombatSkill(valueBool), Value);
                    break;
                case "IsUmbrellaSkill":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsUmbrellaSkill(valueBool), Value);
                    break;
                case "SkillLevelDisparityApplies":
                    Result = SetBoolProperty((bool valueBool) => item.SetSkillLevelDisparityApplies(valueBool), Value);
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
            item.SkillAdvancementList.Sort(SortSkillAdvancementByLevel);
        }

        return Result;
    }

    private static int SortSkillAdvancementByLevel(PgSkillAdvancement item1, PgSkillAdvancement item2)
    {
        if (item1.Level > item2.Level)
            return 1;
        else if (item1.Level < item2.Level)
            return -1;
        else
            return 0;
    }

    private bool ParseAdvancementTable(PgSkill item, object value, string parsedFile, string parsedKey)
    {
        if (value == null)
            return true;

        if (!(value is string TableString))
            return Program.ReportFailure($"Value '{value}' was expected to be a string");

        if (TableString == "null")
            return true;

        Dictionary<string, ParsingContext> Table = ParsingContext.ObjectKeyTable[typeof(PgAdvancementTable)];

        foreach (KeyValuePair<string, ParsingContext> Entry in Table)
        {
            ParsingContext Context = Entry.Value;

            if (!(Context.Item is PgAdvancementTable AsAdvancementTable))
                return Program.ReportFailure($"Object '{Context.Item}' was unexpected");

            if (AsAdvancementTable.InternalName == TableString)
            {
                foreach (KeyValuePair<int, PgAdvancement> AdvancementEntry in AsAdvancementTable.LevelTable)
                {
                    int Level = AdvancementEntry.Key;
                    PgAdvancement Advancement = AdvancementEntry.Value;

                    foreach (PgAdvancementEffectAttribute EffectAttribute in Advancement.EffectAttributeList)
                    {
                        PgSkillAdvancement NewSkillAdvancement = new PgSkillAdvancementRewardAdvancement() { RawLevel = Level, EffectAttribute = EffectAttribute };

                        ParsingContext.AddSuplementaryObject(NewSkillAdvancement);
                        item.SkillAdvancementList.Add(NewSkillAdvancement);
                    }
                }

                return true;
            }
        }

        return Program.ReportFailure($"Advancement table '{TableString}' not found");
    }

    private bool ParseCompatibleCombatSkills(PgSkill item, object value, string parsedFile, string parsedKey)
    {
        if (!Inserter<PgSkill>.AddPgObjectArrayByKey<PgSkill>(item.CompatibleCombatSkillList, value))
            return false;

        item.CompatibleCombatSkillList.Sort();
        return true;
    }

    private bool ParseInteractionFlagLevelCaps(PgSkill item, object value, string parsedFile, string parsedKey)
    {
        foreach (object Context in (System.Collections.IEnumerable)value)
        {
            PgLevelCapInteraction Item = null!;
            if (!Inserter<PgLevelCapInteraction>.SetItemProperty((PgLevelCapInteraction valueLevelCapInteraction) => Item = valueLevelCapInteraction, Context))
                return false;

            int Level = Item.Level;
            string? SkillKey = Item.Skill_Key;

            PgSkillAdvancement NewSkillAdvancement = new PgSkillAdvancementLevelCapInteraction() { RawLevel = Level, Skill_Key = SkillKey };

            ParsingContext.AddSuplementaryObject(NewSkillAdvancement);
            item.SkillAdvancementList.Add(NewSkillAdvancement);
        }

        return true;
    }

    private bool ParseAdvancementHints(PgSkill item, object value, string parsedFile, string parsedKey)
    {
        PgAdvancementHintCollection AdvancementHintList = new PgAdvancementHintCollection();
        if (!Inserter<PgAdvancementHint>.AddKeylessArray(AdvancementHintList, value))
            return false;

        foreach (PgAdvancementHint AdvancementHint in AdvancementHintList)
        {
            foreach (KeyValuePair<int, string> Entry in AdvancementHint.HintTable)
            {
                int Level = Entry.Key;
                string Hint = Entry.Value;
                if (!ParseHints(Hint, parsedFile, parsedKey, out PgNpcLocationCollection NpcList))
                    return false;

                PgSkillAdvancement NewSkillAdvancement = new PgSkillAdvancementHint() { RawLevel = Level, Hint = Hint, NpcList = NpcList };

                ParsingContext.AddSuplementaryObject(NewSkillAdvancement);
                item.SkillAdvancementList.Add(NewSkillAdvancement);
            }
        }

        return true;
    }

    private bool ParseHints(string hint, string parsedFile, string parsedKey, out PgNpcLocationCollection npcList)
    {
        npcList = new PgNpcLocationCollection();

        if (hint.EndsWith("learn a new dance move."))
        {
            return true;
        }

        hint = hint.Replace(" during a Full Moon,", string.Empty);

        string Pattern;
        int StartIndex;

        Pattern = "gain favor with ";
        StartIndex = hint.IndexOf(Pattern);

        if (StartIndex < 0)
        {
            Pattern = "speak with ";
            StartIndex = hint.IndexOf(Pattern);
        }

        if (StartIndex < 0)
        {
            Pattern = "seek out ";
            StartIndex = hint.IndexOf(Pattern);
        }

        if (StartIndex < 0)
        {
            if (hint.Contains(" equip "))
                return true;

            return Program.ReportFailure($"Advancement trigger not found in: {hint}");
        }

        StartIndex += Pattern.Length;

        int EndIndex;

        EndIndex = hint.IndexOf(" in ", StartIndex);
        if (EndIndex < 0)
            EndIndex = hint.IndexOf(" outside of ", StartIndex);
        if (EndIndex <= StartIndex)
            return Program.ReportFailure($"Bad advancement hint: {hint}");

        string NpcNameString = hint.Substring(StartIndex, EndIndex - StartIndex);
        string[] NpcNames = NpcNameString.Split(new string[] { " or " }, StringSplitOptions.None);

        foreach (string NpcName in NpcNames)
        {
            PgNpcLocation ParsedNpc = null!;
            if (!Inserter<PgSkill>.SetNpc((PgNpcLocation npcLocation) => ParsedNpc = npcLocation, $"NPC_{NpcName}", parsedFile, parsedKey))
                return false;

            npcList.Add(ParsedNpc);
        }

        if (npcList.Count == 0)
            return Program.ReportFailure($"No NPC name in advancement hint: {hint}");

        return true;
    }

    private bool ParseRewards(PgSkill item, object value, string parsedFile, string parsedKey)
    {
        foreach (object Context in (System.Collections.IEnumerable)value)
        {
            PgReward Reward = null!;
            if (!Inserter<PgReward>.SetItemProperty((PgReward valueReward) => Reward = valueReward, Context))
                return false;

            int Level = Reward.RewardLevel;
            List<Race> RaceRestrictionList = Reward.RaceRestrictionList;
            string[]? AbilityKeys = Reward.Ability_Keys;
            string? BonusLevelSkillKey = Reward.BonusLevelSkill_Key;
            string? RecipeKey = Reward.Recipe_Key;
            string Notes = Reward.Notes;

            if (AbilityKeys != null)
            {
                PgSkillAdvancement NewSkillAdvancement = new PgSkillAdvancementRewardAbility() { RawLevel = Level, RaceRestrictionList = RaceRestrictionList, Ability_Keys = AbilityKeys };

                ParsingContext.AddSuplementaryObject(NewSkillAdvancement);
                item.SkillAdvancementList.Add(NewSkillAdvancement);
            }

            if (BonusLevelSkillKey != null)
            {
                PgSkillAdvancement NewSkillAdvancement = new PgSkillAdvancementRewardBonusLevel() { RawLevel = Level, RaceRestrictionList = RaceRestrictionList, BonusLevelSkill_Key = BonusLevelSkillKey };

                ParsingContext.AddSuplementaryObject(NewSkillAdvancement);
                item.SkillAdvancementList.Add(NewSkillAdvancement);
            }

            if (RecipeKey != null)
            {
                PgSkillAdvancement NewSkillAdvancement = new PgSkillAdvancementRewardRecipe() { RawLevel = Level, RaceRestrictionList = RaceRestrictionList, Recipe_Key = RecipeKey };

                ParsingContext.AddSuplementaryObject(NewSkillAdvancement);
                item.SkillAdvancementList.Add(NewSkillAdvancement);
            }

            if (Notes.Length > 0)
            {
                PgSkillAdvancement NewSkillAdvancement = new PgSkillAdvancementNotes() { RawLevel = Level, RaceRestrictionList = RaceRestrictionList, Text = Notes };

                ParsingContext.AddSuplementaryObject(NewSkillAdvancement);
                item.SkillAdvancementList.Add(NewSkillAdvancement);
            }
        }

        return true;
    }

    private bool ParseReports(PgSkill item, object value, string parsedFile, string parsedKey)
    {
        foreach (object Context in (System.Collections.IEnumerable)value)
        {
            PgReport Report = null!;
            if (!Inserter<PgReport>.SetItemProperty((PgReport valueReport) => Report = valueReport, Context))
                return false;

            item.ReportList.Add(Report);
        }

        return true;
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

            if (Power.Skill_Key != skill.Key)
                continue;
            if (Power.IsUnavailable)
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
            string Skill_Key = Ability.Skill_Key ?? throw new NullReferenceException();
            PgSkill AbilitySkill = (PgSkill)(Skill_Key.Length == 0 ? PgSkill.Unknown : (Skill_Key == "AnySkill" ? PgSkill.AnySkill : ParsingContext.ObjectKeyTable[typeof(PgSkill)][Skill_Key].Item));

            if (AbilitySkill != skill)
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
        int IconId = 0;

        UpdateIconIdFromAbilities(skill, ref IconId);
        UpdateIconIdFromRecipes(skill, ref IconId);
        UpdateIconIdFromItems(skill, ref IconId);

        if (IconId == 0)
        {
            if (skill.Key == "AlcoholTolerance")
                IconId = 3677;
            else if (skill.Key.StartsWith("Anatomy"))
                IconId = 4004;
            else
            {
                foreach (PgSkillAdvancement Item in skill.SkillAdvancementList)
                    if (Item is PgSkillAdvancementRewardAdvancement)
                        IconId = PgObject.SkillIconId;

                IconId = PgObject.AbilityIconId;
            }
        }

        return IconId;
    }

    public static void UpdateIconIdFromAbilities(PgSkill skill, ref int iconId)
    {
        int LowestLevel = int.MaxValue;
        int LowestLevelIconId = 0;

        Dictionary<string, ParsingContext> AbilityParsingTable = ParsingContext.ObjectKeyTable[typeof(PgAbility)];
        foreach (KeyValuePair<string, ParsingContext> Entry in AbilityParsingTable)
        {
            if (iconId != 0)
                break;

            PgAbility Ability = (PgAbility)Entry.Value.Item;
            string Skill_Key = Ability.Skill_Key ?? throw new NullReferenceException();
            PgSkill AbilitySkill = (PgSkill)(Skill_Key.Length == 0 ? PgSkill.Unknown : (Skill_Key == "AnySkill" ? PgSkill.AnySkill : ParsingContext.ObjectKeyTable[typeof(PgSkill)][Skill_Key].Item));

            if (AbilitySkill == skill && Ability.IconId != 0)
            {
                if (Ability.KeywordList.Contains(AbilityKeyword.BasicAttack))
                    iconId = Ability.IconId;
                else if (LowestLevel > Ability.Level)
                {
                    LowestLevel = Ability.Level;
                    LowestLevelIconId = Ability.IconId;
                }
            }
        }

        if (iconId == 0 && LowestLevelIconId != 0)
            iconId = LowestLevelIconId;
    }

    public static void UpdateIconIdFromRecipes(PgSkill skill, ref int iconId)
    {
        Dictionary<string, ParsingContext> RecipeParsingTable = ParsingContext.ObjectKeyTable[typeof(PgRecipe)];
        foreach (KeyValuePair<string, ParsingContext> Entry in RecipeParsingTable)
        {
            if (iconId != 0)
                break;

            PgRecipe Recipe = (PgRecipe)Entry.Value.Item;
            if (Recipe.Skill_Key == skill.Key && Recipe.IconId != 0)
                iconId = Recipe.IconId;
        }
    }

    public static void UpdateIconIdFromItems(PgSkill skill, ref int iconId)
    {
        int LowestLevel = int.MaxValue;
        int LowestLevelIconId = 0;

        Dictionary<string, ParsingContext> ItemParsingTable = ParsingContext.ObjectKeyTable[typeof(PgItem)];
        foreach (KeyValuePair<string, ParsingContext> Entry in ItemParsingTable)
        {
            if (iconId != 0)
                break;

            PgItem Item = (PgItem)Entry.Value.Item;
            if (Item.IconId != 0)
            {
                foreach (KeyValuePair<string, int> SkillEntry in Item.SkillRequirementTable)
                    if ((SkillEntry.Key.Length == 0 && skill == PgSkill.Unknown) || (SkillEntry.Key == "AnySkill" && skill == PgSkill.AnySkill) || (SkillEntry.Key == skill.Key))
                    {
                        if (LowestLevel > SkillEntry.Value)
                        {
                            LowestLevel = SkillEntry.Value;
                            LowestLevelIconId = Item.IconId;
                        }
                    }
            }
        }

        if (iconId == 0 && LowestLevelIconId != 0)
            iconId = LowestLevelIconId;
    }
}
