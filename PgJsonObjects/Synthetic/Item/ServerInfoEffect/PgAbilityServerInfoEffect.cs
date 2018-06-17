namespace PgJsonObjects
{
    public class PgAbilityServerInfoEffect : GenericPgObject, IPgAbilityServerInfoEffect
    {
        public PgAbilityServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Ability BestowAbility { get { return GetObject(4, ref _BestowAbility); } } private Ability _BestowAbility;
    }
}
