namespace PgJsonObjects
{
    public class ConsumedItemByKeyword : ConsumedItem
    {
        public ConsumedItemByKeyword(ConsumedItemCategory Keyword/*, int? RawCount, double? RawConsumedChance, double? RawChanceToStickInCorpse*/)
            : base(/*RawCount, RawConsumedChance, RawChanceToStickInCorpse*/)
        {
            this.Keyword = Keyword;
        }

        public ConsumedItemCategory Keyword { get; private set; }

        public override string Name { get { return TextMaps.ConsumedItemCategoryTextMap[Keyword]; } }
    }
}
