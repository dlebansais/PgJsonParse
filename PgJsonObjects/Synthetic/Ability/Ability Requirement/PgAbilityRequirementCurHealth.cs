namespace PgJsonObjects
{
    public class PgAbilityRequirementCurHealth: GenericPgObject, IPgAbilityRequirementCurHealth
    {
        public PgAbilityRequirementCurHealth(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public double Health { get { return RawHealth.HasValue ? RawHealth.Value : 0; } }
        public double? RawHealth { get { return GetInt(4); } }
    }
}
