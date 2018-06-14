using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLongtimeAnimal: GenericPgObject, IPgAbilityRequirementIsLongtimeAnimal
    {
        public PgAbilityRequirementIsLongtimeAnimal(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
