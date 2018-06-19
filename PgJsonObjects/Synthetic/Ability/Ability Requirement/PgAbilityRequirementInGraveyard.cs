namespace PgJsonObjects
{
    public class PgAbilityRequirementInGraveyard: GenericPgObject<PgAbilityRequirementInGraveyard>, IPgAbilityRequirementInGraveyard
    {
        public PgAbilityRequirementInGraveyard(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInGraveyard CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementInGraveyard CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementInGraveyard(data, ref offset);
        }
    }
}
