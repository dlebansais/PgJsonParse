namespace PgObjects
{
    using System;
    using System.Collections.Generic;

    public class PgQuest : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public string InternalName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Version { get { return RawVersion.HasValue ? RawVersion.Value : 0; } }
        public int? RawVersion { get; set; }
        public PgQuestRequirementCollection QuestRequirementToSustainList { get; set; } = new PgQuestRequirementCollection();
        public TimeSpan ReuseTime { get { return RawReuseTime.HasValue ? RawReuseTime.Value : TimeSpan.Zero; } }
        public TimeSpan? RawReuseTime { get; set; }
        public bool IsCancellable { get { return RawIsCancellable.HasValue && RawIsCancellable.Value; } }
        public bool? RawIsCancellable { get; set; }
        public PgQuestObjectiveCollection QuestObjectiveList { get; set; } = new PgQuestObjectiveCollection();
        public PgNpcLocation FavorNpc { get; set; }
        public string PrefaceText { get; set; } = string.Empty;
        public string SuccessText { get; set; } = string.Empty;
        public string MidwayText { get; set; } = string.Empty;
        public Favor PrerequisiteFavorLevel { get; set; }
        public PgQuestRequirementCollection QuestRequirementList { get; set; } = new PgQuestRequirementCollection();
        public PgQuestRewardCollection QuestRewardList { get; set; } = new PgQuestRewardCollection();
        public PgQuestRewardCollection PreGiveItemList { get; set; } = new PgQuestRewardCollection();
        public PgRecipeCollection PreGiveRecipeList { get; set; } = new PgRecipeCollection();
        public List<QuestKeyword> KeywordList { get; set; } = new List<QuestKeyword>();
        public PgNpcLocation QuestCompleteNpc { get; set; }
        public PgQuestCollection FollowUpQuestList { get; set; } = new PgQuestCollection();
        public PgQuestPreGiveEffectCollection PreGiveEffectList { get; set; } = new PgQuestPreGiveEffectCollection();
        public bool IsAutoPreface { get { return RawIsAutoPreface.HasValue && RawIsAutoPreface.Value; } }
        public bool? RawIsAutoPreface { get; set; }
        public bool IsAutoWrapUp { get { return RawIsAutoWrapUp.HasValue && RawIsAutoWrapUp.Value; } }
        public bool? RawIsAutoWrapUp { get; set; }
        public QuestGroupingName GroupingName { get; set; }
        public bool IsGuildQuest { get { return RawIsGuildQuest.HasValue && RawIsGuildQuest.Value; } }
        public bool? RawIsGuildQuest { get; set; }
        public int NumExpectedParticipants { get { return RawNumExpectedParticipants.HasValue ? RawNumExpectedParticipants.Value : 0; } }
        public int? RawNumExpectedParticipants { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public PgSkill WorkOrderSkill { get; set; }
        public MapAreaName DisplayedLocation { get; set; }
        public PgQuestRewardCollection QuestMidwayGiveItemList { get; set; } = new PgQuestRewardCollection();

        public int IconId { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name; } }
    }
}
