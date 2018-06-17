namespace PgJsonObjects
{
    public class PgAbilityRequirementInGraveyard: GenericPgObject, IPgAbilityRequirementInGraveyard
    {
        public PgAbilityRequirementInGraveyard(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementInGraveyard(data, offset);
        }
    }
}
