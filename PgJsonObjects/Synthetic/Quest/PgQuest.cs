using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuest: MainPgObject<PgQuest>, IPgQuest
    {
        public PgQuest(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 148;
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

        public override string Key { get { return GetString(0); } }
        public string InternalName { get { return GetString(4); } }
        public string Name { get { return GetString(8); } }
        public string Description { get { return GetString(12); } }
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get { return GetInt(16); } }
        public QuestObjectiveCollection QuestObjectiveList { get { return GetObjectList(20, ref _QuestObjectiveList, QuestObjectiveCollection.CreateItem, () => new QuestObjectiveCollection()); } } private QuestObjectiveCollection _QuestObjectiveList;
        public QuestRewardXpCollection RewardsXPList { get { return GetObjectList(24, ref _RewardsXPList, QuestRewardXpCollection.CreateItem, () => new QuestRewardXpCollection()); } } private QuestRewardXpCollection _RewardsXPList;
        public QuestRewardItemCollection QuestRewardsItemList { get { return GetObjectList(28, ref _QuestRewardsItemList, QuestRewardItemCollection.CreateItem, () => new QuestRewardItemCollection()); } } private QuestRewardItemCollection _QuestRewardsItemList;
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
        public QuestRewardItemCollection PreGiveItemList { get { return GetObjectList(84, ref _PreGiveItemList, QuestRewardItemCollection.CreateItem, () => new QuestRewardItemCollection()); } } private QuestRewardItemCollection _PreGiveItemList;
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        public int? RawTSysLevel { get { return GetInt(88); } }
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        public int? RawRewardGold { get { return GetInt(92); } }
        public string RewardsNamedLootProfile { get { return GetString(96); } }
        public RecipeCollection PreGiveRecipeList { get { return GetObjectList(100, ref _PreGiveRecipeList, RecipeCollection.CreateItem, () => new RecipeCollection()); } } private RecipeCollection _PreGiveRecipeList;
        public List<QuestKeyword> KeywordList { get { return GetEnumList(104, ref _KeywordList); } } private List<QuestKeyword> _KeywordList;
        public IPgEffect RewardEffect { get { return GetObject(108, ref _RewardEffect, PgEffect.CreateNew); } } private IPgEffect _RewardEffect;
        public IPgLoreBook RewardLoreBook { get { return GetObject(112, ref _RewardLoreBook, PgLoreBook.CreateNew); } } private IPgLoreBook _RewardLoreBook;
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get { return GetBool(116, 0); } }
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        public bool? RawIsAutoPreface { get { return GetBool(116, 2); } }
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        public bool? RawIsAutoWrapUp { get { return GetBool(116, 4); } }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        public bool? RawIsGuildQuest { get { return GetBool(116, 6); } }
        public MapAreaName DisplayedLocation { get { return GetEnum<MapAreaName>(118); } }
        public Favor PrerequisiteFavorLevel { get { return GetEnum<Favor>(120); } }
        public QuestGroupingName GroupingName { get { return GetEnum<QuestGroupingName>(122); } }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get { return GetInt(124); } }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(128); } }
        public IPgSkill WorkOrderSkill { get { return GetObject(132, ref _WorkOrderSkill, PgSkill.CreateNew); } } private IPgSkill _WorkOrderSkill;
        public QuestCollection FollowUpQuestList { get { return GetObjectList(136, ref _FollowUpQuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _FollowUpQuestList;
        public QuestRequirementCollection QuestRequirementList { get { return GetObjectList(140, ref _QuestRequirementList, QuestRequirementCollection.CreateItem, () => new QuestRequirementCollection()); } } private QuestRequirementCollection _QuestRequirementList;
        public QuestRequirementCollection QuestRequirementToSustainList { get { return GetObjectList(144, ref _QuestRequirementToSustainList, QuestRequirementCollection.CreateItem, () => new QuestRequirementCollection()); } } private QuestRequirementCollection _QuestRequirementToSustainList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
