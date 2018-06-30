using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVolunteerGuide: GenericPgObject<PgAbilityRequirementIsVolunteerGuide>, IPgAbilityRequirementIsVolunteerGuide
    {
        public PgAbilityRequirementIsVolunteerGuide(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsVolunteerGuide CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsVolunteerGuide CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsVolunteerGuide(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
