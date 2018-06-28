using System.Collections.Generic;

namespace PgJsonObjects
{
    public class WeightProfile
    {
        public WeightProfile(string Name)
        {
            this.Name = Name;

            AttributeWeightList = new List<AttributeWeight>();
        }

        public string Name { get; private set; }
        public List<AttributeWeight> AttributeWeightList { get; private set; }

        public void AddAttributeWeight(ICollection<IPgAttribute> AttributeList, string Label, float Weight)
        {
            foreach (AttributeWeight Item in AttributeWeightList)
                if (Item.Attribute.Label == Label)
                    return;

            foreach (IPgAttribute Attribute in AttributeList)
                if (Attribute.Label == Label)
                {
                    AttributeWeightList.Add(new AttributeWeight(Attribute, Weight));
                    break;
                }
        }

        public void AddAttributeWeight(IPgAttribute Attribute, float Weight)
        {
            foreach (AttributeWeight Item in AttributeWeightList)
                if (Item.Attribute.Label == Attribute.Label)
                    return;

            AttributeWeightList.Add(new AttributeWeight(Attribute, Weight));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
