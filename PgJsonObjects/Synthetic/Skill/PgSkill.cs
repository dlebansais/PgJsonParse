using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgSkill : MainPgObject<PgSkill>, IPgSkill
    {
        public PgSkill(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 76;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgSkill CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgSkill CreateNew(byte[] data, ref int offset)
        {
            PgSkill Result = new PgSkill(data, ref offset);
            return Result;
        }

        public override void Init()
        {
            GetIntList(60, ref AdvancementHintTableKey);
            GetStringList(64, ref AdvancementHintTableValue);
            GetIntList(68, ref ReportTableKey);
            GetStringList(72, ref ReportTableValue);

            CombinedRewardList = Skill.CreateCombinedRewardList(InteractionFlagLevelCapList, AdvancementHintTableKey, AdvancementHintTableValue, RewardList, ReportTableKey, ReportTableValue);
        }

        public override string Key { get { return GetString(0); } }
        public PowerSkill CombatSkill { get { return GetEnum<PowerSkill>(4); } }
        public bool HideWhenZero { get { return RawHideWhenZero.HasValue && RawHideWhenZero.Value; } }
        public bool? RawHideWhenZero { get { return GetBool(6, 0); } }
        public bool Combat { get { return RawCombat.HasValue && RawCombat.Value; } }
        public bool? RawCombat { get { return GetBool(6, 2); } }
        public bool SkipBonusLevelsIfSkillUnlearned { get { return RawSkipBonusLevelsIfSkillUnlearned.HasValue && RawSkipBonusLevelsIfSkillUnlearned.Value; } }
        public bool? RawSkipBonusLevelsIfSkillUnlearned { get { return GetBool(6, 4); } }
        public bool AuxCombat { get { return RawAuxCombat.HasValue && RawAuxCombat.Value; } }
        public bool? RawAuxCombat { get { return GetBool(6, 6); } }
        public bool ParentSkillIsEmpty { get { return RawParentSkillIsEmpty.HasValue && RawParentSkillIsEmpty.Value; } }
        public bool? RawParentSkillIsEmpty { get { return GetBool(6, 8); } }
        public bool IsAdvancementTableNull { get { return GetBool(6, 10).Value; } }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(8); } }
        public string Description { get { return GetString(12); } }
        public IPgXpTable XpTable { get { return GetObject(16, ref _XpTable, PgXpTable.CreateNew); } } private IPgXpTable _XpTable;
        public string RawXpTable { get { return GetString(20); } }
        public IPgAdvancementTable AdvancementTable { get { return GetObject(24, ref _AdvancementTable, PgAdvancementTable.CreateNew); } } private IPgAdvancementTable _AdvancementTable;
        public List<PowerSkill> CompatibleCombatSkillList { get { return GetEnumList(28, ref _CompatibleCombatSkillList); } } private List<PowerSkill> _CompatibleCombatSkillList;
        public int MaxBonusLevels { get { return RawMaxBonusLevels.HasValue ? RawMaxBonusLevels.Value : 0; } }
        public int? RawMaxBonusLevels { get { return GetInt(32); } }
        public LevelCapInteractionCollection InteractionFlagLevelCapList { get { return GetObjectList(36, ref _InteractionFlagLevelCapList, LevelCapInteractionCollection.CreateItem, () => new LevelCapInteractionCollection()); } } private LevelCapInteractionCollection _InteractionFlagLevelCapList;
        public RewardCollection RewardList { get { return GetObjectList(40, ref _RewardList, RewardCollection.CreateItem, () => new RewardCollection()); } } private RewardCollection _RewardList;
        public string Name { get { return GetString(44); } }
        public IPgSkill ParentSkill { get { return GetObject(48, ref _ParentSkill, PgSkill.CreateNew); } } private IPgSkill _ParentSkill;
        public List<SkillCategory> TSysCategoryList { get { return GetEnumList(52, ref _TSysCategoryList); } } private List<SkillCategory> _TSysCategoryList;
        protected override List<string> FieldTableOrder { get { return GetStringList(56, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        public List<SkillRewardCommon> CombinedRewardList { get; private set; }
        private List<int> AdvancementHintTableKey = null;
        private List<string> AdvancementHintTableValue = null;
        private List<int> ReportTableKey = null;
        private List<string> ReportTableValue = null;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Id", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawId } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "HideWhenZero", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawHideWhenZero } },
            { "XpTable", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawXpTable } },
            { "AdvancementTable", new FieldParser() {
                Type = FieldType.String,
                GetString = () => IsAdvancementTableNull ? GenericJsonObject.NullString : (AdvancementTable != null ? AdvancementTable.InternalName : null)} },
            { "Combat", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawCombat } },
            { "CompatibleCombatSkills", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<PowerSkill>.ToStringList(CompatibleCombatSkillList) } },
            { "MaxBonusLevels", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMaxBonusLevels } },
            { "InteractionFlagLevelCaps", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetInteractionFlagLevelCaps } },
            { "AdvancementHints", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetAdvancementHints } },
            { "Rewards", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetRewards } },
            { "Reports", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetReports } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Name } },
            { "Parents", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = GetParents,
                GetArrayIsEmpty = () => ParentSkillIsEmpty } },
            { "SkipBonusLevelsIfSkillUnlearned", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawSkipBonusLevelsIfSkillUnlearned } },
            { "AuxCombat", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawAuxCombat } },
            { "TSysCategories", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<SkillCategory>.ToStringList(TSysCategoryList) } },
        }; } }

        private IObjectContentGenerator GetInteractionFlagLevelCaps()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("InteractionFlagLevelCaps");

            foreach (IPgLevelCapInteraction Interaction in InteractionFlagLevelCapList)
            {
                PowerSkill Skill = Interaction.Link.CombatSkill;
                string FieldKey = "LevelCap_" + StringToEnumConversion<PowerSkill>.ToString(Skill);

                switch (Skill)
                {
                    case PowerSkill.Performance_Strings:
                    case PowerSkill.Performance_Percussion:
                    case PowerSkill.Performance_Wind:
                        break;

                    default:
                        FieldKey += "_";
                        break;
                }

                FieldKey += Interaction.OtherLevel;

                int FieldValue = Interaction.Level;

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private IObjectContentGenerator GetAdvancementHints()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("AdvancementHints");

            for (int i = 0; i < AdvancementHintTableKey.Count; i++)
            {
                string FieldKey = AdvancementHintTableKey[i].ToString();
                string FieldValue = AdvancementHintTableValue[i];

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private IObjectContentGenerator GetRewards()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("Rewards");

            foreach (IPgReward Reward in RewardList)
            {
                string FieldKey = Reward.Key;
                IObjectContentGenerator FieldValue = Reward as IObjectContentGenerator;

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private IObjectContentGenerator GetReports()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("Reports");

            for (int i = 0; i < ReportTableKey.Count; i++)
            {
                string FieldKey = ReportTableKey[i].ToString();
                string FieldValue = ReportTableValue[i];

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private List<string> GetParents()
        {
            List<string> Result = new List<string>();

            if (ParentSkill != null)
                Result.Add(StringToEnumConversion<PowerSkill>.ToString(ParentSkill.CombatSkill, null, PowerSkill.Internal_None));

            return Result;
        }
    }
}
