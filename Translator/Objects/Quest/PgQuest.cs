namespace PgObjects
{
    using System;
    using System.Collections.Generic;

    public class PgQuest : PgObject
    {
        public string InternalName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BoolValues { get; set; }
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get; set; }
        public PgQuestRequirementCollection QuestRequirementToSustainList { get; set; } = new PgQuestRequirementCollection();
        public TimeSpan ReuseTime { get { return RawReuseTime.HasValue ? RawReuseTime.Value : TimeSpan.Zero; } }
        public TimeSpan? RawReuseTime { get; set; }
        public const int IsCancellableNotNull = 1 << 0;
        public const int IsCancellableIsTrue = 1 << 1;
        public bool IsCancellable { get { return (BoolValues & (IsCancellableNotNull + IsCancellableIsTrue)) == (IsCancellableNotNull + IsCancellableIsTrue); } }
        public bool? RawIsCancellable { get { return ((BoolValues & IsCancellableNotNull) != 0) ? (BoolValues & IsCancellableIsTrue) != 0 : null; } }
        public void SetIsCancellable(bool value) { BoolValues |= (BoolValues & ~(IsCancellableNotNull + IsCancellableIsTrue)) | ((value ? IsCancellableIsTrue : 0) + IsCancellableNotNull); }
        public PgQuestObjectiveCollection QuestObjectiveList { get; set; } = new PgQuestObjectiveCollection();
        public PgNpcLocation? FavorNpc { get; set; }
        public string PrefaceText { get; set; } = string.Empty;
        public string SuccessText { get; set; } = string.Empty;
        public string MidwayText { get; set; } = string.Empty;
        public Favor PrerequisiteFavorLevel { get; set; }
        public PgQuestRequirementCollection QuestRequirementList { get; set; } = new PgQuestRequirementCollection();
        public PgQuestRewardCollection QuestRewardList { get; set; } = new PgQuestRewardCollection();
        public PgQuestRewardCollection PreGiveItemList { get; set; } = new PgQuestRewardCollection();
        public PgRecipeCollection PreGiveRecipeList { get; set; } = new PgRecipeCollection();
        public List<QuestKeyword> KeywordList { get; set; } = new List<QuestKeyword>();
        public PgNpcLocation? QuestCompleteNpc { get; set; }
        public PgQuestCollection FollowUpQuestList { get; set; } = new PgQuestCollection();
        public PgQuestPreGiveEffectCollection PreGiveEffectList { get; set; } = new PgQuestPreGiveEffectCollection();
        public const int IsAutoPrefaceNotNull = 1 << 2;
        public const int IsAutoPrefaceIsTrue = 1 << 3;
        public bool IsAutoPreface { get { return (BoolValues & (IsAutoPrefaceNotNull + IsAutoPrefaceIsTrue)) == (IsAutoPrefaceNotNull + IsAutoPrefaceIsTrue); } }
        public bool? RawIsAutoPreface { get { return ((BoolValues & IsAutoPrefaceNotNull) != 0) ? (BoolValues & IsAutoPrefaceIsTrue) != 0 : null; } }
        public void SetIsAutoPreface(bool value) { BoolValues |= (BoolValues & ~(IsAutoPrefaceNotNull + IsAutoPrefaceIsTrue)) | ((value ? IsAutoPrefaceIsTrue : 0) + IsAutoPrefaceNotNull); }
        public const int IsAutoWrapUpNotNull = 1 << 4;
        public const int IsAutoWrapUpIsTrue = 1 << 5;
        public bool IsAutoWrapUp { get { return (BoolValues & (IsAutoWrapUpNotNull + IsAutoWrapUpIsTrue)) == (IsAutoWrapUpNotNull + IsAutoWrapUpIsTrue); } }
        public bool? RawIsAutoWrapUp { get { return ((BoolValues & IsAutoWrapUpNotNull) != 0) ? (BoolValues & IsAutoWrapUpIsTrue) != 0 : null; } }
        public void SetIsAutoWrapUp(bool value) { BoolValues |= (BoolValues & ~(IsAutoWrapUpNotNull + IsAutoWrapUpIsTrue)) | ((value ? IsAutoWrapUpIsTrue : 0) + IsAutoWrapUpNotNull); }
        public QuestGroupingName GroupingName { get; set; }
        public const int IsGuildQuestNotNull = 1 << 6;
        public const int IsGuildQuestIsTrue = 1 << 7;
        public bool IsGuildQuest { get { return (BoolValues & (IsGuildQuestNotNull + IsGuildQuestIsTrue)) == (IsGuildQuestNotNull + IsGuildQuestIsTrue); } }
        public bool? RawIsGuildQuest { get { return ((BoolValues & IsGuildQuestNotNull) != 0) ? (BoolValues & IsGuildQuestIsTrue) != 0 : null; } }
        public void SetIsGuildQuest(bool value) { BoolValues |= (BoolValues & ~(IsGuildQuestNotNull + IsGuildQuestIsTrue)) | ((value ? IsGuildQuestIsTrue : 0) + IsGuildQuestNotNull); }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public string? WorkOrderSkill_Key { get; set; }
        public MapAreaName DisplayedLocation { get; set; }
        public PgQuestRewardCollection QuestMidwayGiveItemList { get; set; } = new PgQuestRewardCollection();

        public int IconId { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name.Length > 0 ? Name : Key; } }
        public override string ToString() { return Name; }
    }
}
