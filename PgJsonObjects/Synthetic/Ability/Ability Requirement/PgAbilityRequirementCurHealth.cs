namespace PgJsonObjects
{
    public class PgAbilityRequirementCurHealth: GenericPgObject<PgAbilityRequirementCurHealth>, IPgAbilityRequirementCurHealth
    {
        public PgAbilityRequirementCurHealth(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementCurHealth CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementCurHealth(data, offset);
        }

        public double Health { get { return RawHealth.HasValue ? RawHealth.Value : 0; } }
        public double? RawHealth { get { return GetInt(4); } }
    }
}
