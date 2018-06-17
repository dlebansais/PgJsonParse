namespace PgJsonObjects
{
    public class PgAbilityRequirementInHotspot: GenericPgObject, IPgAbilityRequirementInHotspot
    {
        public PgAbilityRequirementInHotspot(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Name { get { return GetString(4); } }
    }
}
