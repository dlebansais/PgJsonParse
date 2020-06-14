namespace PgObjects
{
    using System.Collections.Generic;

    public class PgSkill
    {
        public static PgSkill Unknown { get; } = new PgSkill();
        public static PgSkill AnySkill { get; } = new PgSkill();

        public string Key { get; set; } = string.Empty;
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool HideWhenZero { get { return RawHideWhenZero.HasValue && RawHideWhenZero.Value; } }
        public bool? RawHideWhenZero { get; set; }
        public PgXpTable XpTable { get; set; }
        public PgAdvancementTable AdvancementTable { get; set; }
        public bool IsCombatSkill { get { return RawIsCombatSkill.HasValue && RawIsCombatSkill.Value; } }
        public bool? RawIsCombatSkill { get; set; }
        public PgSkillCollection CompatibleCombatSkillList { get; set; } = new PgSkillCollection();
        public int MaxBonusLevels { get { return RawMaxBonusLevels.HasValue ? RawMaxBonusLevels.Value : 0; } }
        public int? RawMaxBonusLevels { get; set; }
        public PgLevelCapInteractionList InteractionFlagLevelCapList { get; set; }
        public PgAdvancementHintCollection AdvancementHintList { get; set; } = new PgAdvancementHintCollection();
        public PgRewardList RewardList { get; set; }
        public PgReportList ReportList { get; set; }
        public string Name { get; set; } = string.Empty;
        public PgSkillCollection ParentSkillList { get; set; } = new PgSkillCollection();
        public bool SkipBonusLevelsIfSkillUnlearned { get { return RawSkipBonusLevelsIfSkillUnlearned.HasValue && RawSkipBonusLevelsIfSkillUnlearned.Value; } }
        public bool? RawSkipBonusLevelsIfSkillUnlearned { get; set; }
        public bool AuxCombat { get { return RawAuxCombat.HasValue && RawAuxCombat.Value; } }
        public bool? RawAuxCombat { get; set; }
        public List<ItemKeyword> RecipeIngredientKeywordList { get; set; } = new List<ItemKeyword>();
        public int GuestLevelCap { get { return RawGuestLevelCap.HasValue ? RawGuestLevelCap.Value : 0; } }
        public int? RawGuestLevelCap { get; set; }
        public bool IsFakeCombatSkill { get { return RawIsFakeCombatSkill.HasValue && RawIsFakeCombatSkill.Value; } }
        public bool? RawIsFakeCombatSkill { get; set; }

        public Dictionary<ItemSlot, List<string>> AssociationTablePower { get; set; } = new Dictionary<ItemSlot, List<string>>();
        public List<string> AssociationListAbility { get; set; } = new List<string>();
    }
}
