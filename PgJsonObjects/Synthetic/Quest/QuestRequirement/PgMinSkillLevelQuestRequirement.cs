using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgMinSkillLevelQuestRequirement : PgQuestRequirement<PgMinSkillLevelQuestRequirement>, IPgMinSkillLevelQuestRequirement
    {
        public PgMinSkillLevelQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgMinSkillLevelQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgMinSkillLevelQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgMinSkillLevelQuestRequirement(data, ref offset);
        }

        public IPgSkill Skill { get { return GetObject(PropertiesOffset + 0, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(PropertiesOffset + 4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Skill != null ? StringToEnumConversion<PowerSkill>.ToString(Skill.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "Level", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawSkillLevel } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return new List<IBackLinkable>() { Skill };
        }
    }
}
