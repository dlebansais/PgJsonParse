namespace PgJsonObjects
{
    public class PgItemSkillLink : GenericPgObject, IPgItemSkillLink
    {
        public PgItemSkillLink(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgItemSkillLink(data, offset);
        }

        public string SkillName { get { return GetString(0); } }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get { return GetInt(4); } }
        public Skill Link { get { return GetObject(8, ref _Link); } } private Skill _Link;
    }
}
