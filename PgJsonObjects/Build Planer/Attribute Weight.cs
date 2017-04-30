namespace PgJsonObjects
{
    public class AttributeWeight
    {
        public AttributeWeight(Attribute Attribute, float Weight)
        {
            this.Attribute = Attribute;
            this.Weight = Weight;
        }

        public Attribute Attribute { get; private set; }
        public float Weight { get; private set; }
    }
}
