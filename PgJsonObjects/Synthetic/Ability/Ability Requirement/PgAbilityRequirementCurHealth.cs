namespace PgJsonObjects
{
    public class PgAbilityRequirementCurHealth: GenericPgObject<PgAbilityRequirementCurHealth>, IPgAbilityRequirementCurHealth
    {
        public PgAbilityRequirementCurHealth(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementCurHealth CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementCurHealth CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementCurHealth(data, ref offset);
        }

        public double Health { get { return RawHealth.HasValue ? RawHealth.Value : 0; } }
        public double? RawHealth { get { return GetInt(4); } }
    }
}
