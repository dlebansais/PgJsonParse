namespace PgJsonObjects
{
    public class PlanerSlotGear
    {
        public PlanerSlotGear(IPgItemEffect Effect, float Weight)
        {
            this.Effect = Effect;
            this.Weight = Weight;
        }

        public IPgItemEffect Effect { get; private set; }
        public float Weight { get; private set; }
    }
}
