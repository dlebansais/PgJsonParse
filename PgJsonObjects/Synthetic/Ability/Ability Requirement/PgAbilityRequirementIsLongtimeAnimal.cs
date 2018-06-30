using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLongtimeAnimal: GenericPgObject<PgAbilityRequirementIsLongtimeAnimal>, IPgAbilityRequirementIsLongtimeAnimal
    {
        public PgAbilityRequirementIsLongtimeAnimal(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsLongtimeAnimal CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsLongtimeAnimal CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsLongtimeAnimal(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
