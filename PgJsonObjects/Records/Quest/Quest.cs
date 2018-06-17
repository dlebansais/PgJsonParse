using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Quest : GenericJsonObject<Quest>, IPgQuest, ISearchableObject
    {
        #region Direct Properties
        public string InternalName { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get; private set; }
        public QuestObjectiveCollection QuestObjectiveList { get; } = new QuestObjectiveCollection();
        public QuestRewardXpCollection RewardsXPList { get; } = new QuestRewardXpCollection();
        public QuestRewardItemCollection QuestRewardsItemList { get; } = new QuestRewardItemCollection();
        public TimeSpan? RawReuseTime { get; private set; }
        public int RewardCombatXP { get { return RawRewardCombatXP.HasValue ? RawRewardCombatXP.Value : 0; } }
        public int? RawRewardCombatXP { get; private set; }
        public GameNpc FavorNpc { get; private set; }
        public string PrefaceText { get; private set; }
        public string SuccessText { get; private set; }
        public string MidwayText { get; private set; }
        public Ability RewardAbility { get; private set; }
        public int RewardFavor { get { return RawRewardFavor.HasValue ? RawRewardFavor.Value : 0; } }
        public int? RawRewardFavor { get; private set; }
        public Skill RewardSkill { get; private set; }
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get; private set; }
        public Recipe RewardRecipe { get; private set; }
        public int RewardGuildXp { get { return RawRewardGuildXp.HasValue ? RawRewardGuildXp.Value : 0; } }
        public int? RawRewardGuildXp { get; private set; }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get; private set; }
        public QuestRewardItemCollection PreGiveItemList { get; } = new QuestRewardItemCollection();
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        public int? RawTSysLevel { get; private set; }
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        public int? RawRewardGold { get; private set; }
        public string RewardsNamedLootProfile { get; private set; }
        public RecipeCollection PreGiveRecipeList { get; private set; } = new RecipeCollection();
        public List<QuestKeyword> KeywordList { get; } = new List<QuestKeyword>();
        public Effect RewardEffect { get; private set; }
        public LoreBook RewardLoreBook { get; private set; }
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get; private set; }
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        public bool? RawIsAutoPreface { get; private set; }
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        public bool? RawIsAutoWrapUp { get; private set; }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        public bool? RawIsGuildQuest { get; private set; }
        public MapAreaName DisplayedLocation { get; private set; }
        public Favor PrerequisiteFavorLevel { get; private set; }
        public QuestGroupingName GroupingName { get; private set; }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get; private set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; private set; }
        public Skill WorkOrderSkill { get; private set; }
        public QuestCollection FollowUpQuestList { get; private set; } = new QuestCollection();
        public QuestRequirementCollection QuestRequirementList { get; private set; } = new QuestRequirementCollection();
        public QuestRequirementCollection QuestRequirementToSustainList { get; private set; } = new QuestRequirementCollection();

        private bool IsQuestRewardsItemListEmpty;
        private int? RawReuseTime_Minutes;
        private int? RawReuseTime_Hours;
        private int? RawReuseTime_Days;
        private bool IsFavorNpcParsed;
        private MapAreaName FavorNpcArea;
        private string FavorNpcId;
        private string FavorNpcName;
        private bool IsEmptyNpc;
        private string RawRewardAbility;
        private bool IsRawRewardAbilityParsed;
        private PowerSkill RawRewardSkill;
        private bool IsConnectedRewardSkillParsed;
        private string RawRewardRecipe;
        private bool IsRawRewardRecipeEmpty;
        private bool IsRawRewardRecipeParsed;
        private List<string> RawPreGiveRecipeList { get; } = new List<string>();
        private string RawRewardEffect;
        private bool IsRawRewardEffectParsed;
        private List<string> RawRewardInteractionFlags = new List<string>();
        private string RawRewardEnsureLoreBook;
        private bool IsRawLoreBookParsed;
        private PowerSkill RawWorkOrderSkill;
        private bool IsConnectedWorkOrderSkillParsed;
        private List<string> RawFollowUpQuestList = new List<string>();
        private bool IsQuestRequirementListSimple;
        private bool IsQuestRequirementListNested;
        private List<QuestReward> QuestRewardList = new List<QuestReward>();
        #endregion

        #region Indirect Properties
        public virtual string SortingName { get { return Name; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => InternalName = value,
                GetString = () => InternalName } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseName,
                GetString = () => Name } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Description = value,
                GetString = () => Description } },
            { "Version", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawVersion = value,
                GetInteger = () => RawVersion } },
            { "RequirementsToSustain", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<QuestRequirement>.ParseList("Requirements", value, QuestRequirementToSustainList, errorInfo),
                GetObjectArray = () => QuestRequirementToSustainList,
                SimplifyArray = true } },
            { "ReuseTime_Minutes", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseReuseTime_Minutes,
                GetInteger = () => RawReuseTime_Minutes } },
            { "IsCancellable", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsCancellable = value,
                GetBool = () => RawIsCancellable } },
            { "Objectives", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<QuestObjective>.ParseList("Quest Objectives", value, QuestObjectiveList, errorInfo),
                GetObjectArray = () => QuestObjectiveList } },
            { "Rewards_XP", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseRewards_XP,
                GetObject = GetXPRewards } },
            { "Rewards_Items", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<QuestRewardItem>.ParseList("RewardItems", value, QuestRewardsItemList, errorInfo),
                SetArrayIsEmpty = () => IsQuestRewardsItemListEmpty = true,
                GetObjectArray = () => QuestRewardsItemList,
                GetArrayIsEmpty = () => IsQuestRewardsItemListEmpty } },
            { "ReuseTime_Days", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseReuseTime_Days,
                GetInteger = () => RawReuseTime_Days } },
            { "ReuseTime_Hours", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseReuseTime_Hours,
                GetInteger = () => RawReuseTime_Hours } },
            { "Reward_CombatXP", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseReward_CombatXP,
                GetInteger = () => RawRewardCombatXP } },
            { "FavorNpc", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseFavorNpc,
                GetString = () => NpcToString(FavorNpcArea, FavorNpcId, FavorNpcName, IsEmptyNpc) } },
            { "PrefaceText", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => PrefaceText = value,
                GetString = () => PrefaceText } },
            { "SuccessText", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => SuccessText = value,
                GetString = () => SuccessText } },
            { "MidwayText", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => MidwayText = value,
                GetString = () => MidwayText } },
            { "PrerequisiteFavorLevel", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => PrerequisiteFavorLevel = StringToEnumConversion<Favor>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<Favor>.ToString(PrerequisiteFavorLevel, null, Favor.Internal_None) } },
            { "Rewards_Favor", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRewardFavor = value,
                GetInteger = () => RawRewardFavor } },
            { "Rewards_Recipes", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseRewards_Recipes,
                SetArrayIsEmpty = () => IsRawRewardRecipeEmpty = true,
                GetStringArray = () => CreateSingleOrEmptyStringList(RawRewardRecipe),
                GetArrayIsEmpty = () => IsRawRewardRecipeEmpty } },
            { "Rewards_Ability", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawRewardAbility = value,
                GetString = () => RawRewardAbility } },
            { "Requirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<QuestRequirement>.ParseList("Requirements", value, QuestRequirementList, errorInfo),
                GetObjectArray = () => QuestRequirementList,
                SimplifyArray = true,
                SetArrayIsSimple = () => IsQuestRequirementListSimple = true,
                GetArrayIsSimple = () => IsQuestRequirementListSimple,
                SetArrayIsNested = () => IsQuestRequirementListNested = true,
                GetArrayIsNested = () => IsQuestRequirementListNested } },
            { "Reward_Favor", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRewardFavor = value,
                GetInteger = () => RawRewardFavor } },
            { "Rewards", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = ParseRewards,
                GetObjectArray = () => QuestRewardList } },
            { "PreGiveItems", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<QuestRewardItem>.ParseList("PreGiveItems", value, PreGiveItemList, errorInfo),
                GetObjectArray = () => PreGiveItemList } },
            { "TSysLevel", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawTSysLevel = value,
                GetInteger = () => RawTSysLevel } },
            { "Reward_Gold", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRewardGold = value,
                GetInteger = () => RawRewardGold } },
            { "Rewards_NamedLootProfile", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RewardsNamedLootProfile = value,
                GetString = () => RewardsNamedLootProfile } },
            { "PreGiveRecipes", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawPreGiveRecipeList.Add(value),
                GetStringArray = () => RawPreGiveRecipeList } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<QuestKeyword>.ParseList(value, KeywordList, errorInfo),
                GetStringArray = () => StringToEnumConversion<QuestKeyword>.ToStringList(KeywordList) } },
            { "Rewards_Effects", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseRewards_Effects,
                GetStringArray = GetRewards_Effects } },
            { "IsAutoPreface", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsAutoPreface = value,
                GetBool = () => RawIsAutoPreface } },
            { "IsAutoWrapUp", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsAutoWrapUp = value,
                GetBool = () => RawIsAutoWrapUp } },
            { "GroupingName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => GroupingName = StringToEnumConversion<QuestGroupingName>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<QuestGroupingName>.ToString(GroupingName, null, QuestGroupingName.Internal_None) } },
            { "IsGuildQuest", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsGuildQuest = value,
                GetBool = () => RawIsGuildQuest } },
            { "NumExpectedParticipants", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNumExpectedParticipants = value,
                GetInteger = () => RawNumExpectedParticipants } },
            { "Level", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawLevel = value,
                GetInteger = () => RawLevel } },
            { "WorkOrderSkill", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawWorkOrderSkill = StringToEnumConversion<PowerSkill>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawWorkOrderSkill, null, PowerSkill.Internal_None) } },
            { "DisplayedLocation", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DisplayedLocation = StringToEnumConversion<MapAreaName>.Parse(value, TextMaps.MapAreaNameStringMap, errorInfo),
                GetString = () => StringToEnumConversion<MapAreaName>.ToString(DisplayedLocation, TextMaps.MapAreaNameStringMap, MapAreaName.Internal_None) } },
            { "FollowUpQuests", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawFollowUpQuestList.Add(value),
                GetStringArray = () => RawFollowUpQuestList } },
        }; } }

        private void ParseName(string value, ParseErrorInfo ErrorInfo)
        {
            Name = value;
            ErrorInfo.AddIconId(SearchResultIconId);
        }

        private void ParseReuseTime_Minutes(int value, ParseErrorInfo ErrorInfo)
        {
            RawReuseTime_Minutes = value;

            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromMinutes(value);
            else
                RawReuseTime += TimeSpan.FromMinutes(value);
        }

        private void ParseRewards_XP(JsonObject value, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> RawRewardXP in value)
            {
                PowerSkill ParsedSkill;
                if (StringToEnumConversion<PowerSkill>.TryParse(RawRewardXP.Key, out ParsedSkill, ErrorInfo))
                {
                    JsonInteger AsJsonInteger;
                    if ((AsJsonInteger = RawRewardXP.Value as JsonInteger) != null)
                    {
                        QuestRewardXp NewReward = new QuestRewardXp();
                        NewReward.RawSkill = ParsedSkill;
                        NewReward.RawXp = AsJsonInteger.Number;
                        RewardsXPList.Add(NewReward);
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Quest RewardsXP");
                }
            }
        }

        private IObjectContentGenerator GetXPRewards()
        {
            XPReward Rewards = new XPReward();

            foreach (QuestRewardXp Item in RewardsXPList)
                Rewards.SetFieldValue(Item.RawSkill, Item.Xp);

            return Rewards;
        }

        private void ParseReuseTime_Days(int value, ParseErrorInfo ErrorInfo)
        {
            RawReuseTime_Days = value;

            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromDays(value);
            else
                RawReuseTime += TimeSpan.FromDays(value);
        }

        private void ParseReuseTime_Hours(int value, ParseErrorInfo ErrorInfo)
        {
            RawReuseTime_Hours = value;

            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromHours(value);
            else
                RawReuseTime += TimeSpan.FromHours(value);
        }

        private void ParseReward_CombatXP(int value, ParseErrorInfo ErrorInfo)
        {
            if (RawRewardCombatXP == null)
                RawRewardCombatXP = value;
            else
                ErrorInfo.AddInvalidObjectFormat("Quest RewardCombatXP");
        }

        private void ParseFavorNpc(string value, ParseErrorInfo ErrorInfo)
        {
            if (value.Length == 0)
            {
                IsEmptyNpc = true;
                return;
            }

            MapAreaName ParsedArea;
            string NpcNameId;
            string NpcName;
            if (TryParseNPC(value, out ParsedArea, out NpcNameId, out NpcName, ErrorInfo))
            {
                FavorNpcArea = ParsedArea;
                FavorNpcId = NpcNameId;
                IsFavorNpcParsed = false;
                FavorNpcName = NpcName;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Quest FavorNpc");
        }

        private bool ParseRewards_Recipes(string value, ParseErrorInfo ErrorInfo)
        {
            if (RawRewardRecipe == null)
            {
                RawRewardRecipe = value;
                return true;
            }
            else
            {
                ErrorInfo.AddDuplicateString("Quest", "RecipeRewards");
                return false;
            }
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
                            if (RawRewardSkill == PowerSkill.Internal_None && !RawRewardSkillXp.HasValue)
                            {
                                JsonString SkillValue;
                                JsonInteger XpValue;
                                if (((SkillValue = RawReward["Skill"] as JsonString) != null) && ((XpValue = RawReward["Xp"] as JsonInteger) != null))
                                {
                                    if (StringToEnumConversion<PowerSkill>.TryParse(SkillValue.String, out PowerSkill ParsedSkill, ErrorInfo))
                                    {
                                        RawRewardSkill = ParsedSkill;
                                        RawRewardSkillXp = XpValue.Number;

                                        QuestReward NewReward = new QuestReward(RewardType);
                                        NewReward.RewardSkill = RawRewardSkill;
                                        NewReward.RawRewardXp = RawRewardSkillXp;
                                        NewReward.AddFieldTableOrder("T");
                                        NewReward.AddFieldTableOrder("Skill");
                                        NewReward.AddFieldTableOrder("Xp");
                                        QuestRewardList.Add(NewReward);
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
                                {
                                    RawRewardRecipe = RecipeValue.String;

                                    QuestReward NewReward = new QuestReward(RewardType);
                                    NewReward.RewardRecipe = RawRewardRecipe;
                                    NewReward.AddFieldTableOrder("T");
                                    NewReward.AddFieldTableOrder("Recipe");
                                    QuestRewardList.Add(NewReward);
                                }
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
                                {
                                    RawRewardCombatXP = XpValue.Number;

                                    QuestReward NewReward = new QuestReward(RewardType);
                                    NewReward.RawRewardXp = RawRewardCombatXP;
                                    NewReward.AddFieldTableOrder("T");
                                    NewReward.AddFieldTableOrder("Xp");
                                    QuestRewardList.Add(NewReward);
                                }
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
                                {
                                    RawRewardGuildXp = XpValue.Number;

                                    QuestReward NewReward = new QuestReward(RewardType);
                                    NewReward.RawRewardXp = RawRewardGuildXp;
                                    NewReward.AddFieldTableOrder("T");
                                    NewReward.AddFieldTableOrder("Xp");
                                    QuestRewardList.Add(NewReward);
                                }
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
                                {
                                    RawRewardGuildCredits = CreditsValue.Number;

                                    QuestReward NewReward = new QuestReward(RewardType);
                                    NewReward.RawRewardGuildCredits = RawRewardGuildCredits;
                                    NewReward.AddFieldTableOrder("T");
                                    NewReward.AddFieldTableOrder("Credits");
                                    QuestRewardList.Add(NewReward);
                                }
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

        private bool ParseRewards_Effects(string RawRewardEffect, ParseErrorInfo ErrorInfo)
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

        private List<string> GetRewards_Effects()
        {
            List<string> Result = new List<string>();

            foreach (string RewardInteractionFlag in RawRewardInteractionFlags)
                Result.Add("SetInteractionFlag(" + RewardInteractionFlag + ")");

            if (RawRewardEnsureLoreBook != null)
                Result.Add("EnsureLoreBookKnown(" + RawRewardEnsureLoreBook + ")");

            if (RawRewardEffect != null)
                Result.Add(RawRewardEffect);

            return Result;
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
                    ParsedArea = StringToEnumConversion<MapAreaName>.Parse(RawMapName.Substring(4), TextMaps.MapAreaNameStringMap, ErrorInfo);
                else
                    return false;

                string Npc = AreaNpc[1];
                if (Npc.ToUpper().StartsWith("NPC_"))
                {
                    NpcId = Npc;
                    NpcName = Npc.Substring(4);
                    return true;
                }
                else if (StringToEnumConversion<SpecialNpc>.TryParse(Npc, out SpecialNpc ParsedSpecialNpc, ErrorInfo))
                {
                    NpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
                    return true;
                }
                else
                    return false;
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

            else if (StringToEnumConversion<SpecialNpc>.TryParse(Npc, out SpecialNpc ParsedSpecialNpc, ErrorInfo))
            {
                NpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
                return true;
            }

            else
                return false;
        }

        public static string NpcToString(string NpcId, string NpcName)
        {
            if (NpcId != null)
                return NpcId;

            foreach (KeyValuePair<SpecialNpc, string> Entry in TextMaps.SpecialNpcTextMap)
                if (Entry.Value == NpcName)
                    return StringToEnumConversion<SpecialNpc>.ToString(Entry.Key);

            return null;
        }

        public static string NpcToString(MapAreaName area, string NpcId, string NpcName, bool isEmpty)
        {
            if (isEmpty)
                return "";

            if (NpcName == null)
                return null;

            string Result = "Area" + StringToEnumConversion<MapAreaName>.ToString(area) + "/";

            if (NpcId != null)
                Result += "NPC_" + NpcName;
            else
            {
                foreach (KeyValuePair<SpecialNpc, string> Entry in TextMaps.SpecialNpcTextMap)
                    if (Entry.Value == NpcName)
                    {
                        Result += StringToEnumConversion<SpecialNpc>.ToString(Entry.Key);
                        break;
                    }
            }

            return Result;
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
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[Reward.RawSkill]);
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
                if (RewardSkill != null)
                    AddWithFieldSeparator(ref Result, RewardSkill.Name);
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
                if (RawWorkOrderSkill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[RawWorkOrderSkill]);
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
                    Item.Skill = Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Item.RawSkill, Item.Skill, ref IsSkillParsed, ref IsConnected, this);
                    Item.IsSkillParsed = IsSkillParsed;
                }

            foreach (QuestRewardItem Item in QuestRewardsItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            foreach (QuestRewardItem Item in PreGiveItemList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            RewardAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawRewardAbility, RewardAbility, ref IsRawRewardAbilityParsed, ref IsConnected, this);
            RewardRecipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRewardRecipe, RewardRecipe, ref IsRawRewardRecipeParsed, ref IsConnected, this);
            RewardEffect = Effect.ConnectSingleProperty(ErrorInfo, EffectTable, RawRewardEffect, RewardEffect, ref IsRawRewardEffectParsed, ref IsConnected, this);
            RewardSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawRewardSkill, RewardSkill, ref IsConnectedRewardSkillParsed, ref IsConnected, this);
            WorkOrderSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawWorkOrderSkill, WorkOrderSkill, ref IsConnectedWorkOrderSkillParsed, ref IsConnected, this);

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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddString(InternalName , data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Name , data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(Description , data, ref offset, BaseOffset, 8, StoredStringtable);
            AddInt(RawVersion , data, ref offset, BaseOffset, 12);
            AddObjectList(QuestObjectiveList , data, ref offset, BaseOffset,16, StoredObjectListTable);
            AddObjectList(RewardsXPList , data, ref offset, BaseOffset, 20, StoredObjectListTable);
            AddObjectList(QuestRewardsItemList , data, ref offset, BaseOffset,24, StoredObjectListTable);
            AddTimeSpan(RawReuseTime , data, ref offset, BaseOffset,28);
            AddInt(RawRewardCombatXP , data, ref offset, BaseOffset, 32);
            AddObject(FavorNpc , data, ref offset, BaseOffset,  36, StoredObjectTable);
            AddString(PrefaceText , data, ref offset, BaseOffset, 40, StoredStringtable);
            AddString(SuccessText , data, ref offset, BaseOffset, 44, StoredStringtable);
            AddString(MidwayText , data, ref offset, BaseOffset,  48, StoredStringtable);
            AddObject(RewardAbility , data, ref offset, BaseOffset, 52, StoredObjectTable);
            AddInt(RawRewardFavor , data, ref offset, BaseOffset,56);
            AddObject(RewardSkill , data, ref offset, BaseOffset, 60, StoredObjectTable);
            AddInt(RawRewardSkillXp , data, ref offset, BaseOffset,64);
            AddObject(RewardRecipe , data, ref offset, BaseOffset,68, StoredObjectTable);
            AddInt(RawRewardGuildXp , data, ref offset, BaseOffset,72);
            AddInt(RawRewardGuildCredits , data, ref offset, BaseOffset, 76);
            AddObjectList(PreGiveItemList , data, ref offset, BaseOffset, 80, StoredObjectListTable);
            AddInt(RawTSysLevel , data, ref offset, BaseOffset, 84);
            AddInt(RawRewardGold , data, ref offset, BaseOffset,88);
            AddString(RewardsNamedLootProfile , data, ref offset, BaseOffset, 92, StoredStringtable);
            AddObjectList(PreGiveRecipeList , data, ref offset, BaseOffset, 96, StoredObjectListTable);
            AddEnumList(KeywordList , data, ref offset, BaseOffset, 100, StoredEnumListTable);
            AddObject(RewardEffect , data, ref offset, BaseOffset, 104, StoredObjectTable);
            AddObject(RewardLoreBook , data, ref offset, BaseOffset, 108, StoredObjectTable);
            AddBool(RawIsCancellable , data, ref offset, ref BitOffset, BaseOffset, 112, 0);
            AddBool(RawIsAutoPreface , data, ref offset, ref BitOffset, BaseOffset, 112, 2);
            AddBool(RawIsAutoWrapUp , data, ref offset, ref BitOffset, BaseOffset,  112, 4);
            AddBool(RawIsGuildQuest , data, ref offset, ref BitOffset, BaseOffset,  112, 6);
            CloseBool(ref offset, ref BitOffset);
            AddEnum(DisplayedLocation , data, ref offset, BaseOffset, 114);
            AddEnum(PrerequisiteFavorLevel , data, ref offset, BaseOffset,116);
            AddEnum(GroupingName , data, ref offset, BaseOffset,  118);
            AddInt(RawNumExpectedParticipants , data, ref offset, BaseOffset, 120);
            AddInt(RawLevel , data, ref offset, BaseOffset,124);
            AddObject(WorkOrderSkill , data, ref offset, BaseOffset, 128, StoredObjectTable);
            AddObjectList(FollowUpQuestList , data, ref offset, BaseOffset,132, StoredObjectListTable);
            AddObjectList(QuestRequirementList , data, ref offset, BaseOffset,136, StoredObjectListTable);
            AddObjectList(QuestRequirementToSustainList , data, ref offset, BaseOffset, 140, StoredObjectListTable);

            FinishSerializing(data, ref offset, BaseOffset, 144, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
