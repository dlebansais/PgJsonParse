namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLongtimeAnimal: GenericPgObject, IPgAbilityRequirementIsLongtimeAnimal
    {
        public PgAbilityRequirementIsLongtimeAnimal(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsLongtimeAnimal(data, offset);
        }
    }
}
