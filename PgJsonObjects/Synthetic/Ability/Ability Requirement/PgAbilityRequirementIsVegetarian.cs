using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVegetarian: GenericPgObject, IPgAbilityRequirementIsVegetarian
    {
        public PgAbilityRequirementIsVegetarian(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
