namespace PgJsonObjects
{
    public class PgDrinkEffectServerInfoEffect : GenericPgObject<PgDrinkEffectServerInfoEffect>, IPgServerInfoEffect, IPgDrinkEffectServerInfoEffect
    {
        public PgDrinkEffectServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgDrinkEffectServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgDrinkEffectServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgDrinkEffectServerInfoEffect(data, ref offset);
        }

        public int DrinkATValue { get { return RawDrinkATValue.HasValue ? RawDrinkATValue.Value : 0; } }
        public int? RawDrinkATValue { get { return GetInt(4); } }
        public int AlcoholPowerValue { get { return RawAlcoholPowerValue.HasValue ? RawAlcoholPowerValue.Value : 0; } }
        public int? RawAlcoholPowerValue { get { return GetInt(8); } }
    }
}
