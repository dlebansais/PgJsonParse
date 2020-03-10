using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuest: MainPgObject<PgQuest>, IPgQuest
    {
        public PgQuest(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 212;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgQuest CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuest CreateNew(byte[] data, ref int offset)
        {
            return new PgQuest(data, ref offset);
        }

        public override void Init()
        {
            AddLinkBackCollection(QuestObjectiveList, (IPgQuestObjective value) => value.GetLinkBack());
            AddLinkBackCollection(RewardsXPList, (IPgQuestRewardXp value) => new List<IBackLinkable>() { value.Skill });
            AddLinkBackCollection(QuestRewardsItemList, (IPgQuestRewardItem value) => new List<IBackLinkable>() { value.QuestItem });
            AddLinkBack(FavorNpc);
            AddLinkBack(RewardAbility);
            AddLinkBack(RewardSkill);
            AddLinkBack(RewardRecipe);
            AddLinkBackCollection(PreGiveItemList, (IPgQuestRewardItem value) => new List<IBackLinkable>() { value.QuestItem });
            AddLinkBackCollection(PreGiveRecipeList);
            AddLinkBackCollection(RewardEffectList);
            AddLinkBack(RewardLoreBook);
            AddLinkBack(WorkOrderSkill);
            AddLinkBackCollection(FollowUpQuestList);
            AddLinkBackCollection(QuestRequirementList, (IPgQuestRequirement value) => value.GetLinkBack());
            AddLinkBackCollection(QuestRequirementToSustainList, (IPgQuestRequirement value) => value.GetLinkBack());
            //AddLinkBackCollection(QuestRewardList);
            AddLinkBack(RewardTitle);
            AddLinkBackCollection(QuestMidwayGiveItemList, (IPgQuestRewardItem value) => new List<IBackLinkable>() { value.QuestItem });
            AddLinkBack(QuestCompleteNpc);
            //AddLinkBackCollection(RewardsFavorList, (IPgQuestRewardFavor value) => new List<IBackLinkable>() { value.Npc });
            AddLinkBackCollection(RewardsLevelList, (IPgQuestRewardLevel value) => new List<IBackLinkable>() { value.Skill });
        }

        public override string Key { get { return GetString(0); } }
        public string InternalName { get { return GetString(4); } }
        public string Name { get { return GetString(8); } }
        public string Description { get { return GetString(12); } }
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get { return GetInt(16); } }
        public IPgQuestObjectiveCollection QuestObjectiveList { get { return GetObjectList(20, ref _QuestObjectiveList, PgQuestObjectiveCollection.CreateItem, () => new PgQuestObjectiveCollection()); } } private IPgQuestObjectiveCollection _QuestObjectiveList;
        public IPgQuestRewardXpCollection RewardsXPList { get { return GetObjectList(24, ref _RewardsXPList, PgQuestRewardXpCollection.CreateItem, () => new PgQuestRewardXpCollection()); } } private IPgQuestRewardXpCollection _RewardsXPList;
        public IPgQuestRewardItemCollection QuestRewardsItemList { get { return GetObjectList(28, ref _QuestRewardsItemList, PgQuestRewardItemCollection.CreateItem, () => new PgQuestRewardItemCollection()); } } private IPgQuestRewardItemCollection _QuestRewardsItemList;
        public TimeSpan? RawReuseTime { get { return GetTimeSpan(32); } }
        public int RewardCombatXP { get { return RawRewardCombatXP.HasValue ? RawRewardCombatXP.Value : 0; } }
        public int? RawRewardCombatXP { get { return GetInt(36); } }
        public IPgGameNpc FavorNpc { get { return GetObject(40, ref _FavorNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _FavorNpc;
        public string PrefaceText { get { return GetString(44); } }
        public string SuccessText { get { return GetString(48); } }
        public string MidwayText { get { return GetString(52); } }
        public IPgAbility RewardAbility { get { return GetObject(56, ref _RewardAbility, PgAbility.CreateNew); } } private IPgAbility _RewardAbility;
        public int RewardFavor { get { return RawRewardFavor.HasValue ? RawRewardFavor.Value : 0; } }
        public int? RawRewardFavor { get { return GetInt(60); } }
        public IPgSkill RewardSkill { get { return GetObject(64, ref _RewardSkill, PgSkill.CreateNew); } } private IPgSkill _RewardSkill;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get { return GetInt(68); } }
        public IPgRecipe RewardRecipe { get { return GetObject(72, ref _RewardRecipe, PgRecipe.CreateNew); } } private IPgRecipe _RewardRecipe;
        public int RewardGuildXp { get { return RawRewardGuildXp.HasValue ? RawRewardGuildXp.Value : 0; } }
        public int? RawRewardGuildXp { get { return GetInt(76); } }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get { return GetInt(80); } }
        public IPgQuestRewardItemCollection PreGiveItemList { get { return GetObjectList(84, ref _PreGiveItemList, PgQuestRewardItemCollection.CreateItem, () => new PgQuestRewardItemCollection()); } } private IPgQuestRewardItemCollection _PreGiveItemList;
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        public int? RawTSysLevel { get { return GetInt(88); } }
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        public int? RawRewardGold { get { return GetInt(92); } }
        public string RewardsNamedLootProfile { get { return GetString(96); } }
        public IPgRecipeCollection PreGiveRecipeList { get { return GetObjectList(100, ref _PreGiveRecipeList, PgRecipeCollection.CreateItem, () => new PgRecipeCollection()); } } private IPgRecipeCollection _PreGiveRecipeList;
        public List<QuestKeyword> KeywordList { get { return GetEnumList(104, ref _KeywordList); } } private List<QuestKeyword> _KeywordList;
        public IPgEffectCollection RewardEffectList { get { return GetObjectList(108, ref _RewardEffectList, PgEffectCollection.CreateItem, () => new PgEffectCollection()); } } private IPgEffectCollection _RewardEffectList;
        public IPgLoreBook RewardLoreBook { get { return GetObject(112, ref _RewardLoreBook, PgLoreBook.CreateNew); } } private IPgLoreBook _RewardLoreBook;
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get { return GetBool(116, 0); } }
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        public bool? RawIsAutoPreface { get { return GetBool(116, 2); } }
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        public bool? RawIsAutoWrapUp { get { return GetBool(116, 4); } }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        public bool? RawIsGuildQuest { get { return GetBool(116, 6); } }
        public bool IsUnknownWorkOrderSkill { get { return RawIsUnknownWorkOrderSkill.HasValue && RawIsUnknownWorkOrderSkill.Value; } }
        public bool? RawIsUnknownWorkOrderSkill { get { return GetBool(116, 8); } }
        public MapAreaName DisplayedLocation { get { return GetEnum<MapAreaName>(118); } }
        public Favor PrerequisiteFavorLevel { get { return GetEnum<Favor>(120); } }
        public QuestGroupingName GroupingName { get { return GetEnum<QuestGroupingName>(122); } }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get { return GetInt(124); } }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(128); } }
        public IPgSkill WorkOrderSkill { get { return GetObject(132, ref _WorkOrderSkill, PgSkill.CreateNew); } } private IPgSkill _WorkOrderSkill;
        public IPgQuestCollection FollowUpQuestList { get { return GetObjectList(136, ref _FollowUpQuestList, PgQuestCollection.CreateItem, () => new PgQuestCollection()); } } private IPgQuestCollection _FollowUpQuestList;
        public IPgQuestRequirementCollection QuestRequirementList { get { return GetObjectList(140, ref _QuestRequirementList, PgQuestRequirementCollection.CreateItem, () => new PgQuestRequirementCollection()); } } private IPgQuestRequirementCollection _QuestRequirementList;
        public IPgQuestRequirementCollection QuestRequirementToSustainList { get { return GetObjectList(144, ref _QuestRequirementToSustainList, PgQuestRequirementCollection.CreateItem, () => new PgQuestRequirementCollection()); } } private IPgQuestRequirementCollection _QuestRequirementToSustainList;
        protected override List<string> FieldTableOrder { get { return GetStringList(148, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public int? RawReuseTime_Minutes { get { return GetInt(152); } }
        public int? RawReuseTime_Hours { get { return GetInt(156); } }
        public int? RawReuseTime_Days { get { return GetInt(160); } }
        public string FavorNpcId { get { return GetString(164); } }
        public string FavorNpcName { get { return GetString(168); } }
        public MapAreaName FavorNpcArea { get { return GetEnum<MapAreaName>(172); } }
        public bool IsQuestRewardsItemListEmpty { get { return RawIsQuestRewardsItemListEmpty.HasValue && RawIsQuestRewardsItemListEmpty.Value; } }
        public bool? RawIsQuestRewardsItemListEmpty { get { return GetBool(174, 0); } }
        public bool IsEmptyNpc { get { return RawIsEmptyNpc.HasValue && RawIsEmptyNpc.Value; } }
        public bool? RawIsEmptyNpc { get { return GetBool(174, 2); } }
        public bool IsRewardRecipeEmpty { get { return RawIsRewardRecipeEmpty.HasValue && RawIsRewardRecipeEmpty.Value; } }
        public bool? RawIsRewardRecipeEmpty { get { return GetBool(174, 4); } }
        public bool IsQuestRequirementListSimple { get { return RawIsQuestRequirementListSimple.HasValue && RawIsQuestRequirementListSimple.Value; } }
        public bool? RawIsQuestRequirementListSimple { get { return GetBool(174, 6); } }
        public bool IsQuestRequirementListNested { get { return RawIsQuestRequirementListNested.HasValue && RawIsQuestRequirementListNested.Value; } }
        public bool? RawIsQuestRequirementListNested { get { return GetBool(174, 8); } }
        public bool IsLearnAbility { get { return RawIsLearnAbility.HasValue && RawIsLearnAbility.Value; } }
        public bool? RawIsLearnAbility { get { return GetBool(174, 10); } }
        public IPgQuestRewardCollection QuestRewardList { get { return GetObjectList(176, ref _QuestRewardList, PgQuestRewardCollection.CreateItem, () => new PgQuestRewardCollection()); } } private IPgQuestRewardCollection _QuestRewardList;
        public List<string> RawRewardInteractionFlags { get { return GetStringList(180, ref _RawRewardInteractionFlags); } } private List<string> _RawRewardInteractionFlags;
        public IPgPlayerTitle RewardTitle { get { return GetObject(184, ref _RewardTitle, PgPlayerTitle.CreateNew); } } private PgPlayerTitle _RewardTitle;
        public IPgQuestRewardCurrencyCollection RewardsCurrencyList { get { return GetObjectList(188, ref _RewardsCurrencyList, PgQuestRewardCurrencyCollection.CreateItem, () => new PgQuestRewardCurrencyCollection()); } } private IPgQuestRewardCurrencyCollection _RewardsCurrencyList;
        public IPgQuestRewardItemCollection QuestMidwayGiveItemList { get { return GetObjectList(192, ref _QuestMidwayGiveItemList, PgQuestRewardItemCollection.CreateItem, () => new PgQuestRewardItemCollection()); } } private IPgQuestRewardItemCollection _QuestMidwayGiveItemList;
        public IPgGameNpc QuestCompleteNpc { get { return GetObject(196, ref _QuestCompleteNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _QuestCompleteNpc;
        public string QuestCompleteNpcName { get { return GetString(200); } }
        public IPgQuestRewardFavorCollection RewardsFavorList { get { return GetObjectList(204, ref _RewardsFavorList, PgQuestRewardFavorCollection.CreateItem, () => new PgQuestRewardFavorCollection()); } } private IPgQuestRewardFavorCollection _RewardsFavorList;
        public IPgQuestRewardLevelCollection RewardsLevelList { get { return GetObjectList(208, ref _RewardsLevelList, PgQuestRewardLevelCollection.CreateItem, () => new PgQuestRewardLevelCollection()); } } private IPgQuestRewardLevelCollection _RewardsLevelList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InternalName } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Name } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Version", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawVersion } },
            { "RequirementsToSustain", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => QuestRequirementToSustainList,
                SimplifyArray = true } },
            { "ReuseTime_Minutes", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawReuseTime_Minutes } },
            { "IsCancellable", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsCancellable } },
            { "Objectives", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => QuestObjectiveList } },
            { "Rewards_XP", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetXPRewards } },
            { "Rewards_Currency", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetCurrencyRewards } },
            { "Rewards_Items", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => QuestRewardsItemList,
                GetArrayIsEmpty = () => IsQuestRewardsItemListEmpty } },
            { "ReuseTime_Days", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawReuseTime_Days } },
            { "ReuseTime_Hours", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawReuseTime_Hours } },
            { "Reward_CombatXP", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRewardCombatXP } },
            { "FavorNpc", new FieldParser() {
                Type = FieldType.String,
                GetString = () => NpcToString(FavorNpcArea, FavorNpcId, FavorNpcName, IsEmptyNpc) } },
            { "PrefaceText", new FieldParser() {
                Type = FieldType.String,
                GetString = () => PrefaceText } },
            { "SuccessText", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SuccessText } },
            { "MidwayText", new FieldParser() {
                Type = FieldType.String,
                GetString = () => MidwayText } },
            { "PrerequisiteFavorLevel", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Favor>.ToString(PrerequisiteFavorLevel, null, Favor.Internal_None) } },
            { "Rewards_Favor", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRewardFavor } },
            { "Rewards_Recipes", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = GetRewards_Recipes,
                GetArrayIsEmpty = () => IsRewardRecipeEmpty } },
            { "Rewards_Ability", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RewardAbility != null ? RewardAbility.InternalName : null } },
            { "Requirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => QuestRequirementList,
                SimplifyArray = true,
                GetArrayIsSimple = () => IsQuestRequirementListSimple,
                GetArrayIsNested = () => IsQuestRequirementListNested } },
            { "Reward_Favor", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRewardFavor } },
            { "Rewards", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => QuestRewardList } },
            { "PreGiveItems", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => PreGiveItemList } },
            { "TSysLevel", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawTSysLevel } },
            { "Reward_Gold", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRewardGold } },
            { "Rewards_NamedLootProfile", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RewardsNamedLootProfile } },
            { "PreGiveRecipes", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = GetPreGiveRecipeList } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<QuestKeyword>.ToStringList(KeywordList) } },
            { "Rewards_Effects", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = GetRewards_Effects } },
            { "IsAutoPreface", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsAutoPreface } },
            { "IsAutoWrapUp", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsAutoWrapUp } },
            { "GroupingName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestGroupingName>.ToString(GroupingName, null, QuestGroupingName.Internal_None) } },
            { "IsGuildQuest", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsGuildQuest } },
            { "NumExpectedParticipants", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumExpectedParticipants } },
            { "Level", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawLevel } },
            { "WorkOrderSkill", new FieldParser() {
                Type = FieldType.String,
                GetString = GetWorkOrderSkill } },
            { "DisplayedLocation", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<MapAreaName>.ToString(DisplayedLocation, TextMaps.MapAreaNameStringMap, MapAreaName.Internal_None) } },
            { "FollowUpQuests", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = GetFollowUpQuestList } },
            { "MidwayGiveItems", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => QuestMidwayGiveItemList } },
        }; } }

        private IObjectContentGenerator GetXPRewards()
        {
            XPReward Rewards = new XPReward();

            foreach (IPgQuestRewardXp Item in RewardsXPList)
                Rewards.SetFieldValue(Item.Skill.CombatSkill, Item.Xp);

            return Rewards;
        }

        private IObjectContentGenerator GetCurrencyRewards()
        {
            XPReward Rewards = new XPReward();

            return Rewards;
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

        private List<string> GetRewards_Recipes()
        {
            List<string> Result = new List<string>();

            if (RewardRecipe != null)
                Result.Add(RewardRecipe.InternalName);

            return Result;
        }

        private List<string> GetPreGiveRecipeList()
        {
            List<string> Result = new List<string>();
            foreach (IPgRecipe Item in PreGiveRecipeList)
                Result.Add(Item.InternalName);

            return Result;
        }

        private List<string> GetRewards_Effects()
        {
            List<string> Result = new List<string>();

            foreach (string RewardInteractionFlag in RawRewardInteractionFlags)
                Result.Add("SetInteractionFlag(" + RewardInteractionFlag + ")");

            if (RewardLoreBook != null)
                Result.Add("EnsureLoreBookKnown(" + RewardLoreBook.InternalName + ")");

            if (RewardTitle != null)
                Result.Add("BestowTitle(" + PlayerTitle.KeyToTitleMap[RewardTitle.Key] + ")");

            foreach (IPgEffect RewardEffect in RewardEffectList)
                Result.Add(RewardEffect.Name);

            return Result;
        }

        private string GetWorkOrderSkill()
        {
            if (IsUnknownWorkOrderSkill)
                return StringToEnumConversion<PowerSkill>.ToString(PowerSkill.Unknown);

            if (WorkOrderSkill != null)
                return StringToEnumConversion<PowerSkill>.ToString(WorkOrderSkill.CombatSkill, null, PowerSkill.Internal_None);

            return null;
        }

        private List<string> GetFollowUpQuestList()
        {
            List<string> Result = new List<string>();

            foreach (IPgQuest Item in FollowUpQuestList)
                Result.Add(Item.InternalName);

            return Result;
        }

        public override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return "icon_" + Quest.SearchResultIconId; } }
    }
}
