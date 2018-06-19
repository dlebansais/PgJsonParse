namespace PgJsonObjects
{
    public class PgAbilityRequirementInHotspot: GenericPgObject<PgAbilityRequirementInHotspot>, IPgAbilityRequirementInHotspot
    {
        public PgAbilityRequirementInHotspot(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInHotspot CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementInHotspot CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementInHotspot(data, ref offset);
        }

        public string Name { get { return GetString(4); } }
    }
}
