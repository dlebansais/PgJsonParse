namespace PgJsonObjects
{
    public class ValueServerInfoEffect : ServerInfoEffect
    {
        public ValueServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, int Value)
            : base(ServerInfoEffect, RawLevel)
        {
            this.Value = Value;
        }

        public int Value { get; private set; }
    }
}
