namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleAppearance: GenericPgObject<PgAbilityRequirementSingleAppearance>, IPgAbilityRequirementSingleAppearance
    {
        public PgAbilityRequirementSingleAppearance(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementSingleAppearance CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementSingleAppearance(data, offset);
        }

        public Appearance Appearance { get { return GetEnum<Appearance>(4); } }
    }
}
