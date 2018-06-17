namespace PgJsonObjects
{
    public class PgDrinkEffectServerInfoEffect : GenericPgObject, IPgDrinkEffectServerInfoEffect
    {
        public PgDrinkEffectServerInfoEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgDrinkEffectServerInfoEffect(data, offset);
        }

        public int DrinkATValue { get { return RawDrinkATValue.HasValue ? RawDrinkATValue.Value : 0; } }
        public int? RawDrinkATValue { get { return GetInt(4); } }
        public int AlcoholPowerValue { get { return RawAlcoholPowerValue.HasValue ? RawAlcoholPowerValue.Value : 0; } }
        public int? RawAlcoholPowerValue { get { return GetInt(8); } }
    }
}
