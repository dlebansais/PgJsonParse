namespace PgJsonObjects
{
    public class ConsumedItemByKeyword : ConsumedItem
    {
        public ConsumedItemByKeyword(ConsumedItems Keyword, int? RawCount, double? RawConsumedChance, double? RawChanceToStickInCorpse)
            : base(RawCount, RawConsumedChance, RawChanceToStickInCorpse)
        {
            this.Keyword = Keyword;
        }

        public ConsumedItems Keyword { get; private set; }

        public override string Name { get { return TextMaps.ConsumedItemsTextMap[Keyword]; } }
    }
}
