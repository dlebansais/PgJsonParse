using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgSkill : IJsonKey, IObjectContentGenerator, IBackLinkable
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
        bool ParentSkillIsEmpty { get; }
        bool? RawParentSkillIsEmpty { get; }
        bool IsAdvancementTableNull { get; }
        int Id { get; }
        int? RawId { get; }
        string Description { get; }
        IPgXpTable XpTable { get; }
        string RawXpTable { get; }
        IPgAdvancementTable AdvancementTable { get; }
        List<PowerSkill> CompatibleCombatSkillList { get; }
        int MaxBonusLevels { get; }
        int? RawMaxBonusLevels { get; }
        IPgLevelCapInteractionCollection InteractionFlagLevelCapList { get; }
        IPgRewardCollection RewardList { get; }
        string Name { get; }
        IPgSkill ParentSkill { get; }
        List<SkillCategory> TSysCategoryList { get; }
        List<SkillRewardCommon> CombinedRewardList { get; }
        int IconId { get; }
    }
}
