namespace PgJsonObjects
{
    public class ConsumedItemDirect : ConsumedItem
    {
        public ConsumedItemDirect(IPgItem Link, int? RawCount, double? RawConsumedChance, double? RawChanceToStickInCorpse)
            : base(RawCount, RawConsumedChance, RawChanceToStickInCorpse)
        {
            this.Link = Link;
        }

        public IPgItem Link { get; private set; }
        public override string Name { get { return Link.Name; } }
    }
}
