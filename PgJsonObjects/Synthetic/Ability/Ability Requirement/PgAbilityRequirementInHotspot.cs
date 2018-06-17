namespace PgJsonObjects
{
    public class PgAbilityRequirementInHotspot: GenericPgObject<PgAbilityRequirementInHotspot>, IPgAbilityRequirementInHotspot
    {
        public PgAbilityRequirementInHotspot(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInHotspot CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementInHotspot(data, offset);
        }

        public string Name { get { return GetString(4); } }
    }
}
