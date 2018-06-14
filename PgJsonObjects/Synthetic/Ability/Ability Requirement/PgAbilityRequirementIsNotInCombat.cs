using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsNotInCombat: GenericPgObject, IPgAbilityRequirementIsNotInCombat
    {
        public PgAbilityRequirementIsNotInCombat(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
