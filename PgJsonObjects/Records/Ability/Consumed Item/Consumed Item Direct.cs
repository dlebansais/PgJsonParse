namespace PgJsonObjects
{
    public class ConsumedItemDirect : ConsumedItem
    {
        public ConsumedItemDirect(Item Link, int? RawCount, double? RawConsumedChance, double? RawChanceToStickInCorpse)
            : base(RawCount, RawConsumedChance, RawChanceToStickInCorpse)
        {
            this.Link = Link;
        }

        public Item Link { get; private set; }
        public override string Name { get { return Link.Name; } }
    }
}
