namespace PgJsonObjects
{
    public class PgPowerAttributeLink : GenericPgObject, IPgPowerAttributeLink
    {
        public PgPowerAttributeLink(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public float AttributeEffect { get { return (float)GetDouble(0); } }
        public Attribute AttributeLink { get { return GetObject(4, ref _AttributeLink); } } private Attribute _AttributeLink;
        public Skill SkillLink { get { return GetObject(8, ref _SkillLink); } } private Skill _SkillLink;
    }
}
