namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgSkill
    {
        public PowerSkill CombatSkill { get; set; }
        public bool HideWhenZero { get { return RawHideWhenZero.HasValue && RawHideWhenZero.Value; } }
        public bool? RawHideWhenZero { get; set; }
        public bool Combat { get { return RawCombat.HasValue && RawCombat.Value; } }
        public bool? RawCombat { get; set; }
        public bool SkipBonusLevelsIfSkillUnlearned { get { return RawSkipBonusLevelsIfSkillUnlearned.HasValue && RawSkipBonusLevelsIfSkillUnlearned.Value; } }
        public bool? RawSkipBonusLevelsIfSkillUnlearned { get; set; }
        public bool AuxCombat { get { return RawAuxCombat.HasValue && RawAuxCombat.Value; } }
        public bool? RawAuxCombat { get; set; }
        public bool ParentSkillIsEmpty { get { return RawParentSkillIsEmpty.HasValue && RawParentSkillIsEmpty.Value; } }
        public bool? RawParentSkillIsEmpty { get; set; }
        public bool IsFakeCombatSkill { get { return RawIsFakeCombatSkill.HasValue && RawIsFakeCombatSkill.Value; } }
        public bool? RawIsFakeCombatSkill { get; set; }
        public bool IsAdvancementTableNull { get; set; }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; set; }
        public string Description { get; set; }
        public PgXpTable XpTable { get; set; }
        public string RawXpTable { get; set; }
        public PgAdvancementTable AdvancementTable { get; set; }
        public List<PowerSkill> CompatibleCombatSkillList { get; } = new List<PowerSkill>();
        public int MaxBonusLevels { get { return RawMaxBonusLevels.HasValue ? RawMaxBonusLevels.Value : 0; } }
        public int? RawMaxBonusLevels { get; set; }
        public PgLevelCapInteractionCollection InteractionFlagLevelCapList { get; } = new PgLevelCapInteractionCollection();
        public PgRewardCollection RewardList { get; } = new PgRewardCollection();
        public string Name { get; set; }
        public PgSkill ParentSkill { get; set; }
        public List<SkillCategory> TSysCategoryList { get; } = new List<SkillCategory>();
        public List<ItemKeyword> RecipeIngredientKeywordList { get; } = new List<ItemKeyword>();
        public int GuestLevelCap { get { return RawGuestLevelCap.HasValue ? RawGuestLevelCap.Value : 0; } }
        public int? RawGuestLevelCap { get; set; }
    }
}
