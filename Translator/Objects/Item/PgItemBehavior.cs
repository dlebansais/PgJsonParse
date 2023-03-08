namespace PgObjects
{
    using System.Collections.Generic;

    public class PgItemBehavior
    {
        public ItemUseVerb UseVerb { get; set; }
        public List<ItemUseRequirement> UseRequirementList { get; set; } = new List<ItemUseRequirement>();
        public ItemUseAnimation UseAnimation { get; set; }
        public ItemUseAnimation UseDelayAnimation { get; set; }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get; set; }
        public float UseDelay { get { return RawUseDelay.HasValue ? RawUseDelay.Value : 0; } }
        public float? RawUseDelay { get; set; }
        public int MinStackSizeNeeded { get { return RawMinStackSizeNeeded.HasValue ? RawMinStackSizeNeeded.Value : 0; } }
        public int? RawMinStackSizeNeeded { get; set; }
    }
}
