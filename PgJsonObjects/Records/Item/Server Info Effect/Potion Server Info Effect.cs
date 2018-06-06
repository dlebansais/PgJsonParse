namespace PgJsonObjects
{
    public class PotionServerInfoEffect : ServerInfoEffect
    {
        public PotionServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int? HealthGainInstant, int? ArmorGainInstant, int? PowerGainInstant)
            : base(ServerInfoEffect, RawLevel)
        {
            EffectString = HealthGainInstant.Value.ToString();

            if (ArmorGainInstant.HasValue)
                EffectString += "," + ArmorGainInstant.Value.ToString();

            if (PowerGainInstant.HasValue)
                EffectString += "," + PowerGainInstant.Value.ToString();
        }

        public PotionServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int? HealthGainPerSecond, int? ArmorGainPerSecond, int? PowerGainPerSecond, int? TotalGainDuration)
            : base(ServerInfoEffect, RawLevel)
        {
            EffectString = HealthGainPerSecond.Value + "," + ArmorGainPerSecond.Value + "," + PowerGainPerSecond.Value + ",0," + TotalGainDuration.Value;
        }

        private string EffectString;

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + EffectString + ")";
            }
        }
    }
}
