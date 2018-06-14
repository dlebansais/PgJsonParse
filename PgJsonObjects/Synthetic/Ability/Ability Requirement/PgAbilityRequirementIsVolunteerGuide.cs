using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVolunteerGuide: GenericPgObject, IPgAbilityRequirementIsVolunteerGuide
    {
        public PgAbilityRequirementIsVolunteerGuide(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
