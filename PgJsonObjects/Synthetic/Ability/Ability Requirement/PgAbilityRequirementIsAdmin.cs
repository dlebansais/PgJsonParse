using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsAdmin: GenericPgObject, IPgAbilityRequirementIsAdmin
    {
        public PgAbilityRequirementIsAdmin(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
