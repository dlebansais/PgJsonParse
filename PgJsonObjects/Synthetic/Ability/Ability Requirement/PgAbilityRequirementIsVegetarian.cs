namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVegetarian: GenericPgObject<PgAbilityRequirementIsVegetarian>, IPgAbilityRequirementIsVegetarian
    {
        public PgAbilityRequirementIsVegetarian(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsVegetarian CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsVegetarian(data, offset);
        }
    }
}
