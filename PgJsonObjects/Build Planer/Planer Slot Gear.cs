namespace PgJsonObjects
{
    public class PlanerSlotGear
    {
        public PlanerSlotGear(ItemEffect Effect, float Weight)
        {
            this.Effect = Effect;
            this.Weight = Weight;
        }

        public ItemEffect Effect { get; private set; }
        public float Weight { get; private set; }
    }
}
