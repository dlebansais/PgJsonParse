namespace PgJsonObjects
{
    public class IntervalServerInfoEffect : ServerInfoEffect
    {
        public IntervalServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int LowValue, int HighValue)
            : base(ServerInfoEffect, RawLevel)
        {
            this.LowValue = LowValue;
            this.HighValue = HighValue;
        }

        public int LowValue { get; private set; }
        public int HighValue { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + LowValue + "," + HighValue + ")";
            }
        }
    }
}
