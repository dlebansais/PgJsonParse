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
    }
}
