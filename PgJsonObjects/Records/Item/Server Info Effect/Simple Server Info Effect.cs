namespace PgJsonObjects
{
    public class SimpleServerInfoEffect : ServerInfoEffect
    {
        public SimpleServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, string EffectParameter)
            : base(ServerInfoEffect, RawLevel)
        {
            this.EffectParameter = EffectParameter;
        }

        private string EffectParameter;

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + (EffectParameter != null ? "(" + EffectParameter + ")" : "");
            }
        }
    }
}
