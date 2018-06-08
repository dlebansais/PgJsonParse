namespace PgJsonObjects
{
    public class ValueServerInfoEffect : ServerInfoEffect
    {
        public ValueServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int Value, bool sameValue)
            : base(ServerInfoEffect, RawLevel)
        {
            this.Value = Value;
            SameValue = sameValue;
        }

        public int Value { get; private set; }
        public bool SameValue { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + Value + (SameValue ? ("," + Value) : "") + ")";
            }
        }
    }
}
