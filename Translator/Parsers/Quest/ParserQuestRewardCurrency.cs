namespace Translator
{
    using PgJsonObjects;

    public class ParserQuestRewardCurrency : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestRewardCurrency();
        }
    }
}
