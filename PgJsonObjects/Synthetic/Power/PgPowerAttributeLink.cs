using System.Collections.Generic;

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

        public override string Key { get { return GetString(4); } }
        public string AttributeName { get { return GetString(8); } }
        public float AttributeEffect { get { return (float)GetDouble(12); } }
        public IPgAttribute AttributeLink { get { return GetObject(16, ref _AttributeLink, PgAttribute.CreateNew); } } private IPgAttribute _AttributeLink;
        public IPgSkill SkillLink { get { return GetObject(20, ref _SkillLink, PgSkill.CreateNew); } } private IPgSkill _SkillLink;
        public FloatFormat AttributeEffectFormat { get { return GetEnum<FloatFormat>(24); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
