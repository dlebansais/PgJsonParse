using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Quest : GenericJsonObject<Quest>
    {
        #region Constants
        public const int SearchResultIconId = 2118;

        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "InternalName", ParseFieldInternalName },
            { "Name", ParseFieldName },
            { "Description", ParseFieldDescription },
            { "Version", ParseFieldVersion },
            { "ReuseTime_Minutes", ParseFieldReuseTimeMinutes },
            { "IsCancellable", ParseFieldIsCancellable },
            { "Objectives", ParseFieldObjectives },
            { "Rewards_XP", ParseFieldRewardsXP },
            { "Rewards_Items", ParseFieldRewardsItems },
            { "ReuseTime_Days", ParseFieldReuseTimeDays },
            { "PrerequisiteQuest", ParseFieldPrerequisiteQuest },
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
        };
        #endregion

        #region Properties
        public string InternalName { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        private int? RawVersion;
        public int ReuseTimeMinutes { get { return RawReuseTimeMinutes.HasValue ? RawReuseTimeMinutes.Value : 0; } }
        private int? RawReuseTimeMinutes;
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        private bool? RawIsCancellable;
        public List<QuestObjective> QuestObjectiveList { get; set; }
        public List<QuestRewardXp> RewardsXPList { get; private set; }
        public List<QuestRewardItem> QuestRewardsItemList { get; private set; }
        public int ReuseTimeDays { get { return RawReuseTimeDays.HasValue ? RawReuseTimeDays.Value : 0; } }
        private int? RawReuseTimeDays;
        public Quest PrerequisiteQuest { get; private set; }
        private string RawPrerequisiteQuest;
        private bool IsRawPrerequisiteParsed;
        public int ReuseTimeHours { get { return RawReuseTimeHours.HasValue ? RawReuseTimeHours.Value : 0; } }
        private int? RawReuseTimeHours;
        public int RewardCombatXP { get { return RawRewardCombatXP.HasValue ? RawRewardCombatXP.Value : 0; } }
        private int? RawRewardCombatXP;
        public string FavorNpc { get; private set; }
        public MapAreaName FavorNpcArea { get; private set; }
        public string PrefaceText { get; private set; }
        public string SuccessText { get; private set; }
        public string MidwayText { get; private set; }
        public Favor PrerequisiteFavorLevel { get; private set; }
        public string RewardsFavor { get; private set; }
        public List<string> RewardsRecipeList { get; private set; }
        public string RewardsAbility { get; private set; }
        public string RequirementFavorNpc { get; private set; }
        public Favor RequirementFavorLevel { get; private set; }
        public string RequirementQuest { get; private set; }
        public string RequirementGuildQuest { get; private set; }
        public PowerSkill RequirementSkill { get; private set; }
        public int RequirementSkillLevel { get; private set; }
        public int RewardFavor { get { return RawRewardFavor.HasValue ? RawRewardFavor.Value : 0; } }
        private int? RawRewardFavor;
        public ArrayList Rewards { get; private set; }
        public PowerSkill RewardSkill { get; private set; }
        public int RewardSkillXp { get; private set; }
        public Recipe RewardRecipe { get; private set; }
        private string RawRewardRecipe;
        private bool IsRawRewardRecipeParsed;
        public int RewardCombatXp { get; private set; }
        public int RewardGuildXp { get; private set; }
        public int RewardGuildCredits { get; private set; }
        public List<QuestRewardItem> PreGiveItemList { get; private set; }
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        private int? RawTSysLevel;
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        private int? RawRewardGold;
        public string RewardsNamedLootProfile { get; private set; }
        public List<string> PreGiveRecipeList { get; private set; }
        public List<QuestKeyword> KeywordList { get; private set; }
        public Effect RewardEffect { get; private set; }
        private string RawRewardEffect;
        private bool IsRawRewardEffectParsed;
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        private bool? RawIsAutoPreface;
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        private bool? RawIsAutoWrapUp;
        public string GroupingName { get; private set; }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        private bool? RawIsGuildQuest;
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        private int? RawNumExpectedParticipants;
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        private int? RawLevel;
        public PowerSkill WorkOrderSkill { get; private set; }

        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Client Interface
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

        private static void ParseFieldReuseTimeMinutes(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseReuseTimeMinutes((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest ReuseTimeMinutes");
        }

        private void ParseReuseTimeMinutes(int RawReuseTimeMinutes, ParseErrorInfo ErrorInfo)
        {
            this.RawReuseTimeMinutes = RawReuseTimeMinutes;
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
            QuestObjectiveList = ParsedQuestObjectiveList;
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
            QuestRewardsItemList = ParsedRewardItemList;
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
            this.RawReuseTimeDays = RawReuseTimeDays;
        }

        private static void ParseFieldPrerequisiteQuest(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawPrerequisiteQuest;
            if ((RawPrerequisiteQuest = Value as string) != null)
                This.ParsePrerequisiteQuest(RawPrerequisiteQuest, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest PrerequisiteQuest");
        }

        private void ParsePrerequisiteQuest(string RawPrerequisiteQuest, ParseErrorInfo ErrorInfo)
        {
            this.RawPrerequisiteQuest = RawPrerequisiteQuest;
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
            this.RawReuseTimeHours = RawReuseTimeHours;
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
            this.RawRewardCombatXP = RawRewardCombatXP;
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

            string[] AreaNpc = RawFavorNpc.Split('/');
            if (AreaNpc.Length == 2)
            {
                string RawMapName = AreaNpc[0];
                if (RawMapName.StartsWith("Area"))
                {
                    MapAreaName ParsedArea;
                    StringToEnumConversion<MapAreaName>.TryParse(RawMapName.Substring(4), out ParsedArea, ErrorInfo);
                    FavorNpcArea = ParsedArea;
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest FavorNpc");

                FavorNpc = AreaNpc[1];
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
            RewardsAbility = RawRewardsAbility;
        }

        private static void ParseFieldRequirements(Quest This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawRequirements;
            if ((RawRequirements = Value as ArrayList) != null)
                This.ParseRequirements(RawRequirements, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
        }

        private void ParseRequirements(ArrayList RawRequirements, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawRequirement in RawRequirements)
            {
                Dictionary<string, object> AsDictionary;
                if ((AsDictionary = RawRequirement as Dictionary<string, object>) != null)
                    ParseParseRequirementAsDictionary(AsDictionary, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
            }
        }

        private void ParseParseRequirementAsDictionary(Dictionary<string, object> RawRequirement, ParseErrorInfo ErrorInfo)
        {
            if (RawRequirement.ContainsKey("T"))
            {
                string RequirementType;
                if ((RequirementType = RawRequirement["T"] as string) != null)
                {
                    if (RequirementType == "MinFavorLevel")
                    {
                        if (RawRequirement.ContainsKey("Npc") && RawRequirement.ContainsKey("Level"))
                        {
                            if (RequirementFavorNpc == null && RequirementFavorLevel == Favor.Internal_None)
                            {
                                RequirementFavorNpc = RawRequirement["Npc"] as string;

                                Favor ParsedFavor;
                                StringToEnumConversion<Favor>.TryParse(RawRequirement["Level"] as string, out ParsedFavor, ErrorInfo);
                                RequirementFavorLevel = ParsedFavor;

                                if (RequirementFavorNpc == null)
                                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "FavorLevelRequirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                    }

                    else if (RequirementType == "QuestCompleted")
                    {
                        if (RawRequirement.ContainsKey("Quest"))
                        {
                            if (RequirementQuest == null)
                            {
                                RequirementQuest = RawRequirement["Quest"] as string;
                                if (RequirementQuest == null)
                                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "QuestRequirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                    }

                    else if (RequirementType == "GuildQuestCompleted")
                    {
                        if (RawRequirement.ContainsKey("Quest"))
                        {
                            if (RequirementGuildQuest == null)
                            {
                                RequirementGuildQuest = RawRequirement["Quest"] as string;
                                if (RequirementGuildQuest == null)
                                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "QuestRequirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                    }

                    else if (RequirementType == "MinSkillLevel")
                    {
                        if (RawRequirement.ContainsKey("Skill") && RawRequirement.ContainsKey("Level"))
                        {
                            if (RequirementSkill == PowerSkill.Internal_None)
                            {
                                PowerSkill ParsedSkill;
                                StringToEnumConversion<PowerSkill>.TryParse(RawRequirement["Skill"] as string, out ParsedSkill, ErrorInfo);
                                RequirementSkill = ParsedSkill;

                                if (RawRequirement["Level"] is int)
                                    RequirementSkillLevel = (int)RawRequirement["Level"];
                                else
                                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "SkillRequirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                    }

                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
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
                            if (RewardSkill == PowerSkill.Internal_None)
                            {
                                PowerSkill ParsedSkill;
                                StringToEnumConversion<PowerSkill>.TryParse(RawReward["Skill"] as string, out ParsedSkill, ErrorInfo);
                                RewardSkill = ParsedSkill;

                                if (RawReward["Xp"] is int)
                                    RewardSkillXp = (int)RawReward["Xp"];
                                else
                                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
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
                            if (RewardCombatXp == 0)
                            {
                                if (RawReward["Xp"] is int)
                                    RewardCombatXp = (int)RawReward["Xp"];
                                else
                                    ErrorInfo.AddInvalidObjectFormat("Quest Rewards");
                            }
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
                            if (RewardGuildXp == 0)
                            {
                                if (RawReward["Xp"] is int)
                                    RewardGuildXp = (int)RawReward["Xp"];
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
                            if (RewardGuildCredits == 0)
                            {
                                if (RawReward["Credits"] is int)
                                    RewardGuildCredits = (int)RawReward["Credits"];
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
            PreGiveItemList = ParsedPreGiveItemList;
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
                    PreGiveRecipeList.Add(AsString);
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
                        RawRewardEffect = AsString;
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
            GroupingName = RawGroupingName;
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

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }

        public static Quest ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, Quest> QuestTable, string RawQuestName, Quest ParsedQuest, ref bool IsRawQuestParsed, ref bool IsConnected)
        {
            if (IsRawQuestParsed)
                return ParsedQuest;

            IsRawQuestParsed = true;

            if (RawQuestName == null)
                return null;

            foreach (KeyValuePair<string, Quest> Entry in QuestTable)
                if (Entry.Value.InternalName == RawQuestName)
                {
                    IsConnected = true;
                    return Entry.Value;
                }

            ErrorInfo.AddMissingKey(RawQuestName);
            return null;
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                Result += Name + JsonGenerator.FieldSeparator;
                Result += Description + JsonGenerator.FieldSeparator;

                foreach (QuestObjective Item in QuestObjectiveList)
                    Result += Item.TextContent + JsonGenerator.FieldSeparator;

                foreach (QuestRewardItem Item in QuestRewardsItemList)
                    Result += Item.TextContent + JsonGenerator.FieldSeparator;

                if (PrerequisiteQuest != null)
                    Result += PrerequisiteQuest.TextContent + JsonGenerator.FieldSeparator;
                Result += FavorNpc + JsonGenerator.FieldSeparator;
                Result += PrefaceText + JsonGenerator.FieldSeparator;
                Result += SuccessText + JsonGenerator.FieldSeparator;
                Result += MidwayText + JsonGenerator.FieldSeparator;
                Result += RewardsFavor + JsonGenerator.FieldSeparator;

                foreach (string RewardsRecipe in RewardsRecipeList)
                    Result += RewardsRecipe + JsonGenerator.FieldSeparator;

                Result += RewardsAbility + JsonGenerator.FieldSeparator;
                Result += RequirementFavorNpc + JsonGenerator.FieldSeparator;
                Result += RequirementQuest + JsonGenerator.FieldSeparator;
                Result += RequirementGuildQuest + JsonGenerator.FieldSeparator;
                if (RewardRecipe != null)
                    Result += RewardRecipe.TextContent + JsonGenerator.FieldSeparator;

                foreach (QuestRewardItem Item in PreGiveItemList)
                    Result += Item.TextContent + JsonGenerator.FieldSeparator;

                Result += RewardsNamedLootProfile + JsonGenerator.FieldSeparator;

                foreach (string PreGiveRecipe in PreGiveRecipeList)
                    Result += PreGiveRecipe + JsonGenerator.FieldSeparator;

                if (RewardEffect != null)
                    Result += RewardEffect.TextContent + JsonGenerator.FieldSeparator;
                Result += GroupingName + JsonGenerator.FieldSeparator;

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "Quest"; } }

        protected override void InitializeFields()
        {
            QuestObjectiveList = new List<QuestObjective>();
            QuestRewardsItemList = new List<QuestRewardItem>();
            RewardsXPList = new List<QuestRewardXp>();
            RewardsRecipeList = new List<string>();
            PreGiveItemList = new List<QuestRewardItem>();
            PreGiveRecipeList = new List<string>();
            KeywordList = new List<QuestKeyword>();
            RewardSkill = PowerSkill.Internal_None;
            RewardSkillXp = 0;
            RewardRecipe = null;
            RawRewardRecipe = null;
            IsRawRewardRecipeParsed = false;
            RewardCombatXp = 0;
            RewardGuildXp = 0;
            RewardGuildCredits = 0;
            RequirementFavorNpc = null;
            RequirementFavorLevel = Favor.Internal_None;
            RequirementQuest = null;
            RequirementGuildQuest = null;
            RequirementSkill = PowerSkill.Internal_None;
            RequirementSkillLevel = 0;
            PrerequisiteQuest = null;
            RawPrerequisiteQuest = null;
            IsRawPrerequisiteParsed = false;
            RewardEffect = null;
            RawRewardEffect = null;
            IsRawRewardEffectParsed = false;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            bool IsConnected = false;

            foreach (QuestObjective Item in QuestObjectiveList)
                IsConnected |= Item.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            foreach (QuestRewardItem Item in QuestRewardsItemList)
                IsConnected |= Item.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            foreach (QuestRewardItem Item in PreGiveItemList)
                IsConnected |= Item.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            PrerequisiteQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawPrerequisiteQuest, PrerequisiteQuest, ref IsRawPrerequisiteParsed, ref IsConnected);
            RewardRecipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRewardRecipe, RewardRecipe, ref IsRawRewardRecipeParsed, ref IsConnected);
            RewardEffect = Effect.ConnectSingleProperty(ErrorInfo, EffectTable, RawRewardEffect, RewardEffect, ref IsRawRewardEffectParsed, ref IsConnected);

            return IsConnected;
        }
        #endregion
    }
}
