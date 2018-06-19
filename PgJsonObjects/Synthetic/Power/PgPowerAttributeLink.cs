namespace PgJsonObjects
{
    public class PgPowerAttributeLink : GenericPgObject<PgPowerAttributeLink>, IPgPowerAttributeLink
    {
        public PgPowerAttributeLink(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPowerAttributeLink CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPowerAttributeLink CreateNew(byte[] data, ref int offset)
        {
            return new PgPowerAttributeLink(data, ref offset);
        }

        public float AttributeEffect { get { return (float)GetDouble(0); } }
        public IPgAttribute AttributeLink { get { return GetObject(4, ref _AttributeLink, PgAttribute.CreateNew); } } private IPgAttribute _AttributeLink;
        public IPgSkill SkillLink { get { return GetObject(8, ref _SkillLink, PgSkill.CreateNew); } } private IPgSkill _SkillLink;
    }
}
