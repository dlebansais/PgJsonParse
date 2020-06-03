namespace Translator
{
    using PgJsonObjects;

    public class ParserQuestReward : Parser
    {
        public override object CreateItem()
        {
            return new PgQuestReward();
        }
    }
}
