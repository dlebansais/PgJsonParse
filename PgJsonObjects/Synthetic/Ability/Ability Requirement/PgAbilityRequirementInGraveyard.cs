namespace PgJsonObjects
{
    public class PgAbilityRequirementInGraveyard: GenericPgObject<PgAbilityRequirementInGraveyard>, IPgAbilityRequirementInGraveyard
    {
        public PgAbilityRequirementInGraveyard(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInGraveyard CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementInGraveyard(data, offset);
        }
    }
}
