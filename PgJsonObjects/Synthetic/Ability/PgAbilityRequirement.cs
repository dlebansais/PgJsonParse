namespace PgJsonObjects
{
    public abstract class PgAbilityRequirement<TPg> : GenericPgObject<TPg>, IPgAbilityRequirement
        where TPg : IDeserializablePgObject
    {
        public PgAbilityRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }
    }
}
