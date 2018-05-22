using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Quest : GenericJsonObject<Quest>
    {
        #region Direct Properties
        public string InternalName { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get; private set; }
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get; private set; }
        public List<QuestObjective> QuestObjectiveList { get; } = new List<QuestObjective>();
        public List<QuestRewardXp> RewardsXPList { get; } = new List<QuestRewardXp>();
        public List<QuestRewardItem> QuestRewardsItemList { get; } = new List<QuestRewardItem>();
        public TimeSpan? RawReuseTime { get; private set; }
        public int RewardCombatXP { get { return RawRewardCombatXP.HasValue ? RawRewardCombatXP.Value : 0; } }
        public int? RawRewardCombatXP { get; private set; }
        public string FavorNpcId { get; private set; }
        private bool IsFavorNpcParsed;
        public GameNpc FavorNpc { get; private set; }
        public string FavorNpcName { get; private set; }
        public MapAreaName FavorNpcArea { get; private set; }
        public string PrefaceText { get; private set; }
        public string SuccessText { get; private set; }
        public string MidwayText { get; private set; }
        public Favor PrerequisiteFavorLevel { get; private set; }
        public Ability RewardAbility { get; private set; }
        private string RawRewardAbility;
        private bool IsRawRewardAbilityParsed;
        public int RewardFavor { get { return RawRewardFavor.HasValue ? RawRewardFavor.Value : 0; } }
        public int? RawRewardFavor { get; private set; }
        public PowerSkill RewardSkill { get; private set; }
        public Skill ConnectedRewardSkill { get; private set; }
        private bool IsConnectedRewardSkillParsed;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get; private set; }
        public Recipe RewardRecipe { get; private set; }
        private string RawRewardRecipe;
        private bool IsRawRewardRecipeParsed;
        public int RewardGuildXp { get { return RawRewardGuildXp.HasValue ? RawRewardGuildXp.Value : 0; } }
        public int? RawRewardGuildXp { get; private set; }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get; private set; }
        public List<QuestRewardItem> PreGiveItemList { get; } = new List<QuestRewardItem>();
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        public int? RawTSysLevel { get; private set; }
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        public int? RawRewardGold { get; private set; }
        public string RewardsNamedLootProfile { get; private set; }
        public List<Recipe> PreGiveRecipeList { get; private set; } = new List<Recipe>();
        private List<string> RawPreGiveRecipeList { get; } = new List<string>();
        public List<QuestKeyword> KeywordList { get; } = new List<QuestKeyword>();
        public Effect RewardEffect { get; private set; }
        private string RawRewardEffect;
        private bool IsRawRewardEffectParsed;
        private List<string> RawRewardInteractionFlags = new List<string>();
        private string RawRewardEnsureLoreBook;
        private bool IsRawLoreBookParsed;
        public LoreBook RewardLoreBook { get; private set; }
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        public bool? RawIsAutoPreface { get; private set; }
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        public bool? RawIsAutoWrapUp { get; private set; }
        public QuestGroupingName GroupingName { get; private set; }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        public bool? RawIsGuildQuest { get; private set; }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get; private set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; private set; }
        public PowerSkill WorkOrderSkill { get; private set; }
        public Skill ConnectedWorkOrderSkill { get; private set; }
        private bool IsConnectedWorkOrderSkillParsed;
        public MapAreaName DisplayedLocation { get; private set; }
        public List<Quest> FollowUpQuestList { get; private set; } = new List<Quest>();
        private List<string> RawFollowUpQuestList = new List<string>();
        public List<QuestRequirement> QuestRequirementList { get; private set; } = new List<QuestRequirement>();
        public List<QuestRequirement> QuestRequirementToSustainList { get; private set; } = new List<QuestRequirement>();
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Name; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get; } = new Dictionary<string, FieldParser>()
        {
            { "InternalName", ParseFieldInternalName },
            { "Name", ParseFieldName },
            { "Description", ParseFieldDescription },
            { "Version", ParseFieldVersion },
            { "RequirementsToSustain", ParseFieldRequirementsToSustain },
            { "ReuseTime_Minutes", ParseFieldReuseTimeMinutes },
            { "IsCancellable", ParseFieldIsCancellable },
            { "Objectives", ParseFieldObjectives },
            { "Rewards_XP", ParseFieldRewardsXP },
            { "Rewards_Items", ParseFieldRewardsItems },
            { "ReuseTime_Days", ParseFieldReuseTimeDays },
            { "ReuseTime_Hours", ParseFieldReuseTimeHours },
            { "Reward_CombatXP", ParseFieldRewardCombatXP },
            { "FavorNpc", ParseFieldFavorNpc },
            { "PrefaceText", ParseFieldPrefaceText },
            { "SuccessText", ParseFieldSuccessText },
            { "MidwayText", ParseFieldMidwayText },
            { "PrerequisiteFavorLevel", ParseFieldPrerequisiteFavorLevel },
            { "Rewards_Favor", ParseFieldRewardsFavor },
            { "Rewards_Recipes", ParseFieldRewardsRecipes },
            { "Rewards_Ability", ParseFieldRewardsAbility },
            { "Requirements", ParseFieldRequirements },
            { "Reward_Favor", ParseFieldRewardFavor },
            { "Rewards", ParseFieldRewards },
            { "PreGiveItems", ParseFieldPreGiveItems },
            { "TSysLevel", ParseFieldTSysLevel },
            { "Reward_Gold", ParseFieldRewardGold },
            { "Rewards_NamedLootProfile", ParseFieldRewardsNamedLootProfile },
            { "PreGiveRecipes", ParseFieldPreGiveRecipes },
            { "Keywords", ParseFieldKeywords },
            { "Rewards_Effects", ParseFieldRewardsEffects },
            { "IsAutoPreface", ParseFieldIsAutoPreface },
            { "IsAutoWrapUp", ParseFieldIsAutoWrapUp },
            { "GroupingName", ParseFieldGroupingName },
            { "IsGuildQuest", ParseFieldIsGuildQuest },
            { "NumExpectedParticipants", ParseFieldNumExpectedParticipants },
            { "Level", ParseFieldLevel },
            { "WorkOrderSkill", ParseFieldWorkOrderSkill },
            { "DisplayedLocation", ParseFieldDisplayedLocation },
            { "FollowUpQuests", ParseFieldFollowUpQuests },
        };

        private static void ParseFieldInternalName(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest InternalName", This.ParseInternalName);
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;
        }

        private static void ParseFieldName(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest Name", This.ParseName);
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
            ErrorInfo.AddIconId(SearchResultIconId);
        }

        private static void ParseFieldDescription(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest Description", This.ParseDescription);
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
        }

        private static void ParseFieldVersion(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest Version", This.ParseVersion);
        }

        private void ParseVersion(long RawVersion, ParseErrorInfo ErrorInfo)
        {
            this.RawVersion = (int)RawVersion;
        }

        private static void ParseFieldRequirementsToSustain(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringObjectOrArray(Value, ErrorInfo, "Quest RequirementsToSustain", This.ParseRequirementToSustainAsDictionary);
        }

        private void ParseRequirementToSustainAsDictionary(JsonObject RawRequirements, ParseErrorInfo ErrorInfo)
        {
            QuestRequirement ParsedQuestRequirement;
            JsonObjectParser<QuestRequirement>.InitAsSubitem("Requirements", RawRequirements, out ParsedQuestRequirement, ErrorInfo);

            QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
            if (ConvertedQuestRequirement != null)
                QuestRequirementToSustainList.Add(ConvertedQuestRequirement);
        }

        private static void ParseFieldReuseTimeMinutes(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest ReuseTimeMinutes", This.ParseReuseTimeMinutes);
        }

        private void ParseReuseTimeMinutes(long RawReuseTimeMinutes, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromMinutes(RawReuseTimeMinutes);
            else
                RawReuseTime += TimeSpan.FromMinutes(RawReuseTimeMinutes);
        }

        private static void ParseFieldIsCancellable(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "Quest IsCancellable", This.ParseIsCancellable);
        }

        private void ParseIsCancellable(bool RawIsCancellable, ParseErrorInfo ErrorInfo)
        {
            this.RawIsCancellable = RawIsCancellable;
        }

        private static void ParseFieldObjectives(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueArray(Value, ErrorInfo, "Quest Objectives", This.ParseObjectives);
        }

        private void ParseObjectives(JsonObject RawObjectives, ParseErrorInfo ErrorInfo)
        {
            QuestObjective ParsedQuestObjective;
            JsonObjectParser<QuestObjective>.InitAsSubitem("Quest Objectives", RawObjectives, out ParsedQuestObjective, ErrorInfo);

            QuestObjective ConvertedQuestObjective = ParsedQuestObjective.ToSpecificQuestObjective(ErrorInfo);
            if (ConvertedQuestObjective != null)
                QuestObjectiveList.Add(ConvertedQuestObjective);
        }

        private static void ParseFieldRewardsXP(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueArray(Value, ErrorInfo, "Quest RewardsXP", This.ParseRewardsXP);
        }

        private void ParseRewardsXP(JsonObject RawRewardsXP, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> RawRewardXP in RawRewardsXP)
            {
                PowerSkill ParsedSkill;
                if (StringToEnumConversion<PowerSkill>.TryParse(RawRewardXP.Key, out ParsedSkill, ErrorInfo))
                {
                    JsonInteger AsJsonInteger;
                    if ((AsJsonInteger = RawRewardXP.Value as JsonInteger) != null)
                    {
                        QuestRewardXp NewReward = new QuestRewardXp();
                        NewReward.Skill = ParsedSkill;
                        NewReward.Xp = AsJsonInteger.Number;
                        RewardsXPList.Add(NewReward);
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest RewardsXP");
                }
            }
        }

        private static void ParseFieldRewardsItems(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueArray(Value, ErrorInfo, "Quest RewardsItems", This.ParseRewardsItems);
        }

        private void ParseRewardsItems(JsonObject RawRewardsItems, ParseErrorInfo ErrorInfo)
        {
            QuestRewardItem ParsedRewardItem;
            JsonObjectParser<QuestRewardItem>.InitAsSubitem("RewardsItems", RawRewardsItems, out ParsedRewardItem, ErrorInfo);
            if (ParsedRewardItem != null)
                QuestRewardsItemList.Add(ParsedRewardItem);
        }

        private static void ParseFieldReuseTimeDays(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest ReuseTimeDays", This.ParseReuseTimeDays);
        }

        private void ParseReuseTimeDays(long RawReuseTimeDays, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromDays(RawReuseTimeDays);
            else
                RawReuseTime += TimeSpan.FromDays(RawReuseTimeDays);
        }

        private static void ParseFieldReuseTimeHours(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest ReuseTimeHours", This.ParseReuseTimeHours);
        }

        private void ParseReuseTimeHours(long RawReuseTimeHours, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromHours(RawReuseTimeHours);
            else
                RawReuseTime += TimeSpan.FromHours(RawReuseTimeHours);
        }

        private static void ParseFieldRewardCombatXP(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest RewardCombatXP", This.ParseRewardCombatXP);
        }

        private void ParseRewardCombatXP(long RawRewardCombatXP, ParseErrorInfo ErrorInfo)
        {
            if (this.RawRewardCombatXP == null)
                this.RawRewardCombatXP = (int)RawRewardCombatXP;
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardCombatXP");
        }

        private static void ParseFieldFavorNpc(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest FavorNpc", This.ParseFavorNpc);
        }

        private void ParseFavorNpc(string RawFavorNpc, ParseErrorInfo ErrorInfo)
        {
            if (RawFavorNpc.Length == 0)
                return;

            MapAreaName ParsedArea;
            string NpcNameId;
            string NpcName;
            if (TryParseNPC(RawFavorNpc, out ParsedArea, out NpcNameId, out NpcName, ErrorInfo))
            {
                FavorNpcArea = ParsedArea;
                FavorNpcId = NpcNameId;
                IsFavorNpcParsed = false;
                FavorNpcName = NpcName;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Quest FavorNpc");
        }

        private static void ParseFieldPrefaceText(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest PrefaceText", This.ParsePrefaceText);
        }

        private void ParsePrefaceText(string RawPrefaceText, ParseErrorInfo ErrorInfo)
        {
            PrefaceText = RawPrefaceText;
        }

        private static void ParseFieldSuccessText(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest SuccessText", This.ParseSuccessText);
        }

        private void ParseSuccessText(string RawSuccessText, ParseErrorInfo ErrorInfo)
        {
            SuccessText = RawSuccessText;
        }

        private static void ParseFieldMidwayText(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest MidwayText", This.ParseMidwayText);
        }

        private void ParseMidwayText(string RawMidwayText, ParseErrorInfo ErrorInfo)
        {
            MidwayText = RawMidwayText;
        }

        private static void ParseFieldPrerequisiteFavorLevel(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest PrerequisiteFavorLevel", This.ParsePrerequisiteFavorLevel);
        }

        private void ParsePrerequisiteFavorLevel(string RawPrerequisiteFavorLevel, ParseErrorInfo ErrorInfo)
        {
            Favor ParsedFavor;
            StringToEnumConversion<Favor>.TryParse(RawPrerequisiteFavorLevel, out ParsedFavor, ErrorInfo);
            PrerequisiteFavorLevel = ParsedFavor;
        }

        private static void ParseFieldRewardsFavor(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest RewardFavor", This.ParseRewardFavor);
        }

        private static void ParseFieldRewardFavor(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest RewardFavor", This.ParseRewardFavor);
        }

        private void ParseRewardFavor(long RawRewardFavor, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardFavor = (int)RawRewardFavor;
        }

        private static void ParseFieldRewardsRecipes(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "Quest RewardsRecipes", This.ParseRewardsRecipes);
        }

        private bool ParseRewardsRecipes(string RawRewardRecipe, ParseErrorInfo ErrorInfo)
        {
            if (this.RawRewardRecipe == null)
            {
                this.RawRewardRecipe = RawRewardRecipe;
                return true;
            }
            else
            {
                ErrorInfo.AddDuplicateString("Quest", "RecipeRewards");
                return false;
            }
        }

        private static void ParseFieldRewardsAbility(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest RewardsAbility", This.ParseRewardsAbility);
        }

        private void ParseRewardsAbility(string RawRewardsAbility, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardAbility = RawRewardsAbility;
            RewardAbility = null;
            IsRawRewardAbilityParsed = false;
        }

        private static void ParseFieldRequirements(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringObjectOrArray(Value, ErrorInfo, "Quest Requirements", This.ParseRequirements);
        }

        private void ParseRequirements(JsonObject RawRequirements, ParseErrorInfo ErrorInfo)
        {
            QuestRequirement ParsedQuestRequirement;
            JsonObjectParser<QuestRequirement>.InitAsSubitem("Requirements", RawRequirements, out ParsedQuestRequirement, ErrorInfo);

            QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
            if (ConvertedQuestRequirement != null)
                QuestRequirementList.Add(ConvertedQuestRequirement);
        }

        private static void ParseFieldRewards(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueArray(Value, ErrorInfo, "Quest Rewards", This.ParseRewards);
        }

        private void ParseRewards(JsonObject RawReward, ParseErrorInfo ErrorInfo)
        {
            if (RawReward.Has("T"))
            {
                JsonString AsJsonString;
                if ((AsJsonString = RawReward["T"] as JsonString) != null)
                {
                    string RewardType = AsJsonString.String;
                    if (RewardType == "SkillXp")
                    {
                        if (RawReward.Has("Skill") && RawReward.Has("Xp"))
                        {
                            if (RewardSkill == PowerSkill.Internal_None && !RawRewardSkillXp.HasValue)
                            {
                                JsonString SkillValue;
                                JsonInteger XpValue;
                                if (((SkillValue = RawReward["Skill"] as JsonString) != null) && ((XpValue = RawReward["Xp"] as JsonInteger) != null))
                                {
                                    PowerSkill ParsedSkill;
                                    if (StringToEnumConversion<PowerSkill>.TryParse(SkillValue.String, out ParsedSkill, ErrorInfo))
                                    {
                                        RewardSkill = ParsedSkill;
                                        RawRewardSkillXp = XpValue.Number;
                                    }
                                    else
                                        ErrorInfo.AddDuplicateString("Quest", "SkillRewards");
                                }
                                else
                                    ErrorInfo.AddDuplicateString("Quest", "SkillRewards");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "SkillRewards");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                    }

                    else if (RewardType == "Recipe")
                    {
                        if (RawReward.Has("Recipe"))
                        {
                            if (RewardRecipe == null)
                            {
                                JsonString RecipeValue;
                                if ((RecipeValue = RawReward["Recipe"] as JsonString) != null)
                                    RawRewardRecipe = RecipeValue.String;
                                else
                                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "RecipeRewards");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                    }

                    else if (RewardType == "CombatXp")
                    {
                        if (RawReward.Has("Xp"))
                        {
                            if (RawRewardCombatXP == null)
                            {
                                JsonInteger XpValue;
                                if ((XpValue = RawReward["Xp"] as JsonInteger) != null)
                                    RawRewardCombatXP = XpValue.Number;
                                else
                                    ErrorInfo.AddDuplicateString("Quest", "CombatXpRewards");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "CombatXpRewards");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                    }

                    else if (RewardType == "GuildXp")
                    {
                        if (RawReward.Has("Xp"))
                        {
                            if (!RawRewardGuildXp.HasValue)
                            {
                                JsonInteger XpValue;
                                if ((XpValue = RawReward["Xp"] as JsonInteger) != null)
                                    RawRewardGuildXp = XpValue.Number;
                                else
                                    ErrorInfo.AddDuplicateString("Quest", "GuildXpRewards");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "GuildXpRewards");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                    }

                    else if (RewardType == "GuildCredits")
                    {
                        if (RawReward.Has("Credits"))
                        {
                            if (!RawRewardGuildCredits.HasValue)
                            {
                                JsonInteger CreditsValue;
                                if ((CreditsValue = RawReward["Credits"] as JsonInteger) != null)
                                    RawRewardGuildCredits = CreditsValue.Number;
                                else
                                    ErrorInfo.AddDuplicateString("Quest", "GuildCreditsRewards");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "GuildCreditsRewards");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
        }

        private static void ParseFieldPreGiveItems(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueArray(Value, ErrorInfo, "Quest PreGiveItems", This.ParsePreGiveItems);
        }

        private void ParsePreGiveItems(JsonObject RawPreGiveItems, ParseErrorInfo ErrorInfo)
        {
            QuestRewardItem ParsedPreGiveItem;
            JsonObjectParser<QuestRewardItem>.InitAsSubitem("PreGiveItems", RawPreGiveItems, out ParsedPreGiveItem, ErrorInfo);
            if (ParsedPreGiveItem != null)
                PreGiveItemList.Add(ParsedPreGiveItem);
        }

        private static void ParseFieldTSysLevel(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest TSysLevel", This.ParseTSysLevel);
        }

        private void ParseTSysLevel(long RawTSysLevel, ParseErrorInfo ErrorInfo)
        {
            this.RawTSysLevel = (int)RawTSysLevel;
        }

        private static void ParseFieldRewardGold(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest RewardGold", This.ParseRewardGold);
        }

        private void ParseRewardGold(long RawRewardGold, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardGold = (int)RawRewardGold;
        }

        private static void ParseFieldRewardsNamedLootProfile(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest RewardsNamedLootProfile", This.ParseRewardsNamedLootProfile);
        }

        private void ParseRewardsNamedLootProfile(string RawRewardsNamedLootProfile, ParseErrorInfo ErrorInfo)
        {
            RewardsNamedLootProfile = RawRewardsNamedLootProfile;
        }

        private static void ParseFieldPreGiveRecipes(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "Quest PreGiveRecipes", This.ParsePreGiveRecipes);
        }

        private bool ParsePreGiveRecipes(string RawPreGiveRecipe, ParseErrorInfo ErrorInfo)
        {
            RawPreGiveRecipeList.Add(RawPreGiveRecipe);
            return true;
        }

        private static void ParseFieldKeywords(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "Quest Keywords", This.ParseKeywords);
        }

        private bool ParseKeywords(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            QuestKeyword ParsedKeyword;
            if (StringToEnumConversion<QuestKeyword>.TryParse(RawKeyword, out ParsedKeyword, ErrorInfo))
            {
                KeywordList.Add(ParsedKeyword);
                return true;
            }
            else
                return false;
        }

        private static void ParseFieldRewardsEffects(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "Quest RewardsEffects", This.ParseRewardsEffects);
        }

        private bool ParseRewardsEffects(string RawRewardEffect, ParseErrorInfo ErrorInfo)
        {
            if (this.RawRewardEffect == null)
            {
                if (RawRewardEffect.StartsWith("SetInteractionFlag("))
                {
                    int IndexEnd = RawRewardEffect.IndexOf(')');
                    if (IndexEnd >= 19)
                    {
                        string RawRewardInteractionFlag = RawRewardEffect.Substring(19, IndexEnd - 19);
                        if (!RawRewardInteractionFlags.Contains(RawRewardInteractionFlag))
                            RawRewardInteractionFlags.Add(RawRewardInteractionFlag);
                    }
                    else
                        this.RawRewardEffect = RawRewardEffect;

                    return true;
                }

                else if (RawRewardEffect.StartsWith("EnsureLoreBookKnown("))
                {
                    int IndexEnd = RawRewardEffect.IndexOf(')');
                    if (IndexEnd >= 20)
                    {
                        string RawRewardEnsureLoreBook = RawRewardEffect.Substring(20, IndexEnd - 20);
                        if (this.RawRewardEnsureLoreBook == null)
                        {
                            this.RawRewardEnsureLoreBook = RawRewardEnsureLoreBook;
                            return true;
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");

                    return false;
                }

                else
                {
                    this.RawRewardEffect = RawRewardEffect;
                    return true;
                }
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");
                return false;
            }
        }

        private static void ParseFieldIsAutoPreface(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "Quest IsAutoPreface", This.ParseIsAutoPreface);
        }

        private void ParseIsAutoPreface(bool RawIsAutoPreface, ParseErrorInfo ErrorInfo)
        {
            this.RawIsAutoPreface = RawIsAutoPreface;
        }

        private static void ParseFieldIsAutoWrapUp(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "Quest IsAutoWrapUp", This.ParseIsAutoWrapUp);
        }

        private void ParseIsAutoWrapUp(bool RawIsAutoWrapUp, ParseErrorInfo ErrorInfo)
        {
            this.RawIsAutoWrapUp = RawIsAutoWrapUp;
        }

        private static void ParseFieldGroupingName(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest GroupingName", This.ParseGroupingName);
        }

        private void ParseGroupingName(string RawGroupingName, ParseErrorInfo ErrorInfo)
        {
            QuestGroupingName ParsedGroupingName;
            StringToEnumConversion<QuestGroupingName>.TryParse(RawGroupingName, out ParsedGroupingName, ErrorInfo);
            GroupingName = ParsedGroupingName;
        }

        private static void ParseFieldIsGuildQuest(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "Quest IsGuildQuest", This.ParseIsGuildQuest);
        }

        private void ParseIsGuildQuest(bool RawIsGuildQuest, ParseErrorInfo ErrorInfo)
        {
            this.RawIsGuildQuest = RawIsGuildQuest;
        }

        private static void ParseFieldNumExpectedParticipants(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest NumExpectedParticipants", This.ParseNumExpectedParticipants);
        }

        private void ParseNumExpectedParticipants(long RawNumExpectedParticipants, ParseErrorInfo ErrorInfo)
        {
            this.RawNumExpectedParticipants = (int)RawNumExpectedParticipants;
        }

        private static void ParseFieldLevel(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Quest Level", This.ParseLevel);
        }

        private void ParseLevel(long RawLevel, ParseErrorInfo ErrorInfo)
        {
            this.RawLevel = (int)RawLevel;
        }

        private static void ParseFieldWorkOrderSkill(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest WorkOrderSkill", This.ParseWorkOrderSkill);
        }

        private void ParseWorkOrderSkill(string RawWorkOrderSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawWorkOrderSkill, out ParsedSkill, ErrorInfo);
            WorkOrderSkill = ParsedSkill;
        }

        private static void ParseFieldDisplayedLocation(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Quest DisplayedLocation", This.ParseDisplayedLocation);
        }

        private void ParseDisplayedLocation(string RawDisplayedLocation, ParseErrorInfo ErrorInfo)
        {
            MapAreaName ParsedMapAreaName;
            StringToEnumConversion<MapAreaName>.TryParse(RawDisplayedLocation, TextMaps.MapAreaNameStringMap, out ParsedMapAreaName, ErrorInfo);
            DisplayedLocation = ParsedMapAreaName;
        }

        private static void ParseFieldFollowUpQuests(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "Quest FieldFollowUpQuests", This.ParseFollowUpQuests);
        }

        private bool ParseFollowUpQuests(string RawFollowUpQuest, ParseErrorInfo ErrorInfo)
        {
            RawFollowUpQuestList.Add(RawFollowUpQuest);
            return true;
        }

        public static bool TryParseNPC(string s, out MapAreaName ParsedArea, out string NpcId, out string NpcName, ParseErrorInfo ErrorInfo)
        {
            ParsedArea = MapAreaName.Internal_None;
            NpcId = null;
            NpcName = null;

            if (s.Length == 0)
                return false;

            string[] AreaNpc = s.Split('/');
            if (AreaNpc.Length == 2)
            {
                string RawMapName = AreaNpc[0];
                if (RawMapName.StartsWith("Area"))
                    StringToEnumConversion<MapAreaName>.TryParse(RawMapName.Substring(4), out ParsedArea, ErrorInfo);
                else
                    return false;

                string Npc = AreaNpc[1];
                if (Npc.ToUpper().StartsWith("NPC_"))
                {
                    NpcId = Npc;
                    NpcName = Npc.Substring(4);
                    return true;
                }
                else
                {
                    SpecialNpc ParsedSpecialNpc;
                    if (StringToEnumConversion<SpecialNpc>.TryParse(Npc, out ParsedSpecialNpc, ErrorInfo))
                    {
                        NpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
                        return true;
                    }
                    else
                        return false;
                }
            }
            else
                return false;
        }

        public static bool TryParseNPC(string Npc, out string NpcId, out string NpcName, ParseErrorInfo ErrorInfo)
        {
            NpcId = null;
            NpcName = null;

            if (Npc.Length == 0)
                return false;

            if (Npc.ToUpper().StartsWith("NPC_"))
            {
                NpcId = Npc;
                NpcName = Npc.Substring(4);
                return true;
            }
            else
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(Npc, out ParsedSpecialNpc, ErrorInfo))
                {
                    NpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
                    return true;
                }
                else
                    return false;
            }
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);
                AddWithFieldSeparator(ref Result, Description);
                if (RawIsCancellable.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Cancellable");
                foreach (QuestObjective Item in QuestObjectiveList)
                    AddWithFieldSeparator(ref Result, Item.Description);
                foreach (QuestRewardXp Reward in RewardsXPList)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[Reward.Skill]);
                foreach (QuestRewardItem Reward in QuestRewardsItemList)
                    AddWithFieldSeparator(ref Result, Reward.QuestItem.Name);
                if (FavorNpc != null)
                {
                    AddWithFieldSeparator(ref Result, FavorNpc.Name);
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[FavorNpcArea]);
                }
                else if (FavorNpcName != null)
                {
                    AddWithFieldSeparator(ref Result, FavorNpcName);
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[FavorNpcArea]);
                }
                AddWithFieldSeparator(ref Result, PrefaceText);
                AddWithFieldSeparator(ref Result, SuccessText);
                AddWithFieldSeparator(ref Result, MidwayText);
                if (PrerequisiteFavorLevel != Favor.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.FavorTextMap[PrerequisiteFavorLevel]);
                if (RewardAbility != null)
                    AddWithFieldSeparator(ref Result, RewardAbility.Name);
                if (ConnectedRewardSkill != null)
                    AddWithFieldSeparator(ref Result, ConnectedRewardSkill.Name);
                if (RewardRecipe != null)
                    AddWithFieldSeparator(ref Result, RewardRecipe.Name);
                foreach (QuestRewardItem Item in PreGiveItemList)
                    AddWithFieldSeparator(ref Result, Item.QuestItem.Name);
                foreach (Recipe Item in PreGiveRecipeList)
                    AddWithFieldSeparator(ref Result, Item.Name);
                foreach (QuestKeyword Keyword in KeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.QuestKeywordTextMap[Keyword]);
                if (RewardEffect != null)
                    AddWithFieldSeparator(ref Result, RewardEffect.Name);
                if (RawIsAutoPreface.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Auto Preface");
                if (RawIsAutoWrapUp.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Auto WrapUp");
                if (GroupingName != QuestGroupingName.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.QuestGroupingNameTextMap[GroupingName]);
                if (RawIsGuildQuest.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Guild Quest");
                if (WorkOrderSkill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[WorkOrderSkill]);
                if (DisplayedLocation != MapAreaName.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[DisplayedLocation]);
                foreach (Quest Item in FollowUpQuestList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IGenericJsonObject> QuestTable = AllTables[typeof(Quest)];
            Dictionary<string, IGenericJsonObject> EffectTable = AllTables[typeof(Effect)];
            Dictionary<string, IGenericJsonObject> GameNpcTable = AllTables[typeof(GameNpc)];
            Dictionary<string, IGenericJsonObject> LoreBookTable = AllTables[typeof(LoreBook)];

            foreach (QuestObjective Item in QuestObjectiveList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            foreach (QuestRewardXp Item in RewardsXPList)
                if (!Item.IsSkillParsed)
                {
                    bool IsSkillParsed = false;
                    Item.ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Item.Skill, Item.ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);
                    Item.IsSkillParsed = IsSkillParsed;
                }

            foreach (QuestRewardItem Item in QuestRewardsItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            foreach (QuestRewardItem Item in PreGiveItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            RewardAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawRewardAbility, RewardAbility, ref IsRawRewardAbilityParsed, ref IsConnected, this);
            RewardRecipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRewardRecipe, RewardRecipe, ref IsRawRewardRecipeParsed, ref IsConnected, this);
            RewardEffect = Effect.ConnectSingleProperty(ErrorInfo, EffectTable, RawRewardEffect, RewardEffect, ref IsRawRewardEffectParsed, ref IsConnected, this);
            ConnectedRewardSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RewardSkill, ConnectedRewardSkill, ref IsConnectedRewardSkillParsed, ref IsConnected, this);
            ConnectedWorkOrderSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, WorkOrderSkill, ConnectedWorkOrderSkill, ref IsConnectedWorkOrderSkillParsed, ref IsConnected, this);

            List<string> ToRemove = new List<string>();
            foreach (string RawPreGiveRecipe in RawPreGiveRecipeList)
            {
                Recipe PreGiveRecipe = null;
                bool IsRawPreGiveRecipeParsed = false;
                PreGiveRecipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawPreGiveRecipe, PreGiveRecipe, ref IsRawPreGiveRecipeParsed, ref IsConnected, this);
                if (PreGiveRecipe != null)
                {
                    ToRemove.Add(RawPreGiveRecipe);
                    PreGiveRecipeList.Add(PreGiveRecipe);
                }
            }

            foreach (string RawPreGiveRecipe in ToRemove)
                RawPreGiveRecipeList.Remove(RawPreGiveRecipe);

            foreach (string RawFollowUpQuest in RawFollowUpQuestList)
            {
                Quest FollowUpQuest = null;
                bool IsParsed = false;
                FollowUpQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawFollowUpQuest, FollowUpQuest, ref IsParsed, ref IsConnected, this);

                if (FollowUpQuest != null)
                    FollowUpQuestList.Add(FollowUpQuest);
                else
                    FollowUpQuest = null;
            }

            FavorNpc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, FavorNpcId, FavorNpc, ref IsFavorNpcParsed, ref IsConnected, this);
            if (FavorNpcId != null && FavorNpc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(FavorNpcId, out ParsedSpecialNpc, ErrorInfo))
                    FavorNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            foreach (QuestRequirement Requirement in QuestRequirementList)
                Requirement.Connect(ErrorInfo, this, AllTables);

            foreach (QuestRequirement Requirement in QuestRequirementToSustainList)
                Requirement.Connect(ErrorInfo, this, AllTables);

            bool InteractionFlagRemoved = true;
            while (InteractionFlagRemoved)
            {
                InteractionFlagRemoved = false;

                foreach (string RawRewardInteractionFlag in RawRewardInteractionFlags)
                {
                    foreach (QuestObjective QuestObjective in QuestObjectiveList)
                    {
                        QuestObjectiveInteractionFlag AsQuestObjectiveInteractionFlag;
                        if ((AsQuestObjectiveInteractionFlag = QuestObjective as QuestObjectiveInteractionFlag) != null)
                        {
                            if (AsQuestObjectiveInteractionFlag.InteractionFlag == RawRewardInteractionFlag)
                            {
                                RawRewardInteractionFlags.Remove(RawRewardInteractionFlag);
                                InteractionFlagRemoved = true;
                                break;
                            }
                        }
                    }
                    if (InteractionFlagRemoved)
                        break;
                }
            }

            if (RawRewardEnsureLoreBook != null)
                RewardLoreBook = LoreBook.ConnectByInternalName(ErrorInfo, LoreBookTable, RawRewardEnsureLoreBook, RewardLoreBook, ref IsRawLoreBookParsed, ref IsConnected, this);

            return IsConnected;
        }

        public static Quest ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> QuestTable, string RawQuestName, Quest ParsedQuest, ref bool IsRawQuestParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawQuestParsed)
                return ParsedQuest;

            IsRawQuestParsed = true;

            if (RawQuestName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in QuestTable)
            {
                Quest QuestValue = Entry.Value as Quest;
                if (QuestValue.InternalName == RawQuestName)
                {
                    IsConnected = true;
                    QuestValue.AddLinkBack(LinkBack);
                    return QuestValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawQuestName);

            return null;
        }

        public static Quest ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> QuestTable, int QuestId, Quest ParsedQuest, ref bool IsRawQuestParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawQuestParsed)
                return ParsedQuest;

            IsRawQuestParsed = true;

            string RawQuestId = "quest_" + QuestId;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in QuestTable)
            {
                Quest QuestValue = Entry.Value as Quest;
                if (QuestValue.Key == RawQuestId)
                {
                    IsConnected = true;
                    QuestValue.AddLinkBack(LinkBack);
                    return QuestValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawQuestId);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Quest"; } }
        #endregion
    }
}
