namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVolunteerGuide: GenericPgObject<PgAbilityRequirementIsVolunteerGuide>, IPgAbilityRequirementIsVolunteerGuide
    {
        public PgAbilityRequirementIsVolunteerGuide(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsVolunteerGuide CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsVolunteerGuide(data, offset);
        }
    }
}
