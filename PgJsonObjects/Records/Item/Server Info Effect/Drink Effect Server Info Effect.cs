namespace PgJsonObjects
{
    public class DrinkEffectServerInfoEffect : ServerInfoEffect
    {
        public DrinkEffectServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int DrinkATValue, int AlcoholPowerValue)
            : base(ServerInfoEffect, RawLevel)
        {
            this.DrinkATValue = DrinkATValue;
            this.AlcoholPowerValue = AlcoholPowerValue;
        }

        public int DrinkATValue { get; private set; }
        public int AlcoholPowerValue { get; private set; }
    }
}
