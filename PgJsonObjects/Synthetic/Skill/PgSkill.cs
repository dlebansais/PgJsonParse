using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgSkill : MainPgObject<PgSkill>, IPgSkill
    {
        public PgSkill(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 68;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgSkill CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgSkill CreateNew(byte[] data, ref int offset)
        {
            return new PgSkill(data, ref offset);
        }

        public override void Init()
        {
            List<int> AdvancementHintTableKey = null;
            GetIntList(52, ref AdvancementHintTableKey);
            List<string> AdvancementHintTableValue = null;
            GetStringList(56, ref AdvancementHintTableValue);
            List<int> ReportTableKey = null;
            GetIntList(60, ref ReportTableKey);
            List<string> ReportTableValue = null;
            GetStringList(64, ref ReportTableValue);

            CombinedRewardList = Skill.CreateCombinedRewardList(InteractionFlagLevelCapList, AdvancementHintTableKey, AdvancementHintTableValue, RewardList, ReportTableKey, ReportTableValue);
        }

        public PowerSkill CombatSkill { get { return GetEnum<PowerSkill>(0); } }
        public bool HideWhenZero { get { return RawHideWhenZero.HasValue && RawHideWhenZero.Value; } }
        public bool? RawHideWhenZero { get { return GetBool(2, 0); } }
        public bool Combat { get { return RawCombat.HasValue && RawCombat.Value; } }
        public bool? RawCombat { get { return GetBool(2, 2); } }
        public bool SkipBonusLevelsIfSkillUnlearned { get { return RawSkipBonusLevelsIfSkillUnlearned.HasValue && RawSkipBonusLevelsIfSkillUnlearned.Value; } }
        public bool? RawSkipBonusLevelsIfSkillUnlearned { get { return GetBool(2, 4); } }
        public bool AuxCombat { get { return RawAuxCombat.HasValue && RawAuxCombat.Value; } }
        public bool? RawAuxCombat { get { return GetBool(2, 8); } }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(4); } }
        public string Description { get { return GetString(8); } }
        public IPgXpTable XpTable { get { return GetObject(12, ref _XpTable, PgXpTable.CreateNew); } } private IPgXpTable _XpTable;
        public string RawXpTable { get { return GetString(16); } }
        public IPgAdvancementTable AdvancementTable { get { return GetObject(20, ref _AdvancementTable, PgAdvancementTable.CreateNew); } } private IPgAdvancementTable _AdvancementTable;
        public List<PowerSkill> CompatibleCombatSkillList { get { return GetEnumList(24, ref _CompatibleCombatSkillList); } } private List<PowerSkill> _CompatibleCombatSkillList;
        public int MaxBonusLevels { get { return RawMaxBonusLevels.HasValue ? RawMaxBonusLevels.Value : 0; } }
        public int? RawMaxBonusLevels { get { return GetInt(28); } }
        public LevelCapInteractionCollection InteractionFlagLevelCapList { get { return GetObjectList(32, ref _InteractionFlagLevelCapList, LevelCapInteractionCollection.CreateItem, () => new LevelCapInteractionCollection()); } } private LevelCapInteractionCollection _InteractionFlagLevelCapList;
        public RewardCollection RewardList { get { return GetObjectList(36, ref _RewardList, RewardCollection.CreateItem, () => new RewardCollection()); } } private RewardCollection _RewardList;
        public string Name { get { return GetString(40); } }
        public IPgSkill ParentSkill { get { return GetObject(44, ref _ParentSkill, PgSkill.CreateNew); } } private IPgSkill _ParentSkill;
        public List<SkillCategory> TSysCategoryList { get { return GetEnumList(48, ref _TSysCategoryList); } } private List<SkillCategory> _TSysCategoryList;
        public List<SkillRewardCommon> CombinedRewardList { get; private set; }
    }
}
