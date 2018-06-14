using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementFullMoon: GenericPgObject, IPgAbilityRequirementFullMoon
    {
        public PgAbilityRequirementFullMoon(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
