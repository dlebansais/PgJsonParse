namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleAppearance: GenericPgObject, IPgAbilityRequirementSingleAppearance
    {
        public PgAbilityRequirementSingleAppearance(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Appearance Appearance { get { return GetEnum<Appearance>(4); } }
    }
}
