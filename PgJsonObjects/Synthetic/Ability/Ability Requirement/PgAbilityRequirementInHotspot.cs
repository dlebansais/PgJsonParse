namespace PgJsonObjects
{
    public class PgAbilityRequirementInHotspot: GenericPgObject, IPgAbilityRequirementInHotspot
    {
        public PgAbilityRequirementInHotspot(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementInHotspot(data, offset);
        }

        public string Name { get { return GetString(4); } }
    }
}
