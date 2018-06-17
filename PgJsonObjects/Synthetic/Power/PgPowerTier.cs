namespace PgJsonObjects
{
    public class PgPowerTier : GenericPgObject<PgPowerTier>, IPgPowerTier
    {
        public PgPowerTier(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgPowerTier CreateItem(byte[] data, int offset)
        {
            return new PgPowerTier(data, offset);
        }

        public PowerEffectCollection EffectList { get { return GetObjectList(0, ref _EffectList, PowerEffectCollection.CreateItem, () => new PowerEffectCollection()); } } private PowerEffectCollection _EffectList;
    }
}
