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
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
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
            string RawInternalName;
            if ((RawInternalName = Value as string) != null)
                This.ParseInternalName(RawInternalName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest InternalName");
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;
        }

        private static void ParseFieldName(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
            ErrorInfo.AddIconId(SearchResultIconId);
        }

        private static void ParseFieldDescription(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDescription;
            if ((RawDescription = Value as string) != null)
                This.ParseDescription(RawDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Description");
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
        }

        private static void ParseFieldVersion(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseVersion((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Version");
        }

        private void ParseVersion(int RawVersion, ParseErrorInfo ErrorInfo)
        {
            this.RawVersion = RawVersion;
        }

        private static void ParseFieldRequirementsToSustain(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> AsDictionary;
            ArrayList RawRequirementsToSustain;

            if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseRequirementToSustainAsDictionary(AsDictionary, ErrorInfo);

            else if ((RawRequirementsToSustain = Value as ArrayList) != null)
                This.ParseRequirementsToSustain(RawRequirementsToSustain, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("Quest RequirementsToSustain");
        }

        private void ParseRequirementsToSustain(ArrayList RawRequirementsToSustain, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawRequirement in RawRequirementsToSustain)
            {
                Dictionary<string, object> AsDictionary;
                if ((AsDictionary = RawRequirement as Dictionary<string, object>) != null)
                    ParseRequirementToSustainAsDictionary(AsDictionary, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest RequirementsToSustain");
            }
        }

        private void ParseRequirementToSustainAsDictionary(Dictionary<string, object> RawRequirements, ParseErrorInfo ErrorInfo)
        {
            QuestRequirement ParsedQuestRequirement;
            JsonObjectParser<QuestRequirement>.InitAsSubitem("Requirements", RawRequirements, out ParsedQuestRequirement, ErrorInfo);

            QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
            if (ConvertedQuestRequirement != null)
                QuestRequirementToSustainList.Add(ConvertedQuestRequirement);
        }

        private static void ParseFieldReuseTimeMinutes(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseReuseTimeMinutes((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest ReuseTimeMinutes");
        }

        private void ParseReuseTimeMinutes(int RawReuseTimeMinutes, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromMinutes(RawReuseTimeMinutes);
            else
                RawReuseTime += TimeSpan.FromMinutes(RawReuseTimeMinutes);
        }

        private static void ParseFieldIsCancellable(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsCancellable((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest IsCancellable");
        }

        private void ParseIsCancellable(bool RawIsCancellable, ParseErrorInfo ErrorInfo)
        {
            this.RawIsCancellable = RawIsCancellable;
        }

        private static void ParseFieldObjectives(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawObjectives;
            if ((RawObjectives = Value as ArrayList) != null)
                This.ParseObjectives(RawObjectives, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Objectives");
        }

        private void ParseObjectives(ArrayList RawObjectives, ParseErrorInfo ErrorInfo)
        {
            List<QuestObjective> ParsedQuestObjectiveList;
            JsonObjectParser<QuestObjective>.InitAsSublist(RawObjectives, out ParsedQuestObjectiveList, ErrorInfo);

            foreach (QuestObjective Objective in ParsedQuestObjectiveList)
            {
                QuestObjective ConvertedQuestObjective = Objective.ToSpecificQuestObjective(ErrorInfo);
                QuestObjectiveList.Add(ConvertedQuestObjective);
            }
        }

        private static void ParseFieldRewardsXP(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawRewardsXP;
            if ((RawRewardsXP = Value as Dictionary<string, object>) != null)
                This.ParseRewardsXP(RawRewardsXP, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsXP");
        }

        private void ParseRewardsXP(Dictionary<string, object> RawRewardsXP, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, object> RawRewardXP in RawRewardsXP)
            {
                PowerSkill ParsedSkill;
                if (StringToEnumConversion<PowerSkill>.TryParse(RawRewardXP.Key, out ParsedSkill, ErrorInfo))
                {
                    if (RawRewardXP.Value is int)
                    {
                        QuestRewardXp NewReward = new QuestRewardXp();
                        NewReward.Skill = ParsedSkill;
                        NewReward.Xp = (int)RawRewardXP.Value;
                        RewardsXPList.Add(NewReward);
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest RewardsXP");
                }
            }
        }

        private static void ParseFieldRewardsItems(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawRewardsItems;
            if ((RawRewardsItems = Value as ArrayList) != null)
                This.ParseRewardsItems(RawRewardsItems, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsItems");
        }

        private void ParseRewardsItems(ArrayList RawRewardsItems, ParseErrorInfo ErrorInfo)
        {
            List<QuestRewardItem> ParsedRewardItemList;
            JsonObjectParser<QuestRewardItem>.InitAsSublist(RawRewardsItems, out ParsedRewardItemList, ErrorInfo);
            QuestRewardsItemList.AddRange(ParsedRewardItemList);
        }

        private static void ParseFieldReuseTimeDays(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseReuseTimeDays((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest ReuseTimeDays");
        }

        private void ParseReuseTimeDays(int RawReuseTimeDays, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromDays(RawReuseTimeDays);
            else
                RawReuseTime += TimeSpan.FromDays(RawReuseTimeDays);
        }

        private static void ParseFieldReuseTimeHours(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseReuseTimeHours((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest ReuseTimeHours");
        }

        private void ParseReuseTimeHours(int RawReuseTimeHours, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromHours(RawReuseTimeHours);
            else
                RawReuseTime += TimeSpan.FromHours(RawReuseTimeHours);
        }

        private static void ParseFieldRewardCombatXP(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardCombatXP((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardCombatXP");
        }

        private void ParseRewardCombatXP(int RawRewardCombatXP, ParseErrorInfo ErrorInfo)
        {
            if (this.RawRewardCombatXP == null)
                this.RawRewardCombatXP = RawRewardCombatXP;
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardCombatXP");
        }

        private static void ParseFieldFavorNpc(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawFavorNpc;
            if ((RawFavorNpc = Value as string) != null)
                This.ParseFavorNpc(RawFavorNpc, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest FavorNpc");
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
            string RawPrefaceText;
            if ((RawPrefaceText = Value as string) != null)
                This.ParsePrefaceText(RawPrefaceText, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest PrefaceText");
        }

        private void ParsePrefaceText(string RawPrefaceText, ParseErrorInfo ErrorInfo)
        {
            PrefaceText = RawPrefaceText;
        }

        private static void ParseFieldSuccessText(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSuccessText;
            if ((RawSuccessText = Value as string) != null)
                This.ParseSuccessText(RawSuccessText, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest SuccessText");
        }

        private void ParseSuccessText(string RawSuccessText, ParseErrorInfo ErrorInfo)
        {
            SuccessText = RawSuccessText;
        }

        private static void ParseFieldMidwayText(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMidwayText;
            if ((RawMidwayText = Value as string) != null)
                This.ParseMidwayText(RawMidwayText, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest MidwayText");
        }

        private void ParseMidwayText(string RawMidwayText, ParseErrorInfo ErrorInfo)
        {
            MidwayText = RawMidwayText;
        }

        private static void ParseFieldPrerequisiteFavorLevel(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawPrerequisiteFavorLevel;
            if ((RawPrerequisiteFavorLevel = Value as string) != null)
                This.ParsePrerequisiteFavorLevel(RawPrerequisiteFavorLevel, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest PrerequisiteFavorLevel");
        }

        private void ParsePrerequisiteFavorLevel(string RawPrerequisiteFavorLevel, ParseErrorInfo ErrorInfo)
        {
            Favor ParsedFavor;
            StringToEnumConversion<Favor>.TryParse(RawPrerequisiteFavorLevel, out ParsedFavor, ErrorInfo);
            PrerequisiteFavorLevel = ParsedFavor;
        }

        private static void ParseFieldRewardsFavor(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardFavor((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsFavor");
        }

        private static void ParseFieldRewardFavor(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardFavor((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardFavor");
        }

        private void ParseRewardFavor(int RawRewardFavor, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardFavor = RawRewardFavor;
        }

        private static void ParseFieldRewardsRecipes(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawRewardsRecipes;
            if ((RawRewardsRecipes = Value as ArrayList) != null)
                This.ParseRewardsRecipes(RawRewardsRecipes, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsRecipes");
        }

        private void ParseRewardsRecipes(ArrayList RawRewardsRecipes, ParseErrorInfo ErrorInfo)
        {
            if (RawRewardsRecipes.Count <= 1)
            {
                foreach (object RawRewardRecipe in RawRewardsRecipes)
                {
                    string AsString;
                    if ((AsString = RawRewardRecipe as string) != null)
                    {
                        if (RewardRecipe == null)
                            this.RawRewardRecipe = AsString;
                        else
                            ErrorInfo.AddDuplicateString("Quest", "RecipeRewards");
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest RewardsRecipes");
                    break;
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsRecipes");
        }

        private static void ParseFieldRewardsAbility(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRewardsAbility;
            if ((RawRewardsAbility = Value as string) != null)
                This.ParseRewardsAbility(RawRewardsAbility, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsAbility");
        }

        private void ParseRewardsAbility(string RawRewardsAbility, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardAbility = RawRewardsAbility;
            RewardAbility = null;
            IsRawRewardAbilityParsed = false;
        }

        private static void ParseFieldRequirements(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawRequirements;
            Dictionary<string, object> AsDictionary;

            if ((RawRequirements = Value as ArrayList) != null)
                This.ParseRequirements(RawRequirements, ErrorInfo);
            else if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseRequirementAsDictionary(AsDictionary, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
        }

        private void ParseRequirements(ArrayList RawRequirements, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawRequirement in RawRequirements)
            {
                Dictionary<string, object> AsDictionary;
                ArrayList AsArrayList;

                if ((AsDictionary = RawRequirement as Dictionary<string, object>) != null)
                    ParseRequirementAsDictionary(AsDictionary, ErrorInfo);

                else if ((AsArrayList = RawRequirement as ArrayList) != null)
                    ParseRequirements(AsArrayList, ErrorInfo);

                else
                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
            }
        }

        private void ParseRequirementAsDictionary(Dictionary<string, object> RawRequirements, ParseErrorInfo ErrorInfo)
        {
            QuestRequirement ParsedQuestRequirement;
            JsonObjectParser<QuestRequirement>.InitAsSubitem("Requirements", RawRequirements, out ParsedQuestRequirement, ErrorInfo);

            QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
            if (ConvertedQuestRequirement != null)
                QuestRequirementList.Add(ConvertedQuestRequirement);
        }

        private static void ParseFieldRewards(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawRewards;
            if ((RawRewards = Value as ArrayList) != null)
                This.ParseRewards(RawRewards, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
        }

        private void ParseRewards(ArrayList RawRewards, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawReward in RawRewards)
            {
                Dictionary<string, object> AsDictionary;
                if ((AsDictionary = RawReward as Dictionary<string, object>) != null)
                    ParseParseRewardAsDictionary(AsDictionary, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
            }
        }

        private void ParseParseRewardAsDictionary(Dictionary<string, object> RawReward, ParseErrorInfo ErrorInfo)
        {
            if (RawReward.ContainsKey("T"))
            {
                string RewardType;
                if ((RewardType = RawReward["T"] as string) != null)
                {
                    if (RewardType == "SkillXp")
                    {
                        if (RawReward.ContainsKey("Skill") && RawReward.ContainsKey("Xp"))
                        {
                            if (RewardSkill == PowerSkill.Internal_None && !RawRewardSkillXp.HasValue)
                            {
                                PowerSkill ParsedSkill;
                                StringToEnumConversion<PowerSkill>.TryParse(RawReward["Skill"] as string, out ParsedSkill, ErrorInfo);
                                RewardSkill = ParsedSkill;

                                if (RewardSkill != PowerSkill.Internal_None)
                                {
                                    if (RawReward["Xp"] is int)
                                        RawRewardSkillXp = (int)RawReward["Xp"];
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
                        if (RawReward.ContainsKey("Recipe"))
                        {
                            if (RewardRecipe == null)
                            {
                                RawRewardRecipe = RawReward["Recipe"] as string;
                                if (RawRewardRecipe == null)
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
                        if (RawReward.ContainsKey("Xp"))
                        {
                            if (RawRewardCombatXP == null)
                                if (RawReward["Xp"] is int)
                                    RawRewardCombatXP = (int)RawReward["Xp"];
                                else
                                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                            else
                                ErrorInfo.AddDuplicateString("Quest", "CombatXpRewards");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                    }

                    else if (RewardType == "GuildXp")
                    {
                        if (RawReward.ContainsKey("Xp"))
                        {
                            if (!RawRewardGuildXp.HasValue)
                            {
                                if (RawReward["Xp"] is int)
                                    RawRewardGuildXp = (int)RawReward["Xp"];
                                else
                                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "GuildXpRewards");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                    }

                    else if (RewardType == "GuildCredits")
                    {
                        if (RawReward.ContainsKey("Credits"))
                        {
                            if (!RawRewardGuildCredits.HasValue)
                            {
                                if (RawReward["Credits"] is int)
                                    RawRewardGuildCredits = (int)RawReward["Credits"];
                                else
                                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
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
            ArrayList RawPreGiveItems;
            if ((RawPreGiveItems = Value as ArrayList) != null)
                This.ParsePreGiveItems(RawPreGiveItems, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest PreGiveItems");
        }

        private void ParsePreGiveItems(ArrayList RawPreGiveItems, ParseErrorInfo ErrorInfo)
        {
            List<QuestRewardItem> ParsedPreGiveItemList;
            JsonObjectParser<QuestRewardItem>.InitAsSublist(RawPreGiveItems, out ParsedPreGiveItemList, ErrorInfo);
            PreGiveItemList.AddRange(ParsedPreGiveItemList);
        }

        private static void ParseFieldTSysLevel(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseTSysLevel((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest TSysLevel");
        }

        private void ParseTSysLevel(int RawTSysLevel, ParseErrorInfo ErrorInfo)
        {
            this.RawTSysLevel = RawTSysLevel;
        }

        private static void ParseFieldRewardGold(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardGold((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardGold");
        }

        private void ParseRewardGold(int RawRewardGold, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardGold = RawRewardGold;
        }

        private static void ParseFieldRewardsNamedLootProfile(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRewardsNamedLootProfile;
            if ((RawRewardsNamedLootProfile = Value as string) != null)
                This.ParseRewardsNamedLootProfile(RawRewardsNamedLootProfile, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsNamedLootProfile");
        }

        private void ParseRewardsNamedLootProfile(string RawRewardsNamedLootProfile, ParseErrorInfo ErrorInfo)
        {
            RewardsNamedLootProfile = RawRewardsNamedLootProfile;
        }

        private static void ParseFieldPreGiveRecipes(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawPreGiveRecipes;
            if ((RawPreGiveRecipes = Value as ArrayList) != null)
                This.ParsePreGiveRecipes(RawPreGiveRecipes, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest PreGiveRecipes");
        }

        private void ParsePreGiveRecipes(ArrayList RawPreGiveRecipes, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawRewardRecipe in RawPreGiveRecipes)
            {
                string AsString;
                if ((AsString = RawRewardRecipe as string) != null)
                    RawPreGiveRecipeList.Add(AsString);
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest PreGiveRecipes");
            }
        }

        private static void ParseFieldKeywords(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawKeywords;
            if ((RawKeywords = Value as ArrayList) != null)
                This.ParseKeywords(RawKeywords, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Keywords");
        }

        private void ParseKeywords(ArrayList RawKeywords, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawRewardRecipe in RawKeywords)
            {
                string AsString;
                if ((AsString = RawRewardRecipe as string) != null)
                {
                    QuestKeyword ParsedKeyword;
                    if (StringToEnumConversion<QuestKeyword>.TryParse(AsString, out ParsedKeyword, ErrorInfo))
                        KeywordList.Add(ParsedKeyword);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest Keywords");
            }
        }

        private static void ParseFieldRewardsEffects(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawRewardsEffects;
            if ((RawRewardsEffects = Value as ArrayList) != null)
                This.ParseRewardsEffects(RawRewardsEffects, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");
        }

        private void ParseRewardsEffects(ArrayList RawRewardsEffects, ParseErrorInfo ErrorInfo)
        {
            if (RawRewardsEffects.Count <= 1)
            {
                foreach (object RawRewardRecipe in RawRewardsEffects)
                {
                    string AsString;
                    if ((AsString = RawRewardRecipe as string) != null)
                    {
                        if (AsString.StartsWith("SetInteractionFlag("))
                        {
                            int IndexEnd = AsString.IndexOf(')');
                            if (IndexEnd >= 19)
                            {
                                string RawRewardInteractionFlag = AsString.Substring(19, IndexEnd - 19);
                                if (!RawRewardInteractionFlags.Contains(RawRewardInteractionFlag))
                                    RawRewardInteractionFlags.Add(RawRewardInteractionFlag);
                            }
                            else
                                RawRewardEffect = AsString;
                        }
                        else
                            RawRewardEffect = AsString;
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");
        }

        private static void ParseFieldIsAutoPreface(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsAutoPreface((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest IsAutoPreface");
        }

        private void ParseIsAutoPreface(bool RawIsAutoPreface, ParseErrorInfo ErrorInfo)
        {
            this.RawIsAutoPreface = RawIsAutoPreface;
        }

        private static void ParseFieldIsAutoWrapUp(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsAutoWrapUp((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest IsAutoWrapUp");
        }

        private void ParseIsAutoWrapUp(bool RawIsAutoWrapUp, ParseErrorInfo ErrorInfo)
        {
            this.RawIsAutoWrapUp = RawIsAutoWrapUp;
        }

        private static void ParseFieldGroupingName(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawGroupingName;
            if ((RawGroupingName = Value as string) != null)
                This.ParseGroupingName(RawGroupingName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest GroupingName");
        }

        private void ParseGroupingName(string RawGroupingName, ParseErrorInfo ErrorInfo)
        {
            QuestGroupingName ParsedGroupingName;
            StringToEnumConversion<QuestGroupingName>.TryParse(RawGroupingName, out ParsedGroupingName, ErrorInfo);
            GroupingName = ParsedGroupingName;
        }

        private static void ParseFieldIsGuildQuest(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsGuildQuest((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest IsGuildQuest");
        }

        private void ParseIsGuildQuest(bool RawIsGuildQuest, ParseErrorInfo ErrorInfo)
        {
            this.RawIsGuildQuest = RawIsGuildQuest;
        }

        private static void ParseFieldNumExpectedParticipants(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNumExpectedParticipants((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest NumExpectedParticipants");
        }

        private void ParseNumExpectedParticipants(int RawNumExpectedParticipants, ParseErrorInfo ErrorInfo)
        {
            this.RawNumExpectedParticipants = RawNumExpectedParticipants;
        }

        private static void ParseFieldLevel(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseLevel((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Level");
        }

        private void ParseLevel(int RawLevel, ParseErrorInfo ErrorInfo)
        {
            this.RawLevel = RawLevel;
        }

        private static void ParseFieldWorkOrderSkill(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawWorkOrderSkill;
            if ((RawWorkOrderSkill = Value as string) != null)
                This.ParseWorkOrderSkill(RawWorkOrderSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest WorkOrderSkill");
        }

        private void ParseWorkOrderSkill(string RawWorkOrderSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawWorkOrderSkill, out ParsedSkill, ErrorInfo);
            WorkOrderSkill = ParsedSkill;
        }

        private static void ParseFieldDisplayedLocation(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDisplayedLocation;
            if ((RawDisplayedLocation = Value as string) != null)
                This.ParseDisplayedLocation(RawDisplayedLocation, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest DisplayedLocation");
        }

        private void ParseDisplayedLocation(string RawDisplayedLocation, ParseErrorInfo ErrorInfo)
        {
            MapAreaName ParsedMapAreaName;
            StringToEnumConversion<MapAreaName>.TryParse(RawDisplayedLocation, TextMaps.MapAreaNameStringMap, out ParsedMapAreaName, ErrorInfo);
            DisplayedLocation = ParsedMapAreaName;
        }

        private static void ParseFieldFollowUpQuests(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawFollowUpQuests;
            if ((RawFollowUpQuests = Value as ArrayList) != null)
                This.ParseFollowUpQuests(RawFollowUpQuests, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");
        }

        private void ParseFollowUpQuests(ArrayList RawFollowUpQuests, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawFollowUpQuest in RawFollowUpQuests)
            {
                string AsString;
                if ((AsString = RawFollowUpQuest as string) != null)
                    RawFollowUpQuestList.Add(AsString);
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest RewardsEffects");
            }
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
