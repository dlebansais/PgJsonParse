namespace Translator
{
    using PgJsonObjects;

    public class ParserAI : Parser
    {
        public override object CreateItem()
        {
            return new PgAI();
        }
    }
}
