namespace PgBuilder
{
    using PgJsonObjects;

    public class OtherEffectSimple : OtherEffect
    {
        public OtherEffectSimple(IPgEffect effect)
        {
            Description = effect.Desc;
        }

        public override bool IsDisplayed { get { return true; } }

        public override void Reset()
        {
        }

        public string Description { get; }
    }
}
