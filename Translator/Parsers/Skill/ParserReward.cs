namespace Translator
{
    using PgJsonObjects;

    public class ParserReward : Parser
    {
        public override object CreateItem()
        {
            return new PgReward();
        }
    }
}
