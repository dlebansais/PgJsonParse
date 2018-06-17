namespace PgJsonObjects
{
    public class PgMinSkillLevelQuestRequirement : GenericPgObject, IPgMinSkillLevelQuestRequirement
    {
        public PgMinSkillLevelQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Skill ConnectedSkill { get { return GetObject(4, ref _ConnectedSkill); } } private Skill _ConnectedSkill;
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(8); } }
    }
}
