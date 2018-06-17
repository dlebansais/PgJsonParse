namespace PgJsonObjects
{
    public class PgSkillAndLevelServerInfoEffect : GenericPgObject<PgSkillAndLevelServerInfoEffect>, IPgSkillAndLevelServerInfoEffect
    {
        public PgSkillAndLevelServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgSkillAndLevelServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgSkillAndLevelServerInfoEffect(data, offset);
        }

        public Skill Skill { get { return GetObject(4, ref _Skill); } } private Skill _Skill;
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(8); } }
    }
}
