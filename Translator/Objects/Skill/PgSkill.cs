namespace PgObjects
{
    using System.Collections.Generic;

    public class PgSkill : PgObject
    {
        public static PgSkill Unknown { get; } = new PgSkill() { Name = "Unknown" };
        public static PgSkill AnySkill { get; } = new PgSkill() { Name = "Any Skill", Key = "AnySkill" };

        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int BoolValues { get; set; }
        public const int HideWhenZeroNotNull = 1 << 0;
        public const int HideWhenZeroIsTrue = 1 << 1;
        public bool HideWhenZero { get { return (BoolValues & (HideWhenZeroNotNull + HideWhenZeroIsTrue)) == (HideWhenZeroNotNull + HideWhenZeroIsTrue); } }
        public bool? RawHideWhenZero { get { return ((BoolValues & HideWhenZeroNotNull) != 0) ? (BoolValues & HideWhenZeroIsTrue) != 0 : null; } }
        public void SetHideWhenZero(bool value) { BoolValues |= (BoolValues & ~(HideWhenZeroNotNull + HideWhenZeroIsTrue)) | ((value ? HideWhenZeroIsTrue : 0) + HideWhenZeroNotNull); }
        public PgXpTable XpTable { get; set; } = null!;
        public const int IsCombatSkillNotNull = 1 << 2;
        public const int IsCombatSkillIsTrue = 1 << 3;
        public bool IsCombatSkill { get { return (BoolValues & (IsCombatSkillNotNull + IsCombatSkillIsTrue)) == (IsCombatSkillNotNull + IsCombatSkillIsTrue); } }
        public bool? RawIsCombatSkill { get { return ((BoolValues & IsCombatSkillNotNull) != 0) ? (BoolValues & IsCombatSkillIsTrue) != 0 : null; } }
        public void SetIsCombatSkill(bool value) { BoolValues |= (BoolValues & ~(IsCombatSkillNotNull + IsCombatSkillIsTrue)) | ((value ? IsCombatSkillIsTrue : 0) + IsCombatSkillNotNull); }
        public PgSkillCollection CompatibleCombatSkillList { get; set; } = new PgSkillCollection();
        public int MaxBonusLevels { get { return RawMaxBonusLevels.HasValue ? RawMaxBonusLevels.Value : 0; } }
        public int? RawMaxBonusLevels { get; set; }
        public PgSkillAdvancementCollection SkillAdvancementList { get; set; } = new PgSkillAdvancementCollection();
        public PgReportCollection ReportList { get; set; } = new PgReportCollection();
        public string Name { get; set; } = string.Empty;
        public PgSkillCollection ParentSkillList { get; set; } = new PgSkillCollection();
        public const int SkipBonusLevelsIfSkillUnlearnedNotNull = 1 << 4;
        public const int SkipBonusLevelsIfSkillUnlearnedIsTrue = 1 << 5;
        public bool SkipBonusLevelsIfSkillUnlearned { get { return (BoolValues & (SkipBonusLevelsIfSkillUnlearnedNotNull + SkipBonusLevelsIfSkillUnlearnedIsTrue)) == (SkipBonusLevelsIfSkillUnlearnedNotNull + SkipBonusLevelsIfSkillUnlearnedIsTrue); } }
        public bool? RawSkipBonusLevelsIfSkillUnlearned { get { return ((BoolValues & SkipBonusLevelsIfSkillUnlearnedNotNull) != 0) ? (BoolValues & SkipBonusLevelsIfSkillUnlearnedIsTrue) != 0 : null; } }
        public void SetSkipBonusLevelsIfSkillUnlearned(bool value) { BoolValues |= (BoolValues & ~(SkipBonusLevelsIfSkillUnlearnedNotNull + SkipBonusLevelsIfSkillUnlearnedIsTrue)) | ((value ? SkipBonusLevelsIfSkillUnlearnedIsTrue : 0) + SkipBonusLevelsIfSkillUnlearnedNotNull); }
        public const int AuxCombatNotNull = 1 << 6;
        public const int AuxCombatIsTrue = 1 << 7;
        public bool AuxCombat { get { return (BoolValues & (AuxCombatNotNull + AuxCombatIsTrue)) == (AuxCombatNotNull + AuxCombatIsTrue); } }
        public bool? RawAuxCombat { get { return ((BoolValues & AuxCombatNotNull) != 0) ? (BoolValues & AuxCombatIsTrue) != 0 : null; } }
        public void SetAuxCombat(bool value) { BoolValues |= (BoolValues & ~(AuxCombatNotNull + AuxCombatIsTrue)) | ((value ? AuxCombatIsTrue : 0) + AuxCombatNotNull); }
        public List<ItemKeyword> RecipeIngredientKeywordList { get; set; } = new List<ItemKeyword>();
        public int GuestLevelCap { get { return RawGuestLevelCap.HasValue ? RawGuestLevelCap.Value : 0; } }
        public int? RawGuestLevelCap { get; set; }
        public const int IsFakeCombatSkillNotNull = 1 << 8;
        public const int IsFakeCombatSkillIsTrue = 1 << 9;
        public bool IsFakeCombatSkill { get { return (BoolValues & (IsFakeCombatSkillNotNull + IsFakeCombatSkillIsTrue)) == (IsFakeCombatSkillNotNull + IsFakeCombatSkillIsTrue); } }
        public bool? RawIsFakeCombatSkill { get { return ((BoolValues & IsFakeCombatSkillNotNull) != 0) ? (BoolValues & IsFakeCombatSkillIsTrue) != 0 : null; } }
        public void SetIsFakeCombatSkill(bool value) { BoolValues |= (BoolValues & ~(IsFakeCombatSkillNotNull + IsFakeCombatSkillIsTrue)) | ((value ? IsFakeCombatSkillIsTrue : 0) + IsFakeCombatSkillNotNull); }
        public const int IsUmbrellaSkillNotNull = 1 << 10;
        public const int IsUmbrellaSkillIsTrue = 1 << 11;
        public bool IsUmbrellaSkill { get { return (BoolValues & (IsUmbrellaSkillNotNull + IsUmbrellaSkillIsTrue)) == (IsUmbrellaSkillNotNull + IsUmbrellaSkillIsTrue); } }
        public bool? RawIsUmbrellaSkill { get { return ((BoolValues & IsUmbrellaSkillNotNull) != 0) ? (BoolValues & IsUmbrellaSkillIsTrue) != 0 : null; } }
        public void SetIsUmbrellaSkill(bool value) { BoolValues |= (BoolValues & ~(IsUmbrellaSkillNotNull + IsUmbrellaSkillIsTrue)) | ((value ? IsUmbrellaSkillIsTrue : 0) + IsUmbrellaSkillNotNull); }
        public const int SkillLevelDisparityAppliesNotNull = 1 << 12;
        public const int SkillLevelDisparityAppliesIsTrue = 1 << 13;
        public bool SkillLevelDisparityApplies { get { return (BoolValues & (SkillLevelDisparityAppliesNotNull + SkillLevelDisparityAppliesIsTrue)) == (SkillLevelDisparityAppliesNotNull + SkillLevelDisparityAppliesIsTrue); } }
        public bool? RawSkillLevelDisparityApplies { get { return ((BoolValues & SkillLevelDisparityAppliesNotNull) != 0) ? (BoolValues & SkillLevelDisparityAppliesIsTrue) != 0 : null; } }
        public void SetSkillLevelDisparityApplies(bool value) { BoolValues |= (BoolValues & ~(SkillLevelDisparityAppliesNotNull + SkillLevelDisparityAppliesIsTrue)) | ((value ? SkillLevelDisparityAppliesIsTrue : 0) + SkillLevelDisparityAppliesNotNull); }

        public Dictionary<ItemSlot, List<string>> AssociationTablePower { get; set; } = new Dictionary<ItemSlot, List<string>>();
        public List<string> AssociationListAbility { get; set; } = new List<string>();

        public int IconId { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name.Length > 0 ? Name : Key; } }
        public override string ToString() { return Name.Length > 0 ? Name : Key; }
    }
}
