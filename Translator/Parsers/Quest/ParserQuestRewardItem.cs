namespace Translator
{
    using PgJsonObjects;

    public class ParserQuestRewardItem : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestRewardItem();
        }
    }
}
