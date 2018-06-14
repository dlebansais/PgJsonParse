using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLycanthrope: GenericPgObject, IPgAbilityRequirementIsLycanthrope
    {
        public PgAbilityRequirementIsLycanthrope(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
