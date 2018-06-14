using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsHardcore: GenericPgObject, IPgAbilityRequirementIsHardcore
    {
        public PgAbilityRequirementIsHardcore(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
