namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserQuest : Parser
{
    public override object CreateItem()
    {
        return new PgQuest();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgQuest AsPgQuest)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgQuest, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgQuest item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        int? RawTSysLevel = null;

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
                    Result = SetStringProperty((string valueString) => item.Description = Tools.CleanedUpString(valueString), Value);
                    break;
                case "Version":
                    Result = SetIntProperty((int valueInt) => item.RawVersion = valueInt, Value);
                    break;
                case "RequirementsToSustain":
                    Result = Inserter<PgQuestRequirement>.AddKeylessArray(item.QuestRequirementToSustainList, Value);
                    break;
                case "ReuseTime":
                    Result = Inserter<PgQuestTime>.SetItemProperty((PgQuestTime valueQuestTime) => item.RawReuseTime = valueQuestTime.ToTime(), Value);
                    break;
                case "IsCancellable":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsCancellable(valueBool), Value);
                    break;
                case "Objectives":
                    Result = Inserter<PgQuestObjective>.AddKeylessArray(item.QuestObjectiveList, Value);
                    break;
                case "Rewards_Items":
                    Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestRewardList, Value);
                    break;
                case "QuestNpc":
                    Result = Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => item.QuestNpc = npcLocation, Value, parsedFile, parsedKey);
                    break;
                case "FavorNpc":
                    Result = Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => item.FavorNpc = npcLocation, Value, parsedFile, parsedKey);
                    break;
                case "PrefaceText":
                    Result = SetStringProperty((string valueString) => item.PrefaceText = Tools.CleanedUpString(valueString), Value);
                    break;
                case "SuccessText":
                    Result = SetStringProperty((string valueString) => item.SuccessText = Tools.CleanedUpString(valueString), Value);
                    break;
                case "MidwayText":
                    Result = SetStringProperty((string valueString) => item.MidwayText = Tools.CleanedUpString(valueString), Value);
                    break;
                case "PrerequisiteFavorLevel":
                    Result = StringToEnumConversion<Favor>.SetEnum((Favor valueEnum) => item.PrerequisiteFavorLevel = valueEnum, Value);
                    break;
                case "Requirements":
                    Result = Inserter<PgQuestRequirement>.AddKeylessArray(item.QuestRequirementList, Value);
                    break;
                case "Rewards":
                    Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestRewardList, Value);
                    break;
                case "PreGiveItems":
                    Result = Inserter<PgQuestReward>.AddKeylessArray(item.PreGiveItemList, Value);
                    break;
                case "TSysLevel":
                    Result = SetIntProperty((int valueInt) => RawTSysLevel = valueInt, Value);
                    break;
                case "PreGiveRecipes":
                    Result = Inserter<PgRecipe>.AddPgObjectArrayByInternalName<PgRecipe>(item.PreGiveRecipeList, Value);
                    break;
                case "Keywords":
                    Result = StringToEnumConversion<QuestKeyword>.TryParseList(Value, item.KeywordList);
                    break;
                case "IsAutoPreface":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsAutoPreface(valueBool), Value);
                    break;
                case "IsAutoWrapUp":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsAutoWrapUp(valueBool), Value);
                    break;
                case "GroupingName":
                    Result = StringToEnumConversion<QuestGroupingName>.SetEnum((QuestGroupingName valueEnum) => item.GroupingName = valueEnum, Value);
                    break;
                case "IsGuildQuest":
                    Result = SetBoolProperty((bool valueBool) => item.SetIsGuildQuest(valueBool), Value);
                    break;
                case "NumberOfExpectedParticipants":
                    Result = SetIntProperty((int valueInt) => item.RawNumExpectedParticipants = valueInt, Value);
                    break;
                case "Level":
                    Result = SetIntProperty((int valueInt) => item.RawLevel = valueInt, Value);
                    break;
                case "WorkOrderSkill":
                    Result = ParserSkill.Parse((PgSkill valueSkill) => item.WorkOrderSkill_Key = PgObject.GetItemKey(valueSkill), Value, parsedFile, parsedKey);
                    break;
                case "DisplayedLocation":
                    Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => item.DisplayedLocation = valueEnum, Value);
                    break;
                case "FollowUpQuests":
                    Result = Inserter<PgQuest>.AddPgObjectArrayByInternalName<PgQuest>(item.FollowUpQuestList, Value);
                    break;
                case "PreGiveEffects":
                    Result = Inserter<PgQuestPreGiveEffect>.AddKeylessArray(item.PreGiveEffectList, Value);
                    break;
                case "MidwayGiveItems":
                    Result = Inserter<PgQuestReward>.AddKeylessArray(item.QuestMidwayGiveItemList, Value);
                    break;
                case "RewardsDescription":
                    Result = SetStringProperty((string valueString) => item.RewardsDescription = Tools.CleanedUpString(valueString), Value);
                    break;
                case "CheckRequirementsToSustainOnBestow":
                    Result = SetBoolProperty((bool valueBool) => item.SetCheckRequirementsToSustainOnBestow(valueBool), Value);
                    break;
                case "QuestFailEffects":
                    Result = Inserter<PgQuestFailEffect>.AddKeylessArray(item.QuestFailEffectList, Value);
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
            if (item.RawLevel.HasValue && RawTSysLevel.HasValue)
                return Program.ReportFailure(parsedFile, parsedKey, "Both levels set");

            if (RawTSysLevel.HasValue)
            {
                Debug.Assert(!item.RawLevel.HasValue);
                item.RawLevel = RawTSysLevel;
            }
        }

        return Result;
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
            UpdateIconIdFromGenericRewards(Quest, ref IconId);

            if (IconId == 0)
            {
                IconId = 2001;
            }

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

            PgAbility Ability;
            PgItem Item;
            PgRecipe Recipe;
            PgSkill Skill;

            switch (QuestReward)
            {
                case PgQuestRewardAbility AsRewardAbility:
                    Ability = ParsingContext.GetParsedAbilityByKey(AsRewardAbility.Ability_Key);
                    if (Ability.IconId != 0)
                        iconId = Ability.IconId;
                    break;
                case PgQuestRewardItem AsRewardItem:
                    Item = ParsingContext.GetParsedItemByKey(AsRewardItem.Item_Key);
                    if (Item.IconId != 0)
                        iconId = Item.IconId;
                    break;
                case PgQuestRewardRecipe AsRewardRecipe:
                    Recipe = ParsingContext.GetParsedRecipeByKey(AsRewardRecipe.Recipe_Key);
                    if (Recipe.IconId != 0)
                        iconId = Recipe.IconId;
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
                    if (iconId == 0)
                    {
                        Skill = ParsingContext.GetParsedSkillByKey(AsRewardSkillXp.Skill_Key);
                        iconId = ParserSkill.SkillToIcon(Skill);
                    }
                    break;
            }
        }
    }

    private static void UpdateIconIdFromGenericRewards(PgQuest quest, ref int iconId)
    {
        foreach (PgQuestReward QuestReward in quest.QuestRewardList)
        {
            if (iconId != 0)
                break;

            switch (QuestReward)
            {
                case PgQuestRewardCurrency AsRewardCurrency:
                    if (iconId == 0)
                        iconId = ParserReward.CurrencyToIcon(AsRewardCurrency.Currency);
                    break;
                case PgQuestRewardWorkOrderCurrency AsRewardWorkOrderCurrency:
                    if (iconId == 0)
                        iconId = ParserReward.CurrencyToIcon(AsRewardWorkOrderCurrency.Currency);
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

            PgItem Item;
            PgRecipe Recipe;

            switch (QuestObjective)
            {
                case PgQuestObjectiveCollectItem AsObjectiveCollectItem:
                    if (AsObjectiveCollectItem.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveCollectItem.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveDeliver AsObjectiveDeliver:
                    if (AsObjectiveDeliver.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveDeliver.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveGuildGiveItem AsObjectiveGuildGiveItem:
                    if (AsObjectiveGuildGiveItem.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveGuildGiveItem.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveHarvestItem AsObjectiveHarvestItem:
                    if (AsObjectiveHarvestItem.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveHarvestItem.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveHaveItem AsObjectiveHaveItem:
                    if (AsObjectiveHaveItem.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveHaveItem.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveKill AsObjectiveKill:
                    if (AsObjectiveKill.QuestObjectiveRequirementList.Count > 0 && AsObjectiveKill.QuestObjectiveRequirementList[0] is PgQuestObjectiveRequirementUseAbility UseAbilityRequirement)
                        UpdateIconIdFromAbilityKeyword(ref iconId, UseAbilityRequirement.Keyword);
                    break;
                case PgQuestObjectiveLootItem AsObjectiveLootItem:
                    if (AsObjectiveLootItem.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveLootItem.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveScriptedReceiveItem AsObjectiveScriptedReceiveItem:
                    if (AsObjectiveScriptedReceiveItem.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveScriptedReceiveItem.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveUseAbility AsObjectiveUseAbility:
                    UpdateIconIdFromAbilityKeyword(ref iconId, AsObjectiveUseAbility.Keyword);
                    break;
                case PgQuestObjectiveUseAbilityOnTargets AsObjectiveUseAbilityOnTargets:
                    UpdateIconIdFromAbilityKeyword(ref iconId, AsObjectiveUseAbilityOnTargets.Keyword);
                    break;
                case PgQuestObjectiveUseItem AsObjectiveUseItem:
                    if (AsObjectiveUseItem.Item_Key != null)
                    {
                        Item = ParsingContext.GetParsedItemByKey(AsObjectiveUseItem.Item_Key);
                        if (Item.IconId != 0)
                            iconId = Item.IconId;
                    }
                    break;
                case PgQuestObjectiveUseRecipe AsObjectiveUseRecipe:
                    if (AsObjectiveUseRecipe.Target_Key is not null)
                    {
                        Recipe = ParsingContext.GetParsedRecipeByKey(AsObjectiveUseRecipe.Target_Key);
                        if (Recipe.IconId != 0)
                            iconId = Recipe.IconId;
                    }
                    else
                        UpdateIconIdFromRecipeKeyword(ref iconId, AsObjectiveUseRecipe.TargetKeyword);
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
                case PgQuestObjectiveCollectItemKeyword AsObjectiveCollectItemKeyword:
                    UpdateIconIdFromItemKeyword(ref iconId, AsObjectiveCollectItemKeyword.Keyword);
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
                case PgQuestObjectiveScriptAtomicInt _:
                    iconId = PgObject.DirectedGoalIconId;
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

            bool IsFound = false;
            foreach (PgItemKeywordValues KeywordValues in Item.KeywordValuesList)
            {
                ItemKeyword Keyword = KeywordValues.Keyword;
                if (Keyword == keyword)
                    IsFound = true;
            }

            if (IsFound)
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
