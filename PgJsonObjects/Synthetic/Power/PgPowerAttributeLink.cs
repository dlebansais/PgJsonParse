﻿namespace PgJsonObjects
{
    public class PgPowerAttributeLink : GenericPgObject<PgPowerAttributeLink>, IPgPowerAttributeLink
    {
        public PgPowerAttributeLink(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgPowerAttributeLink CreateItem(byte[] data, int offset)
        {
            return new PgPowerAttributeLink(data, offset);
        }

        public float AttributeEffect { get { return (float)GetDouble(0); } }
        public Attribute AttributeLink { get { return GetObject(4, ref _AttributeLink); } } private Attribute _AttributeLink;
        public Skill SkillLink { get { return GetObject(8, ref _SkillLink); } } private Skill _SkillLink;
    }
}
