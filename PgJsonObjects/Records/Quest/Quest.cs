using System;
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
            { "RequirementsToSustain", ParseFieldRequirementsToSustain },
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
        public int? RawVersion { get; private set; }
        public EffectKeyword RequirementToSustain { get; private set; }
        public int ReuseTimeMinutes { get { return RawReuseTimeMinutes.HasValue ? RawReuseTimeMinutes.Value : 0; } }
        private int? RawReuseTimeMinutes;
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get; private set; }
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
        public int? RawRewardCombatXP { get; private set; }
        public string FavorNpc { get; private set; }
        public MapAreaName FavorNpcArea { get; private set; }
        public string PrefaceText { get; private set; }
        public string SuccessText { get; private set; }
        public string MidwayText { get; private set; }
        public Favor PrerequisiteFavorLevel { get; private set; }
        public Ability RewardAbility { get; private set; }
        private string RawRewardAbility;
        private bool IsRawRewardAbilityParsed;
        public string RequirementFavorNpc { get; private set; }
        public MapAreaName RequirementFavorNpcArea { get; private set; }
        public Favor RequirementFavorLevel { get; private set; }
        public Quest RequirementQuest { get; private set; }
        private string RawRequirementQuest;
        private bool IsRawRequirementQuestParsed;
        public Quest RequirementGuildQuest { get; private set; }
        public string RawRequirementGuildQuest { get; private set; }
        private bool IsRawRequirementGuildQuestParsed;
        public PowerSkill RequirementSkill { get; private set; }
        public Skill ConnectedRequirementSkill { get; private set; }
        private bool IsConnectedRequirementSkillParsed;
        public int RequirementSkillLevel { get; private set; }
        public int RewardFavor { get { return RawRewardFavor.HasValue ? RawRewardFavor.Value : 0; } }
        public int? RawRewardFavor { get; private set; }
        public PowerSkill RewardSkill { get; private set; }
        public Skill ConnectedRewardSkill { get; private set; }
        private bool IsConnectedRewardSkillParsed;
        public int RewardSkillXp { get; private set; }
        public Recipe RewardRecipe { get; private set; }
        private string RawRewardRecipe;
        private bool IsRawRewardRecipeParsed;
        public int RewardGuildXp { get { return RawRewardGuildXp.HasValue ? RawRewardGuildXp.Value : 0; } }
        public int? RawRewardGuildXp { get; private set; }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get; private set; }
        public List<QuestRewardItem> PreGiveItemList { get; private set; }
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        public int? RawTSysLevel { get; private set; }
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        public int? RawRewardGold { get; private set; }
        public string RewardsNamedLootProfile { get; private set; }
        public List<Recipe> PreGiveRecipeList { get; private set; }
        private List<string> RawPreGiveRecipeList;
        public List<QuestKeyword> KeywordList { get; private set; }
        public Effect RewardEffect { get; private set; }
        private string RawRewardEffect;
        private bool IsRawRewardEffectParsed;
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

        protected override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }

        public string CombinedReuseTime
        {
            get
            {
                TimeSpan ReuseTime = TimeSpan.Zero;
                if (RawReuseTimeDays.HasValue && RawReuseTimeDays.Value > 0)
                    ReuseTime += TimeSpan.FromDays(RawReuseTimeDays.Value);
                if (RawReuseTimeHours.HasValue && RawReuseTimeHours.Value > 0)
                    ReuseTime += TimeSpan.FromHours(RawReuseTimeHours.Value);
                if (RawReuseTimeMinutes.HasValue && RawReuseTimeMinutes.Value > 0)
                    ReuseTime += TimeSpan.FromMinutes(RawReuseTimeMinutes.Value);

                if (ReuseTime.Ticks > 0)
                    return ReuseTime.ToString();
                else
                    return "";
            }
        }

        public bool HasRewardsXp { get { return RewardsXPList.Count > 0; } }

        public string CombinedRewardsXp
        {
            get
            {
                string Result = "";

                foreach (QuestRewardXp Reward in RewardsXPList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += TextMaps.PowerSkillTextMap[Reward.Skill] + " (" + Reward.Xp + " XP)";
                }

                return Result;
            }
        }

        public bool HasRewardItems { get { return QuestRewardsItemList.Count > 0; } }

        public string CombinedRewardsItem
        {
            get
            {
                string Result = "";

                foreach (QuestRewardItem Reward in QuestRewardsItemList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += Reward.QuestItem.Name;
                    if (Reward.StackSize > 0)
                        Result += " x" + Reward.StackSize;
                }

                return Result;
            }
        }

        public string CombinedPrerequisiteQuest
        {
            get
            {
                if (PrerequisiteQuest == null)
                    return "None";

                return PrerequisiteQuest.Name;
            }
        }

        public string CombinedRewardAbility
        {
            get
            {
                if (RewardAbility == null)
                    return "None";

                return RewardAbility.Name;
            }
        }

        public string CombinedFavorNPC
        {
            get
            {
                if (FavorNpcArea == MapAreaName.Internal_None || FavorNpc == null || FavorNpc.Length == 0)
                    return "";

                return FavorNpc + " in " + TextMaps.MapAreaNameTextMap[FavorNpcArea];
            }
        }

        public string CombinedRequirementFavorNPC
        {
            get
            {
                if (RequirementFavorNpcArea == MapAreaName.Internal_None || RequirementFavorNpc == null || RequirementFavorNpc.Length == 0)
                    return "";

                return "At least " + TextMaps.FavorTextMap[RequirementFavorLevel] + " with " + RequirementFavorNpc + " in " + TextMaps.MapAreaNameTextMap[RequirementFavorNpcArea];
            }
        }

        public string CombinedRequirementQuest
        {
            get
            {
                if (RequirementQuest == null)
                    return "None";

                return RequirementQuest.Name;
            }
        }

        public string CombinedRequirementGuildQuest
        {
            get
            {
                if (RequirementGuildQuest == null)
                    return "None";

                return RequirementGuildQuest.Name;
            }
        }

        public bool HasRequirementSkill { get { return RequirementSkill != PowerSkill.Internal_None && RequirementSkillLevel > 0; } }

        public string CombinedRequirementSkill
        {
            get
            {
                if (RequirementSkill == PowerSkill.Internal_None || RequirementSkillLevel == 0)
                    return "None";

                return TextMaps.PowerSkillTextMap[RequirementSkill] + " at level " + RequirementSkillLevel;
            }
        }

        public bool HasSkillXpReward { get { return RewardSkill != PowerSkill.Internal_None && RewardSkillXp > 0; } }

        public string CombinedSkillXpReward
        {
            get
            {
                if (RewardSkill == PowerSkill.Internal_None || RewardSkillXp == 0)
                    return "None";

                return RewardSkillXp + " XP in " + TextMaps.PowerSkillTextMap[RewardSkill];
            }
        }

        public string CombinedRewardRecipe
        {
            get
            {
                if (RewardRecipe == null)
                    return "None";

                return RewardRecipe.Name;
            }
        }

        public string CombinedRewardEffect
        {
            get
            {
                if (RewardEffect == null)
                    return "None";

                return RewardEffect.Name;
            }
        }

        public string CombinedKeywords
        {
            get
            {
                string Result = "";

                foreach (QuestKeyword Keyword in KeywordList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += TextMaps.QuestKeywordTextMap[Keyword];
                }

                return Result;
            }
        }

        public bool HasPreGiveItemList { get { return PreGiveItemList.Count > 0; } }

        public string CombinedPreGiveItemList
        {
            get
            {
                string Result = "";

                foreach (QuestRewardItem Reward in PreGiveItemList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += Reward.QuestItem.Name;
                    if (Reward.StackSize > 0)
                        Result += " x" + Reward.StackSize;
                }

                return Result;
            }
        }

        public bool HasPreGiveRecipeList { get { return PreGiveRecipeList.Count > 0; } }

        public string CombinedPreGiveRecipeList
        {
            get
            {
                string Result = "";

                foreach (Recipe Item in PreGiveRecipeList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += Item.Name;
                }

                return Result;
            }
        }

        public string CombinedWorkOrderSkill
        {
            get
            {
                if (ConnectedWorkOrderSkill == null)
                    return "None";

                return ConnectedWorkOrderSkill.Name;
            }
        }
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

        private void ParseRequirementToSustainAsDictionary(Dictionary<string, object> RawRequirement, ParseErrorInfo ErrorInfo)
        {
            if (RawRequirement.ContainsKey("T"))
            {
                string RequirementType;
                if ((RequirementType = RawRequirement["T"] as string) != null)
                {
                    if (RequirementType == "HasEffectKeyword")
                    {
                        if (RawRequirement.ContainsKey("Keyword"))
                        {
                            if (RequirementToSustain == EffectKeyword.Internal_None)
                            {
                                EffectKeyword ParsedEffectKeyword;
                                StringToEnumConversion<EffectKeyword>.TryParse(RawRequirement["Keyword"] as string, out ParsedEffectKeyword, ErrorInfo);
                                RequirementToSustain = ParsedEffectKeyword;
                            }
                            else
                                ErrorInfo.AddDuplicateString("Quest", "RequirementsToSustain");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("Quest RequirementsToSustain");
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest RequirementsToSustain");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Quest RequirementsToSustain");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RequirementsToSustain");
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
                if (FavorNpc.ToUpper().StartsWith("NPC_"))
                    FavorNpc = FavorNpc.Substring(4);
                else if (FavorNpc == "WerewolfAltar")
                    FavorNpc = "Werewolf Altar";
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
                                string RawRequirementFavorNpc = RawRequirement["Npc"] as string;

                                Favor ParsedFavor;
                                StringToEnumConversion<Favor>.TryParse(RawRequirement["Level"] as string, out ParsedFavor, ErrorInfo);
                                RequirementFavorLevel = ParsedFavor;

                                if (RawRequirementFavorNpc == null)
                                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                                else
                                {
                                    string[] AreaNpc = RawRequirementFavorNpc.Split('/');
                                    if (AreaNpc.Length == 2)
                                    {
                                        string RawMapName = AreaNpc[0];
                                        if (RawMapName.StartsWith("Area"))
                                        {
                                            MapAreaName ParsedArea;
                                            StringToEnumConversion<MapAreaName>.TryParse(RawMapName.Substring(4), out ParsedArea, ErrorInfo);
                                            RequirementFavorNpcArea = ParsedArea;
                                        }
                                        else
                                            ErrorInfo.AddInvalidObjectFormat("Quest FavorNpc");

                                        RequirementFavorNpc = AreaNpc[1];
                                        if (RequirementFavorNpc.ToUpper().StartsWith("NPC_"))
                                            RequirementFavorNpc = RequirementFavorNpc.Substring(4);
                                        else if (RequirementFavorNpc == "WerewolfAltar")
                                            RequirementFavorNpc = "Werewolf Altar";
                                    }
                                    else
                                        ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                                }
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
                            if (RawRequirementQuest == null)
                            {
                                RawRequirementQuest = RawRequirement["Quest"] as string;
                                if (RawRequirementQuest == null)
                                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                                else
                                {
                                    RequirementQuest = null;
                                    IsRawRequirementQuestParsed = false;
                                }
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
                            if (RawRequirementGuildQuest == null)
                            {
                                RawRequirementGuildQuest = RawRequirement["Quest"] as string;
                                if (RawRequirementGuildQuest == null)
                                    ErrorInfo.AddInvalidObjectFormat("Quest Requirements");
                                else
                                {
                                    RequirementGuildQuest = null;
                                    IsRawRequirementGuildQuestParsed = false;
                                }
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

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }

        public static Quest ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, Quest> QuestTable, string RawQuestName, Quest ParsedQuest, ref bool IsRawQuestParsed, ref bool IsConnected, GenericJsonObject LinkBack)
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
                    Entry.Value.AddLinkBack(LinkBack);
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

                AddWithFieldSeparator(ref Result, Name);
                AddWithFieldSeparator(ref Result, Description);
                AddWithFieldSeparator(ref Result, CombinedReuseTime);
                if (RequirementToSustain != EffectKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.EffectKeywordTextMap[RequirementToSustain]);
                if (RawIsCancellable.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Cancellable");
                foreach (QuestObjective Item in QuestObjectiveList)
                    AddWithFieldSeparator(ref Result, Item.Description);
                AddWithFieldSeparator(ref Result, CombinedRewardsXp);
                AddWithFieldSeparator(ref Result, CombinedRewardsItem);
                if (PrerequisiteQuest != null)
                    AddWithFieldSeparator(ref Result, PrerequisiteQuest.Name);
                AddWithFieldSeparator(ref Result, CombinedFavorNPC);
                AddWithFieldSeparator(ref Result, PrefaceText);
                AddWithFieldSeparator(ref Result, SuccessText);
                AddWithFieldSeparator(ref Result, MidwayText);
                if (PrerequisiteFavorLevel != Favor.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.FavorTextMap[PrerequisiteFavorLevel]);
                if (RewardAbility != null)
                    AddWithFieldSeparator(ref Result, RewardAbility.Name);
                AddWithFieldSeparator(ref Result, CombinedRequirementFavorNPC);
                if (RequirementQuest != null)
                    AddWithFieldSeparator(ref Result, RequirementQuest.Name);
                if (RequirementGuildQuest != null)
                    AddWithFieldSeparator(ref Result, RequirementGuildQuest.Name);
                AddWithFieldSeparator(ref Result, CombinedRequirementSkill);
                AddWithFieldSeparator(ref Result, CombinedSkillXpReward);
                if (RewardRecipe != null)
                    AddWithFieldSeparator(ref Result, RewardRecipe.Name);
                foreach (QuestRewardItem Item in PreGiveItemList)
                    AddWithFieldSeparator(ref Result, Item.CombinedName);
                foreach (Recipe Item in PreGiveRecipeList)
                    AddWithFieldSeparator(ref Result, Item.Name);
                AddWithFieldSeparator(ref Result, CombinedKeywords);
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
            PreGiveItemList = new List<QuestRewardItem>();
            PreGiveRecipeList = null;
            RawPreGiveRecipeList = new List<string>();
            KeywordList = new List<QuestKeyword>();
            RewardSkill = PowerSkill.Internal_None;
            RewardSkillXp = 0;
            RewardRecipe = null;
            RawRewardRecipe = null;
            IsRawRewardRecipeParsed = false;
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

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            foreach (QuestObjective Item in QuestObjectiveList)
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (QuestRewardXp Item in RewardsXPList)
                if (!Item.IsSkillParsed)
                {
                    bool IsSkillParsed = false;
                    Item.ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Item.Skill, Item.ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);
                    Item.IsSkillParsed = IsSkillParsed;
                }

            foreach (QuestRewardItem Item in QuestRewardsItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (QuestRewardItem Item in PreGiveItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            PrerequisiteQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawPrerequisiteQuest, PrerequisiteQuest, ref IsRawPrerequisiteParsed, ref IsConnected, this);
            RequirementQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawRequirementQuest, RequirementQuest, ref IsRawRequirementQuestParsed, ref IsConnected, this);
            RequirementGuildQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawRequirementGuildQuest, RequirementGuildQuest, ref IsRawRequirementGuildQuestParsed, ref IsConnected, this);
            RewardAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawRewardAbility, RewardAbility, ref IsRawRewardAbilityParsed, ref IsConnected, this);
            RewardRecipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRewardRecipe, RewardRecipe, ref IsRawRewardRecipeParsed, ref IsConnected, this);
            RewardEffect = Effect.ConnectSingleProperty(ErrorInfo, EffectTable, RawRewardEffect, RewardEffect, ref IsRawRewardEffectParsed, ref IsConnected, this);
            ConnectedRequirementSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RequirementSkill, ConnectedRequirementSkill, ref IsConnectedRequirementSkillParsed, ref IsConnected, this);
            ConnectedRewardSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RewardSkill, ConnectedRewardSkill, ref IsConnectedRewardSkillParsed, ref IsConnected, this);
            ConnectedWorkOrderSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, WorkOrderSkill, ConnectedWorkOrderSkill, ref IsConnectedWorkOrderSkillParsed, ref IsConnected, this);

            if (PreGiveRecipeList == null)
            {
                PreGiveRecipeList = new List<Recipe>();
                foreach (string RawPreGiveRecipe in RawPreGiveRecipeList)
                {
                    Recipe PreGiveRecipe = null;
                    bool IsRawPreGiveRecipeParsed = false;
                    PreGiveRecipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawPreGiveRecipe, PreGiveRecipe, ref IsRawPreGiveRecipeParsed, ref IsConnected, this);

                    if (PreGiveRecipe != null)
                        PreGiveRecipeList.Add(PreGiveRecipe);
                }
            }
            return IsConnected;
        }
        #endregion
    }
}
