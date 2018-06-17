using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgSkill
    {
        PowerSkill CombatSkill { get; }
        bool HideWhenZero { get; }
        bool? RawHideWhenZero { get; }
        bool Combat { get; }
        bool? RawCombat { get; }
        bool SkipBonusLevelsIfSkillUnlearned { get; }
        bool? RawSkipBonusLevelsIfSkillUnlearned { get; }
        bool AuxCombat { get; }
        bool? RawAuxCombat { get; }
        int Id { get; }
        int? RawId { get; }
        string Description { get; }
        XpTable XpTable { get; }
        string RawXpTable { get; }
        AdvancementTable AdvancementTable { get; }
        List<PowerSkill> CompatibleCombatSkillList { get; }
        int MaxBonusLevels { get; }
        int? RawMaxBonusLevels { get; }
        LevelCapInteractionCollection InteractionFlagLevelCapList { get; }
        RewardCollection RewardList { get; }
        string Name { get; }
        Skill ParentSkill { get; }
        List<SkillCategory> TSysCategoryList { get; }
        List<SkillRewardCommon> CombinedRewardList { get; }
    }
}
