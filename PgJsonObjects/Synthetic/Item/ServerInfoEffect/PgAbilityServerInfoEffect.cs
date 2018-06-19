namespace PgJsonObjects
{
    public class PgAbilityServerInfoEffect : GenericPgObject<PgAbilityServerInfoEffect>, IPgAbilityServerInfoEffect
    {
        public PgAbilityServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityServerInfoEffect(data, ref offset);
        }

        public IPgAbility BestowAbility { get { return GetObject(4, ref _BestowAbility, PgAbility.CreateNew); } } private IPgAbility _BestowAbility;
    }
}
