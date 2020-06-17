namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ParserQuest : Parser
    {
        public override object CreateItem()
        {
            return new PgQuest();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgQuest AsPgQuest))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgQuest, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgQuest item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "InternalName":
                        Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                        break;
                    case "Name":
                        Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                        break;
                    case "Description":
                        Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                        break;
                    case "Version":
                        Result = SetIntProperty((int valueInt) => item.RawVersion = valueInt, Value);
                        break;
                    case "RequirementsToSustain":
                        Result = Inserter<PgQuestRequirement>.AddKeylessArray(item.QuestRequirementToSustainList, Value);
                        break;
                    case "ReuseTime_Minutes":
                        Result = SetTimeProperty(() => item.RawReuseTime, (TimeSpan valueTime) => item.RawReuseTime = valueTime, 1, Value);
                        break;
                    case "ReuseTime_Hours":
                        Result = SetTimeProperty(() => item.RawReuseTime, (TimeSpan valueTime) => item.RawReuseTime = valueTime, 60, Value);
                        break;
                    case "ReuseTime_Days":
                        Result = SetTimeProperty(() => item.RawReuseTime, (TimeSpan valueTime) => item.RawReuseTime = valueTime, 60 * 24, Value);
                        break;
                    case "IsCancellable":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsCancellable = valueBool, Value);
                        break;
                    case "Objectives":
                        Result = Inserter<PgQuestObjective>.AddKeylessArray(item.QuestObjectiveList, Value);
                        break;
                    case "Rewards_XP":
                        Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestRewardList, Value);
                        break;
                    case "Rewards_Currency":
                        Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestRewardList, Value);
                        break;
                    case "Rewards_Items":
                        Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestRewardList, Value);
                        break;
                    case "Reward_CombatXP":
                        Result = ParseRewardCombatXp(item, Value, parsedFile, parsedKey);
                        break;
                    case "FavorNpc":
                        Result = Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => item.FavorNpc = npcLocation, Value, parsedFile, parsedKey);
                        break;
                    case "PrefaceText":
                        Result = SetStringProperty((string valueString) => item.PrefaceText = valueString, Value);
                        break;
                    case "SuccessText":
                        Result = SetStringProperty((string valueString) => item.SuccessText = valueString, Value);
                        break;
                    case "MidwayText":
                        Result = SetStringProperty((string valueString) => item.MidwayText = valueString, Value);
                        break;
                    case "PrerequisiteFavorLevel":
                        Result = Inserter<Favor>.SetEnum((Favor valueEnum) => item.PrerequisiteFavorLevel = valueEnum, Value);
                        break;
                    case "Rewards_Favor":
                        Result = ParseRewardFavor(item, Value, parsedFile, parsedKey);
                        break;
                    case "Rewards_Recipes":
                        Result = ParseRewardRecipes(item, Value, parsedFile, parsedKey);
                        break;
                    case "Rewards_Ability":
                        Result = ParseRewardAbility(item, Value, parsedFile, parsedKey);
                        break;
                    case "Requirements":
                        Result = Inserter<PgQuestRequirement>.AddKeylessArray(item.QuestRequirementList, Value);
                        break;
                    case "Reward_Favor":
                        Result = ParseRewardFavor(item, Value, parsedFile, parsedKey);
                        break;
                    case "Rewards":
                        Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestRewardList, Value);
                        break;
                    case "PreGiveItems":
                        Result = Inserter<PgQuestReward>.AddKeylessArray(item.PreGiveItemList, Value);
                        break;
                    case "TSysLevel":
                        Result = SetIntProperty((int valueInt) => item.RawTSysLevel = valueInt, Value);
                        break;
                    case "Reward_Gold":
                        Result = ParseRewardCurrency(item, Value, parsedFile, parsedKey);
                        break;
                    case "Rewards_NamedLootProfile":
                        Result = SetStringProperty((string valueString) => item.RewardsNamedLootProfile = valueString, Value);
                        break;
                    case "PreGiveRecipes":
                        Result = Inserter<PgRecipe>.AddArrayByInternalName(item.PreGiveRecipeList, Value);
                        break;
                    case "Keywords":
                        Result = StringToEnumConversion<QuestKeyword>.TryParseList(Value, item.KeywordList);
                        break;
                    case "Rewards_Effects":
                        Result = ParseRewardEffects(item, Value, parsedFile, parsedKey);
                        break;
                    case "IsAutoPreface":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsAutoPreface = valueBool, Value);
                        break;
                    case "IsAutoWrapUp":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsAutoWrapUp = valueBool, Value);
                        break;
                    case "GroupingName":
                        Result = Inserter<QuestGroupingName>.SetEnum((QuestGroupingName valueEnum) => item.GroupingName = valueEnum, Value);
                        break;
                    case "IsGuildQuest":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsGuildQuest = valueBool, Value);
                        break;
                    case "NumExpectedParticipants":
                        Result = SetIntProperty((int valueInt) => item.RawNumExpectedParticipants = valueInt, Value);
                        break;
                    case "Level":
                        Result = SetIntProperty((int valueInt) => item.RawLevel = valueInt, Value);
                        break;
                    case "WorkOrderSkill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => item.WorkOrderSkill = valueSkill, Value, parsedFile, parsedKey);
                        break;
                    case "DisplayedLocation":
                        Result = Inserter<MapAreaName>.SetEnum((MapAreaName valueEnum) => item.DisplayedLocation = valueEnum, Value);
                        break;
                    case "FollowUpQuests":
                        Result = ParseFollowUpQuests(item, Value, parsedFile, parsedKey);
                        break;
                    case "PreGiveEffects":
                        Result = ParsePreGiveEffects(item, Value, parsedFile, parsedKey);
                        break;
                    case "MidwayGiveItems":
                        Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestMidwayGiveItemList, Value);
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

        private bool ParseRewardCombatXp(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            int ValueCombatXp;

            if (value is int ValueInt)
                ValueCombatXp = ValueInt;
            else if (value is string ValueString && int.TryParse(ValueString, out int ValueIntFromString))
                ValueCombatXp = ValueIntFromString;
            else
                return Program.ReportFailure($"Value {value} was expected to be an int");

            PgQuestRewardCombatXp NewReward = new PgQuestRewardCombatXp() { RawXp = ValueCombatXp };
            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
            return true;
        }

        private bool ParseRewardFavor(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            int ValueFavor;

            if (value is int ValueInt)
                ValueFavor = ValueInt;
            else if (value is string ValueString && int.TryParse(ValueString, out int ValueIntFromString))
                ValueFavor = ValueIntFromString;
            else
                return Program.ReportFailure($"Value {value} was expected to be an int");

            PgQuestRewardFavor NewReward = new PgQuestRewardFavor() { RawFavor = ValueFavor };
            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
            return true;
        }

        private bool ParseRewardCurrency(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            int ValueCurrency;

            if (value is int ValueInt)
                ValueCurrency = ValueInt;
            else if (value is string ValueString && int.TryParse(ValueString, out int ValueIntFromString))
                ValueCurrency = ValueIntFromString;
            else
                return Program.ReportFailure($"Value {value} was expected to be an int");

            PgQuestRewardCurrency NewReward = new PgQuestRewardCurrency() { Currency = Currency.Gold, RawAmount = ValueCurrency };
            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
            return true;
        }

        private bool ParseRewardRecipes(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ArrayKey))
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

            Dictionary<string, ParsingContext> KeyTable = ParsingContext.ObjectInternalNameTable[typeof(PgRecipe)];

            foreach (object Item in ArrayKey)
            {
                if (!(Item is string ValueKey))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a string");

                if (!KeyTable.ContainsKey(ValueKey))
                    return Program.ReportFailure($"Key '{Item}' is not a known Internal Name");

                if (!(KeyTable[ValueKey].Item is PgRecipe AsLink))
                    return Program.ReportFailure($"Key '{Item}' was found but for the wrong object type");

                PgQuestRewardRecipe NewReward = new PgQuestRewardRecipe() { Recipe = AsLink };
                ParsingContext.AddSuplementaryObject(NewReward);
                item.QuestRewardList.Add(NewReward);
            }

            return true;
        }

        private bool ParseRewardAbility(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            PgAbility ParsedAbility = null;

            if (!Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => ParsedAbility = valueAbility, value))
                return false;

            PgQuestRewardAbility NewReward = new PgQuestRewardAbility() { Ability = ParsedAbility };
            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);

            return true;
        }

        private bool ParseRewardEffects(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> StringArray))
                return Program.ReportFailure($"Value {value} was expected to be a list");

            foreach (object StringItem in StringArray)
            {
                if (!(StringItem is string EffectString))
                    return Program.ReportFailure($"Value {StringItem} was expected to be a string");

                if (!ParseRewardEffect(item, EffectString, parsedFile, parsedKey))
                    return false;
            }

            return true;
        }

        private bool ParseRewardEffect(PgQuest item, string effectString, string parsedFile, string parsedKey)
        {
            int ParameterStartIndex = effectString.IndexOf('(');
            int ParameterEndIndex = effectString.LastIndexOf(')');
            string EffectName = (ParameterStartIndex < 0 || ParameterEndIndex < effectString.Length - 1) ? effectString : effectString.Substring(0, ParameterStartIndex);
            string EffectParameter = (ParameterStartIndex < 0 || ParameterEndIndex < effectString.Length - 1) ? string.Empty : effectString.Substring(ParameterStartIndex + 1, effectString.Length - 2 - ParameterStartIndex);

            bool Result;
            PgEffect ParsedEffect = null;

            switch (EffectName)
            {
                case "SetInteractionFlag":
                    Result = ParseRewardEffectSetInteractionFlag(item, EffectParameter, parsedFile, parsedKey);
                    break;
                case "EnsureLoreBookKnown":
                    Result = Inserter<PgLoreBook>.SetItemByInternalName((PgLoreBook valueLoreBook) => AddRewardEffectLoreBookKnown(item, valueLoreBook), EffectParameter);
                    break;
                case "BestowTitle":
                    Result = ParseRewardEffectBestowTitle(item, EffectParameter, parsedFile, parsedKey);
                    break;
                case "LearnAbility":
                    Result = ParseRewardAbility(item, EffectParameter, parsedFile, parsedKey);
                    break;
                case "AdvanceScriptedQuestObjective":
                    Result = ParseRewardEffectAdvanceScriptedQuestObjective(item, EffectParameter, parsedFile, parsedKey);
                    break;
                case "GiveXP":
                    Result = ParseRewardEffectGiveXP(item, EffectParameter, parsedFile, parsedKey);
                    break;
                case "DeltaNpcFavor":
                    Result = ParseRewardEffectDeltaNpcFavor(item, EffectParameter, parsedFile, parsedKey);
                    break;
                case "RaiseSkillToLevel":
                    Result = ParseRewardEffectRaiseSkillToLevel(item, EffectParameter, parsedFile, parsedKey);
                    break;
                default:
                    Result = Inserter<PgEffect>.SetItemByName((PgEffect valueEffect) => ParsedEffect = valueEffect, effectString);
                    if (Result)
                    {
                        PgQuestRewardEffect NewReward = new PgQuestRewardEffect() { Effect = ParsedEffect };
                        ParsingContext.AddSuplementaryObject(NewReward);
                        item.QuestRewardList.Add(NewReward);
                    }
                    break;
            }

            return Result;
        }

        private bool ParseRewardEffectSetInteractionFlag(PgQuest item, string effectParameter, string parsedFile, string parsedKey)
        {
            if (item.RewardInteractionFlagList.Contains(effectParameter))
                return Program.ReportFailure(parsedFile, parsedKey, $"Interaction flag '{effectParameter}' already parsed");

            item.RewardInteractionFlagList.Add(effectParameter);
            return true;
        }

        private static void AddRewardEffectLoreBookKnown(PgQuest item, PgLoreBook valueLoreBook)
        {
            PgQuestRewardLoreBook NewReward = new PgQuestRewardLoreBook() { LoreBook = valueLoreBook };
            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
        }

        private bool ParseRewardEffectBestowTitle(PgQuest item, string effectParameter, string parsedFile, string parsedKey)
        {
            if (ParserPlayerTitle.TitleToKeyMap.ContainsKey(effectParameter))
                effectParameter = ParserPlayerTitle.TitleToKeyMap[effectParameter];

            return Inserter<PgPlayerTitle>.SetItemByKey((PgPlayerTitle valuePlayerTitle) => AddRewardEffectPlayerTitle(item, valuePlayerTitle), effectParameter);
        }

        private static void AddRewardEffectPlayerTitle(PgQuest item, PgPlayerTitle valuePlayerTitle)
        {
            PgQuestRewardPlayerTitle NewReward = new PgQuestRewardPlayerTitle() { PlayerTitle = valuePlayerTitle };
            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
        }

        private bool ParseRewardEffectAdvanceScriptedQuestObjective(PgQuest item, string effectParameter, string parsedFile, string parsedKey)
        {
            if (effectParameter.EndsWith("_Complete"))
                effectParameter = effectParameter.Substring(0, effectParameter.Length - 9);
            else if (effectParameter.EndsWith("_Done"))
                effectParameter = effectParameter.Substring(0, effectParameter.Length - 5);

            return Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => item.QuestCompleteNpc = npcLocation, effectParameter, parsedFile, parsedKey);
        }

        private bool ParseRewardEffectGiveXP(PgQuest item, string effectParameter, string parsedFile, string parsedKey)
        {
            string[] Split = effectParameter.Split(',');

            if (Split.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Skill XP reward '{effectParameter}' not parsed");

            string SkillName = Split[0];
            PgSkill ParsedSkill = null;
            if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, SkillName))
                return false;

            if (!int.TryParse(Split[1], out int XpValue))
                return Program.ReportFailure(parsedFile, parsedKey, $"Skill XP reward '{effectParameter}': int expected");

            PgQuestRewardSkillXp NewReward = new PgQuestRewardSkillXp();
            NewReward.XpTable.Add(ParsedSkill, XpValue);

            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
            return true;
        }

        private bool ParseRewardEffectDeltaNpcFavor(PgQuest item, string effectParameter, string parsedFile, string parsedKey)
        {
            string[] Split = effectParameter.Split(',');

            if (Split.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"NPC favor reward '{effectParameter}' not parsed");

            PgQuestRewardFavor NewReward = new PgQuestRewardFavor();

            string NpcName = Split[0];

            if (!int.TryParse(Split[1], out int FavorValue))
                return Program.ReportFailure(parsedFile, parsedKey, $"NPC favor reward '{effectParameter}': int expected");

            if (!Inserter<PgQuestRewardFavor>.SetNpc((PgNpcLocation npcLocation) => NewReward.FavorNpcLocation = npcLocation, NpcName, parsedFile, parsedKey))
                return false;

            NewReward.RawFavor = FavorValue;

            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
            return true;
        }

        private bool ParseRewardEffectRaiseSkillToLevel(PgQuest item, string effectParameter, string parsedFile, string parsedKey)
        {
            string[] Split = effectParameter.Split(',');

            if (Split.Length != 2)
                return Program.ReportFailure(parsedFile, parsedKey, $"Raise to level reward '{effectParameter}' not parsed");

            string SkillName = Split[0].Trim();
            PgSkill ParsedSkill = null;
            if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, SkillName))
                return false;

            if (!int.TryParse(Split[1], out int LevelValue))
                return Program.ReportFailure(parsedFile, parsedKey, $"Raise to level reward '{effectParameter}': int expected");

            PgQuestRewardLevel NewReward = new PgQuestRewardLevel();
            NewReward.Skill = ParsedSkill;
            NewReward.RawLevel = LevelValue;

            ParsingContext.AddSuplementaryObject(NewReward);
            item.QuestRewardList.Add(NewReward);
            return true;
        }

        private bool ParseFollowUpQuests(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ArrayString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a list");

            foreach (object Item in ArrayString)
            {
                if (!(Item is string ValueString))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Value '{Item}' was expected to be a string");

                PgQuest ParsedQuest = null;
                if (!Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => ParsedQuest = valueQuest, ValueString))
                    return false;

                item.FollowUpQuestList.Add(ParsedQuest);
            }

            return true;
        }

        private bool ParsePreGiveEffects(PgQuest item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ArrayString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a list");

            foreach (object Item in ArrayString)
            {
                if (!(Item is string ValueString))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Value '{Item}' was expected to be a string");

                item.PreGiveEffectList.Add(ValueString);
            }

            return true;
        }

        public static void UpdateIconsAndNames()
        {
            Dictionary<string, ParsingContext> QuestParsingTable = ParsingContext.ObjectKeyTable[typeof(PgQuest)];
            foreach (KeyValuePair<string, ParsingContext> Entry in QuestParsingTable)
            {
                PgQuest Quest = (PgQuest)Entry.Value.Item;

                int IconId = 0;

                UpdateIconIdFromRewards(Quest, ref IconId);
                UpdateIconIdFromObjectives(Quest, ref IconId);
                UpdateIconIdFromGenericObjectives(Quest, ref IconId);

                Quest.IconId = IconId;

                Debug.Assert(Quest.ObjectIconId != 0);
                Debug.Assert(Quest.ObjectName.Length > 0);
            }
        }

        private static void UpdateIconIdFromRewards(PgQuest quest, ref int iconId)
        {
            foreach (PgQuestReward QuestReward in quest.QuestRewardList)
            {
                if (iconId != 0)
                    break;

                switch (QuestReward)
                {
                    case PgQuestRewardAbility AsRewardAbility:
                        if (AsRewardAbility.Ability.IconId != 0)
                            iconId = AsRewardAbility.Ability.IconId;
                        break;
                    case PgQuestRewardItem AsRewardItem:
                        if (AsRewardItem.Item.IconId != 0)
                            iconId = AsRewardItem.Item.IconId;
                        break;
                    case PgQuestRewardRecipe AsRewardRecipe:
                        if (AsRewardRecipe.Recipe.IconId != 0)
                            iconId = AsRewardRecipe.Recipe.IconId;
                        break;
                    case PgQuestRewardLoreBook AsRewardLoreBook:
                        if (iconId == 0)
                            iconId = PgObject.LoreBookIconId;
                        break;
                    case PgQuestRewardPlayerTitle AsRewardPlayerTitle:
                        if (iconId == 0)
                            iconId = PgObject.PlayerTitleIconId;
                        break;
                    case PgQuestRewardSkillXp AsRewardSkillXp:
                        foreach (KeyValuePair<PgSkill, int> Entry in AsRewardSkillXp.XpTable)
                            if (iconId == 0)
                                iconId = ParserSkill.SkillToIcon(Entry.Key);
                        break;
                }
            }
        }

        private static void UpdateIconIdFromObjectives(PgQuest quest, ref int iconId)
        {
            foreach (PgQuestObjective QuestObjective in quest.QuestObjectiveList)
            {
                if (iconId != 0)
                    break;

                switch (QuestObjective)
                {
                    case PgQuestObjectiveCollect AsObjectiveCollect:
                        if (AsObjectiveCollect.Item != null && AsObjectiveCollect.Item.IconId != 0)
                            iconId = AsObjectiveCollect.Item.IconId;
                        break;
                    case PgQuestObjectiveDeliver AsObjectiveDeliver:
                        if (AsObjectiveDeliver.Item != null && AsObjectiveDeliver.Item.IconId != 0)
                            iconId = AsObjectiveDeliver.Item.IconId;
                        break;
                    case PgQuestObjectiveGuildGiveItem AsObjectiveGuildGiveItem:
                        if (AsObjectiveGuildGiveItem.Item != null && AsObjectiveGuildGiveItem.Item.IconId != 0)
                            iconId = AsObjectiveGuildGiveItem.Item.IconId;
                        break;
                    case PgQuestObjectiveHarvest AsObjectiveHarvest:
                        if (AsObjectiveHarvest.Item != null && AsObjectiveHarvest.Item.IconId != 0)
                            iconId = AsObjectiveHarvest.Item.IconId;
                        break;
                    case PgQuestObjectiveHave AsObjectiveHave:
                        if (AsObjectiveHave.Item != null && AsObjectiveHave.Item.IconId != 0)
                            iconId = AsObjectiveHave.Item.IconId;
                        break;
                    case PgQuestObjectiveKill AsObjectiveKill:
                        UpdateIconIdFromAbilityKeyword(ref iconId, AsObjectiveKill.Keyword);
                        break;
                    case PgQuestObjectiveLoot AsObjectiveLoot:
                        if (AsObjectiveLoot.Item != null && AsObjectiveLoot.Item.IconId != 0)
                            iconId = AsObjectiveLoot.Item.IconId;
                        break;
                    case PgQuestObjectiveScriptedReceiveItem AsObjectiveScriptedReceiveItem:
                        if (AsObjectiveScriptedReceiveItem.Item != null && AsObjectiveScriptedReceiveItem.Item.IconId != 0)
                            iconId = AsObjectiveScriptedReceiveItem.Item.IconId;
                        break;
                    case PgQuestObjectiveUseAbility AsObjectiveUseAbility:
                        UpdateIconIdFromAbilityKeyword(ref iconId, AsObjectiveUseAbility.Target);
                        break;
                    case PgQuestObjectiveUseAbilityOnTargets AsObjectiveUseAbilityOnTargets:
                        UpdateIconIdFromAbilityKeyword(ref iconId, AsObjectiveUseAbilityOnTargets.Keyword);
                        break;
                    case PgQuestObjectiveUseItem AsObjectiveUseItem:
                        if (AsObjectiveUseItem.Item != null && AsObjectiveUseItem.Item.IconId != 0)
                            iconId = AsObjectiveUseItem.Item.IconId;
                        break;
                    case PgQuestObjectiveUseRecipe AsObjectiveUseRecipe:
                        UpdateIconIdFromRecipeKeyword(ref iconId, AsObjectiveUseRecipe.Target);
                        break;
                }
            }
        }

        private static void UpdateIconIdFromGenericObjectives(PgQuest quest, ref int iconId)
        {
            foreach (PgQuestObjective QuestObjective in quest.QuestObjectiveList)
            {
                if (iconId != 0)
                    break;

                switch (QuestObjective)
                {
                    case PgQuestObjectiveCollect AsObjectiveCollect:
                        UpdateIconIdFromItemKeyword(ref iconId, AsObjectiveCollect.Target);
                        break;
                    case PgQuestObjectiveKill _:
                        iconId = PgObject.KillIconId;
                        break;
                    case PgQuestObjectiveDruidKill _:
                        iconId = PgObject.KillIconId;
                        break;
                    case PgQuestObjectiveScripted _:
                        iconId = PgObject.KillIconId;
                        break;
                    case PgQuestObjectiveCompleteQuest _:
                        iconId = PgObject.KillIconId;
                        break;
                }
            }
        }

        private static void UpdateIconIdFromAbilityKeyword(ref int iconId, AbilityKeyword keyword)
        {
            if (keyword == AbilityKeyword.Internal_None)
                return;

            Dictionary<string, ParsingContext> AbilityParsingTable = ParsingContext.ObjectKeyTable[typeof(PgAbility)];
            foreach (KeyValuePair<string, ParsingContext> Entry in AbilityParsingTable)
            {
                PgAbility Ability = (PgAbility)Entry.Value.Item;
                if (Ability.KeywordList.Contains(keyword))
                {
                    iconId = Ability.IconId;
                    break;
                }
            }
        }

        private static void UpdateIconIdFromItemKeyword(ref int iconId, ItemKeyword keyword)
        {
            if (keyword == ItemKeyword.Internal_None)
                return;

            Dictionary<string, ParsingContext> ItemParsingTable = ParsingContext.ObjectKeyTable[typeof(PgItem)];
            foreach (KeyValuePair<string, ParsingContext> Entry in ItemParsingTable)
            {
                PgItem Item = (PgItem)Entry.Value.Item;
                if (Item.KeywordTable.ContainsKey(keyword))
                {
                    iconId = Item.IconId;
                    break;
                }
            }
        }

        private static void UpdateIconIdFromRecipeKeyword(ref int iconId, RecipeKeyword keyword)
        {
            if (keyword == RecipeKeyword.Internal_None)
                return;

            Dictionary<string, ParsingContext> RecipeParsingTable = ParsingContext.ObjectKeyTable[typeof(PgRecipe)];
            foreach (KeyValuePair<string, ParsingContext> Entry in RecipeParsingTable)
            {
                PgRecipe Recipe = (PgRecipe)Entry.Value.Item;
                if (Recipe.KeywordList.Contains(keyword))
                {
                    iconId = Recipe.IconId;
                    break;
                }
            }
        }
    }
}
