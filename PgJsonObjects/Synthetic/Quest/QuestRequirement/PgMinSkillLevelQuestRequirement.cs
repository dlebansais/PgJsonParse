namespace PgJsonObjects
{
    public class PgMinSkillLevelQuestRequirement : GenericPgObject<PgMinSkillLevelQuestRequirement>, IPgMinSkillLevelQuestRequirement
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

        public override string Key { get { return GetString(4); } }
        public IPgSkill Skill { get { return GetObject(8, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(12); } }
    }
}
