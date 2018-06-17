namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVegetarian: GenericPgObject, IPgAbilityRequirementIsVegetarian
    {
        public PgAbilityRequirementIsVegetarian(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsVegetarian(data, offset);
        }
    }
}
