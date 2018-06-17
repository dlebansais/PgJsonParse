namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLongtimeAnimal: GenericPgObject<PgAbilityRequirementIsLongtimeAnimal>, IPgAbilityRequirementIsLongtimeAnimal
    {
        public PgAbilityRequirementIsLongtimeAnimal(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsLongtimeAnimal CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsLongtimeAnimal(data, offset);
        }
    }
}
