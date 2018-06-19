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

        public IPgSkill ConnectedSkill { get { return GetObject(4, ref _ConnectedSkill, PgSkill.CreateNew); } } private IPgSkill _ConnectedSkill;
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(8); } }
    }
}
