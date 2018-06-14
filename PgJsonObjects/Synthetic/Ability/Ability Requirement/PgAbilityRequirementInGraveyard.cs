namespace PgJsonObjects
{
    public class PgAbilityRequirementInGraveyard: GenericPgObject, IPgAbilityRequirementInGraveyard
    {
        public PgAbilityRequirementInGraveyard(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
