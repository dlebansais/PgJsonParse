namespace PgJsonObjects
{
    public class AttributeWeight
    {
        public AttributeWeight(IPgAttribute Attribute, float Weight)
        {
            this.Attribute = Attribute;
            this.Weight = Weight;
        }

        public IPgAttribute Attribute { get; private set; }
        public float Weight { get; private set; }
    }
}
