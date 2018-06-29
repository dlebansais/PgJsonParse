namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleAppearance: GenericPgObject<PgAbilityRequirementSingleAppearance>, IPgAbilityRequirementSingleAppearance
    {
        public PgAbilityRequirementSingleAppearance(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementSingleAppearance CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementSingleAppearance CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementSingleAppearance(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public Appearance Appearance { get { return GetEnum<Appearance>(8); } }
    }
}
