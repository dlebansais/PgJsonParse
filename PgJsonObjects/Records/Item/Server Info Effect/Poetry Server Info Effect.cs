namespace PgJsonObjects
{
    public class PoetryServerInfoEffect : ServerInfoEffect
    {
        public PoetryServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int PoetryXpValue, int RecitalXpValue)
            : base(ServerInfoEffect, RawLevel)
        {
            this.PoetryXpValue = PoetryXpValue;
            this.RecitalXpValue = RecitalXpValue;
        }

        public int PoetryXpValue { get; private set; }
        public int RecitalXpValue { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + PoetryXpValue + "," + RecitalXpValue + ")";
            }
        }
    }
}
