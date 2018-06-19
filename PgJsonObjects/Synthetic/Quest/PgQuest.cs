using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuest: MainPgObject<PgQuest>, IPgQuest
    {
        public PgQuest(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuest CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuest CreateNew(byte[] data, ref int offset)
        {
            return new PgQuest(data, ref offset);
        }

        public string InternalName { get { return GetString(0); } }
        public string Name { get { return GetString(4); } }
        public string Description { get { return GetString(8); } }
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get { return GetInt(12); } }
        public QuestObjectiveCollection QuestObjectiveList { get { return GetObjectList(16, ref _QuestObjectiveList, QuestObjectiveCollection.CreateItem, () => new QuestObjectiveCollection()); } } private QuestObjectiveCollection _QuestObjectiveList;
        public QuestRewardXpCollection RewardsXPList { get { return GetObjectList(20, ref _RewardsXPList, QuestRewardXpCollection.CreateItem, () => new QuestRewardXpCollection()); } } private QuestRewardXpCollection _RewardsXPList;
        public QuestRewardItemCollection QuestRewardsItemList { get { return GetObjectList(24, ref _QuestRewardsItemList, QuestRewardItemCollection.CreateItem, () => new QuestRewardItemCollection()); } } private QuestRewardItemCollection _QuestRewardsItemList;
        public TimeSpan? RawReuseTime { get { return GetTimeSpan(28); } }
        public int RewardCombatXP { get { return RawRewardCombatXP.HasValue ? RawRewardCombatXP.Value : 0; } }
        public int? RawRewardCombatXP { get { return GetInt(32); } }
        public IPgGameNpc FavorNpc { get { return GetObject(36, ref _FavorNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _FavorNpc;
        public string PrefaceText { get { return GetString(40); } }
        public string SuccessText { get { return GetString(44); } }
        public string MidwayText { get { return GetString(48); } }
        public IPgAbility RewardAbility { get { return GetObject(52, ref _RewardAbility, PgAbility.CreateNew); } } private IPgAbility _RewardAbility;
        public int RewardFavor { get { return RawRewardFavor.HasValue ? RawRewardFavor.Value : 0; } }
        public int? RawRewardFavor { get { return GetInt(56); } }
        public IPgSkill RewardSkill { get { return GetObject(60, ref _RewardSkill, PgSkill.CreateNew); } } private IPgSkill _RewardSkill;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get { return GetInt(64); } }
        public IPgRecipe RewardRecipe { get { return GetObject(68, ref _RewardRecipe, PgRecipe.CreateNew); } } private IPgRecipe _RewardRecipe;
        public int RewardGuildXp { get { return RawRewardGuildXp.HasValue ? RawRewardGuildXp.Value : 0; } }
        public int? RawRewardGuildXp { get { return GetInt(72); } }
        public int RewardGuildCredits { get { return RawRewardGuildCredits.HasValue ? RawRewardGuildCredits.Value : 0; } }
        public int? RawRewardGuildCredits { get { return GetInt(76); } }
        public QuestRewardItemCollection PreGiveItemList { get { return GetObjectList(80, ref _PreGiveItemList, QuestRewardItemCollection.CreateItem, () => new QuestRewardItemCollection()); } } private QuestRewardItemCollection _PreGiveItemList;
        public int TSysLevel { get { return RawTSysLevel.HasValue ? RawTSysLevel.Value : 0; } }
        public int? RawTSysLevel { get { return GetInt(84); } }
        public int RewardGold { get { return RawRewardGold.HasValue ? RawRewardGold.Value : 0; } }
        public int? RawRewardGold { get { return GetInt(88); } }
        public string RewardsNamedLootProfile { get { return GetString(92); } }
        public RecipeCollection PreGiveRecipeList { get { return GetObjectList(96, ref _PreGiveRecipeList, RecipeCollection.CreateItem, () => new RecipeCollection()); } } private RecipeCollection _PreGiveRecipeList;
        public List<QuestKeyword> KeywordList { get { return GetEnumList(100, ref _KeywordList); } } private List<QuestKeyword> _KeywordList;
        public IPgEffect RewardEffect { get { return GetObject(104, ref _RewardEffect, PgEffect.CreateNew); } } private IPgEffect _RewardEffect;
        public IPgLoreBook RewardLoreBook { get { return GetObject(108, ref _RewardLoreBook, PgLoreBook.CreateNew); } } private IPgLoreBook _RewardLoreBook;
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get { return GetBool(112, 0); } }
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        public bool? RawIsAutoPreface { get { return GetBool(112, 2); } }
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        public bool? RawIsAutoWrapUp { get { return GetBool(112, 4); } }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        public bool? RawIsGuildQuest { get { return GetBool(112, 6); } }
        public MapAreaName DisplayedLocation { get { return GetEnum<MapAreaName>(114); } }
        public Favor PrerequisiteFavorLevel { get { return GetEnum<Favor>(116); } }
        public QuestGroupingName GroupingName { get { return GetEnum<QuestGroupingName>(118); } }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get { return GetInt(120); } }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(124); } }
        public IPgSkill WorkOrderSkill { get { return GetObject(128, ref _WorkOrderSkill, PgSkill.CreateNew); } } private IPgSkill _WorkOrderSkill;
        public QuestCollection FollowUpQuestList { get { return GetObjectList(132, ref _FollowUpQuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _FollowUpQuestList;
        public QuestRequirementCollection QuestRequirementList { get { return GetObjectList(136, ref _QuestRequirementList, QuestRequirementCollection.CreateItem, () => new QuestRequirementCollection()); } } private QuestRequirementCollection _QuestRequirementList;
        public QuestRequirementCollection QuestRequirementToSustainList { get { return GetObjectList(140, ref _QuestRequirementToSustainList, QuestRequirementCollection.CreateItem, () => new QuestRequirementCollection()); } } private QuestRequirementCollection _QuestRequirementToSustainList;
    }
}
