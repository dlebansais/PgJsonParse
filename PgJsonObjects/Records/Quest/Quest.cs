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
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "InternalName", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { InternalName = value; }} },
            { "Name", new FieldParser() { Type = FieldType.String, ParserString = ParseName } },
            { "Description", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { Description = value; }} },
            { "Version", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawVersion = value; }} },
            { "RequirementsToSustain", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseRequirementsToSustain } },
            { "ReuseTime_Minutes", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseReuseTime_Minutes } },
            { "IsCancellable", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawIsCancellable = value; }} },
            { "Objectives", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseObjectives } },
            { "Rewards_XP", new FieldParser() { Type = FieldType.Object, ParserObject = ParseRewards_XP } },
            { "Rewards_Items", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseRewards_Items } },
            { "ReuseTime_Days", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseReuseTime_Days } },
            { "ReuseTime_Hours", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseReuseTime_Hours } },
            { "Reward_CombatXP", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseReward_CombatXP } },
            { "FavorNpc", new FieldParser() { Type = FieldType.String, ParserString = ParseFavorNpc } },
            { "PrefaceText", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { PrefaceText = value; }} },
            { "SuccessText", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { SuccessText = value; }} },
            { "MidwayText", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { MidwayText = value; }} },
            { "PrerequisiteFavorLevel", new FieldParser() { Type = FieldType.String, ParserString = ParsePrerequisiteFavorLevel } },
            { "Rewards_Favor", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRewardFavor = value; }} },
            { "Rewards_Recipes", new FieldParser() { Type = FieldType.StringArray, ParserStringArray = ParseRewards_Recipes } },
            { "Rewards_Ability", new FieldParser() { Type = FieldType.String, ParserString = ParseRewards_Ability } },
            { "Requirements", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseRequirements } },
            { "Reward_Favor", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRewardFavor = value; }} },
            { "Rewards", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseRewards } },
            { "PreGiveItems", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParsePreGiveItems } },
            { "TSysLevel", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawTSysLevel = value; }} },
            { "Reward_Gold", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRewardGold = value; }} },
            { "Rewards_NamedLootProfile", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RewardsNamedLootProfile = value; }} },
            { "PreGiveRecipes", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawPreGiveRecipeList.Add(value); }} },
            { "Keywords", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = ParseKeywords } },
            { "Rewards_Effects", new FieldParser() { Type = FieldType.StringArray, ParserStringArray = ParseRewards_Effects } },
            { "IsAutoPreface", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawIsAutoPreface = value; }} },
            { "IsAutoWrapUp", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawIsAutoWrapUp = value; }} },
            { "GroupingName", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { GroupingName = StringToEnumConversion<QuestGroupingName>.Parse(value, errorInfo); }} },
            { "IsGuildQuest", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawIsGuildQuest = value; }} },
            { "NumExpectedParticipants", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawNumExpectedParticipants = value; }} },
            { "Level", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawLevel = value; }} },
            { "WorkOrderSkill", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { WorkOrderSkill = StringToEnumConversion<PowerSkill>.Parse(value, errorInfo); }} },
            { "DisplayedLocation", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { DisplayedLocation = StringToEnumConversion<MapAreaName>.Parse(value, TextMaps.MapAreaNameStringMap, errorInfo); }} },
            { "FollowUpQuests", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawFollowUpQuestList.Add(value); }} },
        }; } }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
            ErrorInfo.AddIconId(SearchResultIconId);
        }

        private void ParseRequirementsToSustain(JsonObject RawRequirements, ParseErrorInfo ErrorInfo)
        {
            QuestRequirement ParsedQuestRequirement;
            JsonObjectParser<QuestRequirement>.InitAsSubitem("Requirements", RawRequirements, out ParsedQuestRequirement, ErrorInfo);

            QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
            if (ConvertedQuestRequirement != null)
                QuestRequirementToSustainList.Add(ConvertedQuestRequirement);
        }

        private void ParseReuseTime_Minutes(int value, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromMinutes(value);
            else
                RawReuseTime += TimeSpan.FromMinutes(value);
        }

        private void ParseObjectives(JsonObject RawObjectives, ParseErrorInfo ErrorInfo)
        {
            QuestObjective ParsedQuestObjective;
            JsonObjectParser<QuestObjective>.InitAsSubitem("Quest Objectives", RawObjectives, out ParsedQuestObjective, ErrorInfo);

            QuestObjective ConvertedQuestObjective = ParsedQuestObjective.ToSpecificQuestObjective(ErrorInfo);
            if (ConvertedQuestObjective != null)
                QuestObjectiveList.Add(ConvertedQuestObjective);
        }

        private void ParseRewards_XP(JsonObject RawRewardsXP, ParseErrorInfo ErrorInfo)
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

        private void ParseRewards_Items(JsonObject RawRewardsItems, ParseErrorInfo ErrorInfo)
        {
            QuestRewardItem ParsedRewardItem;
            JsonObjectParser<QuestRewardItem>.InitAsSubitem("RewardsItems", RawRewardsItems, out ParsedRewardItem, ErrorInfo);
            if (ParsedRewardItem != null)
                QuestRewardsItemList.Add(ParsedRewardItem);
        }

        private void ParseReuseTime_Days(int value, ParseErrorInfo ErrorInfo)
        {
            if (!RawReuseTime.HasValue)
                RawReuseTime = TimeSpan.FromDays(value);
            else
                RawReuseTime += TimeSpan.FromDays(value);
        }

        private void ParseReuseTime_Hours(int value, ParseErrorInfo ErrorInfo)
        {
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

        private void ParsePrerequisiteFavorLevel(string RawPrerequisiteFavorLevel, ParseErrorInfo ErrorInfo)
        {
            Favor ParsedFavor;
            StringToEnumConversion<Favor>.TryParse(RawPrerequisiteFavorLevel, out ParsedFavor, ErrorInfo);
            PrerequisiteFavorLevel = ParsedFavor;
        }

        private bool ParseRewards_Recipes(string RawRewardRecipe, ParseErrorInfo ErrorInfo)
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

        private void ParseRewards_Ability(string RawRewardsAbility, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardAbility = RawRewardsAbility;
            RewardAbility = null;
            IsRawRewardAbilityParsed = false;
        }

        private void ParseRequirements(JsonObject RawRequirements, ParseErrorInfo ErrorInfo)
        {
            QuestRequirement ParsedQuestRequirement;
            JsonObjectParser<QuestRequirement>.InitAsSubitem("Requirements", RawRequirements, out ParsedQuestRequirement, ErrorInfo);

            QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
            if (ConvertedQuestRequirement != null)
                QuestRequirementList.Add(ConvertedQuestRequirement);
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
                                    if (StringToEnumConversion<PowerSkill>.TryParse(SkillValue.String, out PowerSkill ParsedSkill, ErrorInfo))
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

        private void ParsePreGiveItems(JsonObject RawPreGiveItems, ParseErrorInfo ErrorInfo)
        {
            QuestRewardItem ParsedPreGiveItem;
            JsonObjectParser<QuestRewardItem>.InitAsSubitem("PreGiveItems", RawPreGiveItems, out ParsedPreGiveItem, ErrorInfo);
            if (ParsedPreGiveItem != null)
                PreGiveItemList.Add(ParsedPreGiveItem);
        }

        private void ParseKeywords(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<QuestKeyword>.TryParse(RawKeyword, out QuestKeyword ParsedKeyword, ErrorInfo))
                KeywordList.Add(ParsedKeyword);
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
