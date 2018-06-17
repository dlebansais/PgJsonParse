namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVolunteerGuide: GenericPgObject, IPgAbilityRequirementIsVolunteerGuide
    {
        public PgAbilityRequirementIsVolunteerGuide(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsVolunteerGuide(data, offset);
        }
    }
}
