namespace PgJsonObjects
{
    public class PgAbilityServerInfoEffect : GenericPgObject<PgAbilityServerInfoEffect>, IPgAbilityServerInfoEffect
    {
        public PgAbilityServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityServerInfoEffect CreateItem(byte[] data, int offset)
        {
            return new PgAbilityServerInfoEffect(data, offset);
        }

        public Ability BestowAbility { get { return GetObject(4, ref _BestowAbility); } } private Ability _BestowAbility;
    }
}
